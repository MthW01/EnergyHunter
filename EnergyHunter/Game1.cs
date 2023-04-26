using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace EnergyHunter
{
    enum State
    {
        SplashScreen,
        Game,
        Pause,
        Final
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private State state = State.Game;
        private List<Component> _gameComponents;
        private Player player;
        Map map;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }


        protected override void Initialize()
        {
            map = new Map();
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = true;
            player = new Player();
            _graphics.ApplyChanges();
            base.Initialize();
            
        }

        protected override void LoadContent()
        {
            Tiles.Content = Content;
            #region Map Generation
            map.Generate(new int[,] {
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,1,0,0,0,0, },
                {0,0,0,0,0,0,0,0,0,0,0,1,1,1,2,2,0,0,0,0, },
                {0,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0, },
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,2,2,0,0,0,0, },
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0, },
                {1,1,1,2,1,1,2,2,2,2,2,2,2,2,2,2,0,0,0,0, },
            }, 100);

            #endregion
            player.Load(Content);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SplashScreen.Background = Content.Load<Texture2D>("background");
            SplashScreen.nameFont = Content.Load<SpriteFont>("splashFont");
            var sgButton = new Button(Content.Load<Texture2D>("123"), Content.Load<SpriteFont>("buttonFont"))
            {
                Position = new Vector2(100, 350),
                Text = "Начать игру"
               
            };
            var quitButton = new Button(Content.Load<Texture2D>("123"), Content.Load<SpriteFont>("buttonFont"))
            {
                Position = new Vector2(100, 470),
                Text = "Выйти"
            };

            quitButton.Click += QuitButton_Click;
            _gameComponents = new List<Component>()
            {
                sgButton,
                quitButton,
            };
            sgButton.Click += Button_Click;
        }

        protected override void Update(GameTime gameTime)
        {
            switch(state)
            {
                #region SplashScreen
                case State.SplashScreen:
                    SplashScreen.Update();
                    foreach (var component in _gameComponents)
                        component.UpdateButton(gameTime);
                    if (QuitButton_Click == null)
                        Exit();
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        state = State.Game;
                    break;
                #endregion
                #region Game
                case State.Game:
                    player.Update();
                    foreach (ColissionTiles tiles in map.ColissionTiles)
                        player.Collision(tiles.Rectangle, map.Width, map.Height);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        state = State.SplashScreen;
                    break;
                #endregion
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            switch(state)
            {
                case State.SplashScreen:
                    SplashScreen.Draw(_spriteBatch);
                    foreach (var component in _gameComponents)
                        component.DrawButton(gameTime, _spriteBatch);
                    break;
                case State.Game:
                    _spriteBatch.Draw(Content.Load<Texture2D>("backgroundSky"), Vector2.Zero, Color.White);
                    map.Draw(_spriteBatch);
                    player.Draw(gameTime, _spriteBatch);
                    break;
            }
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void QuitButton_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            state = State.Game;
        }
    }
}