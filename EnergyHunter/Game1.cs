using System;
using System.Collections.Generic;

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
        private State state = State.SplashScreen;
        private List<Component> _gameComponents;
        Animation player;

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
            player = new Animation(Content.Load<Texture2D>("run"), new Vector2(300,300), 150, 150);
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            SplashScreen.Background = Content.Load<Texture2D>("background");
            SplashScreen.nameFont = Content.Load<SpriteFont>("splashFont");
            //SplashScreen.sgButton = Content.Load<Texture2D>("startgame");
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
                case State.SplashScreen:
                    SplashScreen.Update();
                    foreach (var component in _gameComponents)
                        component.UpdateButton(gameTime);
                    if (QuitButton_Click == null)
                        Exit();
                    if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                        state = State.Game;
                    break;
                case State.Game:
                    player.Update(gameTime);
                    if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                        state = State.SplashScreen;
                    break;

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
                    player.Draw(_spriteBatch);
                    break;
            }
            
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void QuitButton_Click(object sender, System.EventArgs e)
        {
            Exit();
        }

        private void Button_Click(object sender, System.EventArgs e)
        {
            state = State.Game;
        }
    }
}