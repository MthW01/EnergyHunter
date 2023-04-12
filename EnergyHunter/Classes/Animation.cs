namespace EnergyHunter
{
    class Animation
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 position;
        Vector2 velocity;
        Vector2 origin;

        int currentFrame;
        int currentFrameForStay;
        int frameHeight;
        int frameWidth;

        float timer;
        float interval = 45;

        public Animation(Texture2D newTexture, Vector2 newPosition, int newFrameHeight, int newFrameWidth)
        {
            texture = newTexture;
            position = newPosition;
            frameHeight = newFrameHeight;
            frameWidth = newFrameWidth;
        }
        public void Update(GameTime gameTime)
        {
            rectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
            origin = new Vector2(rectangle.Width / 2, rectangle.Height / 2);
            position += velocity;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                AnimateRight(gameTime);
                velocity.X = 3;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                AnimateLeft(gameTime);
                velocity.X = -3;
            }
            else
                velocity = Vector2.Zero;


        }

        public void AnimateLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame--;
                timer = 0;
                if (currentFrame < 0)
                    currentFrame = 5;
            }
        }
        public void AnimateRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 17 || currentFrame < 9)
                    currentFrame = 9;
            }
        }

        //public void JustStay(GameTime gameTime)
        //{
        //    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
        //    if (timer > interval)
        //    {
        //        currentFrameForStay++;
        //        timer = 0;
        //        if (currentFrameForStay)

        //    }
        //}

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

    }
}
