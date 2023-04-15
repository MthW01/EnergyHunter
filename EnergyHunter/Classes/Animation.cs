using System.Linq;

namespace EnergyHunter
{
    class Animation
    {
        Texture2D texture;
        Rectangle rectangle;
        Vector2 position;
        Vector2 velocity;
        Vector2 origin;
        #region Animation Numbers
        // Animation numbers
        // 0-8 run right
        // 9-17 run left
        // 18-20 stay right
        // 21-23 stay left
        // 24-27 jump right
        // 28-31 jump left
        // 32-35 shoot right
        // 36-39 shoot left
        // 40-43 getdmg right
        // 44-47 getdmg left
        // 48-53 death right
        // 54-59 death left
        #endregion
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
            {
                velocity = Vector2.Zero;
                //if (Keyboard.GetState().GetPressedKeys().Last().Equals(Keys.Left))
                //    StayLeft(gameTime);
                //if (Keyboard.GetState().GetPressedKeys().Last().Equals(Keys.Right))
                //    StayRight(gameTime);
            }


        }

        public void AnimateLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrame++;
                timer = 0;
                if (currentFrame > 8)
                    currentFrame = 0;
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

        public void StayLeft(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrameForStay++;
                timer = 0;
                if (currentFrameForStay > 2)
                    currentFrameForStay = 0;

            }
        }

        public void StayRight(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 2;
            if (timer > interval)
            {
                currentFrameForStay++;
                timer = 0;
                if (currentFrameForStay > 5 || currentFrameForStay <  3)
                    currentFrameForStay = 3;

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, rectangle, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        }

    }
}
