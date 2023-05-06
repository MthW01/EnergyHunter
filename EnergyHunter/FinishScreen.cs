namespace EnergyHunter
{
    static class FinishScreen
    {
        public static Texture2D Background { get; set; }
        public static SpriteFont nameFont { get; set; }
        public static Texture2D sgButton { get; set; }

        static int timeCounter = 0;
        static Color color;

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, Vector2.Zero, color);
            //spriteBatch.Draw(sgButton, new Vector2(100, 400), color);

        }

        public static void Update()
        {

        }
    }
}