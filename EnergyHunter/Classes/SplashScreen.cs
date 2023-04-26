using System;

namespace EnergyHunter
{
    static class SplashScreen
    {
        public static Texture2D Background { get; set; }
        public static Texture2D bgSky { get; set; }
        public static SpriteFont nameFont { get; set; }
        public static Texture2D sgButton { get; set; }

        static int timeCounter = 0;
        static Color color;

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, Vector2.Zero, color);
            spriteBatch.DrawString(nameFont, "Energy  Hunter", new Vector2(100, 100), color);
        }

        public static void Update()
        {
            color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);

            if (timeCounter < 255)
                timeCounter++;
        }
    }

    public abstract class Component
    {
        public abstract void DrawButton(GameTime gameTime, SpriteBatch spriteBatch);
        public abstract void UpdateButton(GameTime gameTime);
    }
    public class Button : Component
    {
        #region Fields

        private MouseState _currentMouse;
        private SpriteFont _font;
        private bool _isHovering;
        private MouseState _previousMouse;
        private Texture2D _texture;

        #endregion

        #region Properties

        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }

        public string Text { get; set; }

        #endregion

        #region Methods

        public Button(Texture2D texture, SpriteFont font)
        {
            _texture = texture;
            _font = font;
            PenColour = Color.White;
        }
        public override void DrawButton(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(_texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (_font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(_font, Text, new Vector2(x, y), PenColour);
            }
        }

        public override void UpdateButton(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                    Click?.Invoke(this, new EventArgs());
            }
        }

        #endregion
    }
}
