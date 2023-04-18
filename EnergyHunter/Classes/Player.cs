using System.Linq;

namespace EnergyHunter
{
    public class Player
    {
        AnimationPlayer animationPlayer;

        Animation runAnim;
        Animation stayAnim;
        Animation stayLeftAnim;

        //Animation jumpAnim;
        public bool isStayRight;

        public Vector2 position = new Vector2(50, 450);
        public Vector2 velocity;

        public Player()
        {

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
            position += velocity;
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

            if (velocity.X != 0)
                animationPlayer.PlayAnimation(runAnim);
            else if (velocity.X == 0 && isStayRight)
                animationPlayer.PlayAnimation(stayAnim);
            else if (velocity.X == 0 && !isStayRight)
                animationPlayer.PlayAnimation(stayLeftAnim);

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
    }
}
