using System.Collections.Generic;

namespace EnergyHunter
{
    public class Game1 : Game
    {
        public enum GameState
        {
            Game, Pause, Menu, Finish
        }

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public GameState gameState = GameState.Menu;
        private List<Component> _gameComponents;
        private List<Component> _gameComponents2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 748;
            this.IsFixedTimeStep = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            EngineFunc.Init("Images/", GraphicsDevice);
            Map.CreateMap();
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SplashScreen.Background = Content.Load<Texture2D>("background");
            SplashScreen.nameFont = Content.Load<SpriteFont>("splashFont");
            FinishScreen.Background = Content.Load<Texture2D>("background");
            var sgButton = new Button(Content.Load<Texture2D>("buttonBg"), Content.Load<SpriteFont>("buttonFont"))
            {
                Position = new Vector2(100, 350),
                Text = "Начать игру"

            };

            var quitButton = new Button(Content.Load<Texture2D>("buttonBg"), Content.Load<SpriteFont>("buttonFont"))
            {
                Position = new Vector2(100, 470),
                Text = "Выйти"
            };

            var mmButton = new Button(Content.Load<Texture2D>("buttonBg"), Content.Load<SpriteFont>("buttonFont"))
            {
                Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 - 50),
                Text = "Главное Меню"

            };
            var finquitButton = new Button(Content.Load<Texture2D>("buttonBg"), Content.Load<SpriteFont>("buttonFont"))
            {
                Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2 + 50),
                Text = "Выйти"
            };

            finquitButton.Click += QuitButton_Click;
            quitButton.Click += QuitButton_Click;
            _gameComponents = new List<Component>()
            {
                sgButton,
                quitButton,
            };
            _gameComponents2 = new List<Component>()
            {
                finquitButton,
                mmButton,
            };
            sgButton.Click += Button_Click;
            mmButton.Click += Button_ClickMM;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            switch (gameState)
            {
                case GameState.Game:
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                            gameState = GameState.Menu;
                        EngineFunc.BackgroundEngine.Move((float)gameTime.ElapsedGameTime.TotalMilliseconds / 16.6f);
                        EngineFunc.SpriteEngine.Move((float)gameTime.ElapsedGameTime.TotalMilliseconds / 16.6f);
                        break;
                    }
                case GameState.Menu:
                    {
                        SplashScreen.Update();
                        foreach (var component in _gameComponents)
                            component.UpdateButton(gameTime);
                        if (QuitButton_Click == null)
                            Exit();
                        if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                            gameState = GameState.Game;
                        break;
                    }
                case GameState.Finish:
                    {
                        FinishScreen.Update();
                        foreach (var component in _gameComponents2)
                            component.UpdateButton(gameTime);
                        if (QuitButton_Click == null)
                            Exit();
                        break;
                    }
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.Game:
                    {
                        EngineFunc.BackgroundEngine.Draw();
                        EngineFunc.SpriteEngine.Draw();
                        EngineFunc.SpriteEngine.Dead();
                        break;
                    }
                case GameState.Menu:
                    {
                        SplashScreen.Draw(_spriteBatch);
                        foreach (var component in _gameComponents)
                            component.DrawButton(gameTime, _spriteBatch);
                        break;
                    }
                case GameState.Finish:
                    {
                        FinishScreen.Draw(_spriteBatch);
                        foreach (var component in _gameComponents2)
                            component.DrawButton(gameTime, _spriteBatch);
                        break;
                    }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        #region Buttons
        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            gameState = GameState.Game;
        }

        private void Button_ClickMM(object sender, System.EventArgs e)
        {
            gameState = GameState.Menu;
        }
        #endregion
    }
}