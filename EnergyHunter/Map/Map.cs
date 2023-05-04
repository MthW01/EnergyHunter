using System.Text.RegularExpressions;

namespace EnergyHunter.Map
{
    class Map
    {
        public static void SprayBox(float PosX, float PosY)
        {
            Random Random = new Random();
            float Rnd()
            {
                return Random.Next(0, 100) * 0.01f;
            }

            for (int i = 0; i <= 6; i++)
            {
                var Particle = new ParticleSprite(EngineFunc.SpriteEngine);
                Particle.Init(EngineFunc.ImageLib, "Star.png", PosX + Random.Next(0, 21), PosY + Random.Next(0, 21), 20, 13, 14);
                Particle.SpriteSheetMode = SpriteSheetMode.NoneSingle;
                Particle.LifeTime = 150;
                Particle.Decay = 1;
                Particle.ScaleX = 1.2f;
                Particle.ScaleY = 1.2f;
                Particle.UpdateSpeed = 0.5f;
                Particle.VelocityX = -4 + Rnd() * 8;
                Particle.VelocityY = -Rnd() * 7;
                Particle.AccelX = 0;
                Particle.AccelY = 0.2f + Rnd() / 2;
            }
        }
        public static void SprayCrystal(float PosX, float PosY)
        {
            for (int i = 0; i <= 15; i++)
            {
                var Spray = new Spray(EngineFunc.SpriteEngine);
                Spray.Init(EngineFunc.ImageLib, "flare.png", PosX, PosY, 20);
                Spray.ScaleX = 0.1f;
                Spray.ScaleY = 0.1f;
                Spray.Acceleration = 0.08f;
                Spray.MinSpeed = 0.8f;
                Spray.MaxSpeed = 1;
                Spray.Direction = i * 16;
                Spray.BlendMode = BlendMode.AddtiveColor;
            }
        }
        public static void CreateMap()
        {
            for (int i = 3; i >= 1; i--)
            {
                var Background = new Background(EngineFunc.BackgroundEngine);
                Background.Init(EngineFunc.ImageLib, "back" + i.ToString() + ".png", 0, 0, -100, 0, 0, 1024, 240);
                Background.Layer = i;
                switch (i)
                {
                    case 1: Background.Y = -450; break;
                    case 2: Background.Y = -400; break;
                    case 3: Background.Y = 0; Background.SetSize(1600, 240); Background.Offset.X = 500; break;
                }
            }

            var Player = new Player(EngineFunc.SpriteEngine);
            Player.Init(EngineFunc.ImageLib, "Idle.png", 800, 400, 100);

            string AllText = System.IO.File.ReadAllText("Map1.txt");
            string[] Section = AllText.Split('/');
            int Length = Section.Length;

            for (int i = Length - 2; i > 0; i--)
            {
                var Str = Section[i].Split(',');
                int X = int.Parse(Regex.Replace(Str[0], @"\D", ""));
                int Y = int.Parse(Regex.Replace(Str[1], @"\D", ""));
                string ImageName = Regex.Replace(Str[2], "ImageName=", "").Trim();
                //create Map tile
                if (ImageName != "Enemy1.png" && ImageName != "Enemy2.png" && ImageName != "Enemy3.png")
                {
                    var Tile = new Tile(EngineFunc.SpriteEngine);
                    Tile.Init(EngineFunc.ImageLib, ImageName, X - 540, Y - 20, -10);
                    Tile.SetSize(Tile.ImageWidth, Tile.ImageHeight);
                    Tile.SetPattern(Tile.Width, Tile.Height);
                    if (ImageName == "Spring1.png")
                    {
                        Tile.SetPattern(48, 48);
                        Tile.SetSize(48, 48);
                    }
                }
                //Create Enemy
                else
                {
                    var Enemy = new Enemy(EngineFunc.SpriteEngine);
                    Enemy.Init(EngineFunc.ImageLib, ImageName, X - 540, Y - 10, 150);
                    switch (ImageName)
                    {
                        case "Enemy1.png": Enemy.SetSize(81, 44); Enemy.AnimCount = 4; Enemy.AnimSpeed = 0.25f; Enemy.Speed = 1.7f; break;
                        case "Enemy2.png": Enemy.SetSize(75, 83); Enemy.AnimCount = 6; Enemy.AnimSpeed = 0.27f; Enemy.Speed = 1.7f; break;
                        case "Enemy3.png": Enemy.SetSize(57, 58); Enemy.AnimCount = 4; Enemy.AnimSpeed = 0.18f; Enemy.Speed = 1.4f; break;
                    }
                    Enemy.SetPattern(Enemy.Width, Enemy.Height);
                }
            }
        }
    }
}
