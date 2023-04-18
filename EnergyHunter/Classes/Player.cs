namespace EnergyHunter
{
    class Player
    {
        AnimationPlayer animationPlayer;

        Animation runAnim;
        Animation stayAnim;
        //Animation jumpAnim;

        Vector2 position = new Vector2(50, 450);
        Vector2 velocity;

        public Player()
        {

        }
        public void Load(ContentManager content)
        {
            runAnim = new Animation(content.Load<Texture2D>("run"), 150, 0.1f, true);
            stayAnim = new Animation(content.Load<Texture2D>("stay"), 150, 0.3f, true);
        }
        public void Update()
        {
            position += velocity;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                velocity.X = 5f;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                velocity.X = -5f;
            else velocity.X = 0f;

            if (velocity.X != 0)
                animationPlayer.PlayAnimation(runAnim);
            else if(velocity.X == 0)
                animationPlayer.PlayAnimation(stayAnim);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            SpriteEffects flip = SpriteEffects.None;
            if (velocity.X >= 0)
                flip = SpriteEffects.None;
            else if (velocity.X < 0)
                flip = SpriteEffects.FlipHorizontally;

            animationPlayer.Draw(gameTime, spriteBatch, position, flip);
        }
    }
}
