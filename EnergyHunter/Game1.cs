namespace EnergyHunter
{
    enum Stat
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
        private Stat stat = Stat.SplashScreen;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1900;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.IsFullScreen = false;

            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SplashScreen.Background = Content.Load<Texture2D>("background");
            SplashScreen.nameFont = Content.Load<SpriteFont>("splashFont");
            SplashScreen.mmButtomsFont = Content.Load<SpriteFont>("mmButtom");
        }

        protected override void Update(GameTime gameTime)
        {
            switch(stat)
            {
                case Stat.SplashScreen:
                    SplashScreen.Update();
                    if (Keyboard.GetState().IsKeyDown(Keys.Q))
                        Exit();
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        stat = Stat.Game;
                    break;
                case Stat.Game:
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        stat = Stat.SplashScreen;
                    break;

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            switch(stat)
            {
                case Stat.SplashScreen:
                    SplashScreen.Draw(_spriteBatch);
                    break;
                case Stat.Game:
                    break;
            }
            SplashScreen.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}