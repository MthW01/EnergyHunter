namespace EnergyHunter
{
    public class Background : BackgroundSprite
    {
        public Background(Sprite Parent) : base(Parent)
        {
            SpriteSheetMode = SpriteSheetMode.NoneSingle;
            TileMode = TileMode.Horizontal;
        }

        public int Layer;
        public override void DoMove(float Delta)
        {
            base.DoMove(Delta);
            switch (Layer)
            {
                case 1: X = EngineFunc.SpriteEngine.Camera.X * 0.5f; break;
                case 2: X = EngineFunc.SpriteEngine.Camera.X * 0.3f; break;
                case 3: X = EngineFunc.SpriteEngine.Camera.X * 0.1f; break;
            }
        }
    }
}
