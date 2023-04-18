namespace EnergyHunter
{
    class Bullets
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 velocity;
        Vector2 origin;
        public bool isVisible;

        public Bullets(Texture2D newTexture)
        {
            texture = newTexture;
            isVisible = false;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, 1f, SpriteEffects.None, 0);
        }
    }
}
