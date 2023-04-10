namespace EnergyHunter
{
    static class SplashScreen
    {
        public static Texture2D Background { get; set; }
        public static SpriteFont nameFont { get; set; }
        public static SpriteFont mmButtomsFont { get; set; }

        static int timeCounter = 0;
        static Color color;

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Background, Vector2.Zero, color);
            spriteBatch.DrawString(nameFont, "Energy  Hunter", new Vector2(100, 100), color);
            spriteBatch.DrawString(mmButtomsFont, "Начать игру", new Vector2(100, 450), color);
            spriteBatch.DrawString(mmButtomsFont, "Выйти", new Vector2(100, 600), color);

        }

        public static void Update()
        {
            color = Color.FromNonPremultiplied(255, 255, 255, timeCounter % 256);
            
            if (timeCounter < 255)
                timeCounter++;
        }
    }
}
