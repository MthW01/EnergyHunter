using System.Linq;

namespace EnergyHunter
{
    public class Player
    {
        AnimationPlayer animationPlayer;

        Animation runAnim;
        Animation stayAnim;
        Animation stayLeftAnim;
        public bool isStayRight;
        public bool hasJumped;
        public Rectangle rectangle;
        public Vector2 position = new Vector2(150, 300);
        public Vector2 velocity;

        public Player()
        {
            hasJumped = true;
        }

        public Vector2 GetPosForShoot => new Vector2(position.X, position.Y);
        public void Load(ContentManager content)
        {
            runAnim = new Animation(content.Load<Texture2D>("run"), 150, 0.08f, true);
            stayAnim = new Animation(content.Load<Texture2D>("stayRight"), 150, 0.2f, true);
            stayLeftAnim = new Animation(content.Load<Texture2D>("stayLeft"), 150, 0.2f, true);
        }
        public void Update()
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, 150, 150);
            position += velocity;
            #region Run Logic
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                velocity.X = 5f;
                isStayRight = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                velocity.X = -5f;
                isStayRight = false;
            }
            else velocity.X = 0f;
            #endregion
            #region Jump Logic
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= 6f;
                velocity.Y = -9f;
                hasJumped = true;
            }
            if (hasJumped)
            {
                float i = 1;
                velocity.Y += 0.15f * i;
            }
            if (position.Y + runAnim.Texture.Height >= 1250)
                hasJumped = false;

            #endregion
            // Run Animation
            if (velocity.X != 0)
                animationPlayer.PlayAnimation(runAnim);

            //Stay Animation
            if (isStayRight && velocity.X == 0)
                animationPlayer.PlayAnimation(stayAnim);
            else if (!isStayRight && velocity.X == 0)
                animationPlayer.PlayAnimation(stayLeftAnim);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;
            if (velocity.X >= 0)
            {
                flip = SpriteEffects.None;
            }
            else if (velocity.X < 0)
            {
                flip = SpriteEffects.FlipHorizontally;
            }

            animationPlayer.Draw(gameTime, spriteBatch, position, flip);
        }
        public void Collision(Rectangle newRectangle, int xOffset, int yOffset)
        {
            if (rectangle.TouchTopOf(newRectangle))
            {
                rectangle.Y = newRectangle.Y - rectangle.Height;
                velocity.Y = 0f;
                hasJumped = false;
            }

            if (rectangle.TouchLeftOf(newRectangle))
                position.X = newRectangle.X - rectangle.Width - 1;

            if (rectangle.TouchRightOf(newRectangle))
                position.X = newRectangle.X + newRectangle.Width + 2;

            if (rectangle.TouchBottomOf(newRectangle))
                velocity.Y = 1f;

            if (position.X < 0)
                position.X = 0;
            if (position.X > xOffset - rectangle.Width)
                position.X = xOffset - rectangle.Width;
            if (position.Y - 145 < 0)
                velocity.Y = 1f;
            if (position.Y > yOffset - rectangle.Height)
            {
                position.Y = 400;
                position.X = 100;
            }
                 
        }
    }
}
