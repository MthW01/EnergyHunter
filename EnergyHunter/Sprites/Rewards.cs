namespace EnergyHunter.Sprites
{
    public class Crystal : JumperSprite
    {
        public Crystal(Sprite parent) : base(parent)
        {
            SpriteSheetMode = SpriteSheetMode.NoneSingle;
        }
        public override void DoMove(float delta)
        {
            base.DoMove(delta);
            SetCollideRect(0, 0, 28, 30);
            Collision();
        }
        public override void OnCollision(Sprite sprite)
        {
            if (sprite is Tile)
            {
                var Tile = (Tile)sprite;
                if (Tile.ImageName == "Ground1.png" || Tile.ImageName == "Rock1.png" || Tile.Name == "Rock2.png")
                {
                    JumpState = JumpState.isNone;
                    Y = Tile.Y - 28;
                }
            }
        }
        public static void Create(float PosX, float PosY)
        {
            Random Random = new Random();
            if (Random.Next(0, 2) == 1)
            {
                var crystal = new Crystal(EngineFunc.SpriteEngine);
                crystal.Init(EngineFunc.ImageLib, "Crystal" + Random.Next(2, 4).ToString() + ".png", PosX, PosY, 5);
                crystal.CollideMode = CollideMode.Rect;
                crystal.CanCollision = true;
                crystal.DoJump = true;
            }
        }
    }
}
