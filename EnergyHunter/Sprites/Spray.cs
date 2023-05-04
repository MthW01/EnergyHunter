namespace EnergyHunter.Sprites
{
    public class Spray : PlayerSprite
    {
        public Spray(Sprite Parent) : base(Parent)
        {
            SpriteSheetMode = SpriteSheetMode.NoneSingle;
        }
        public override void DoMove(float Delta)
        {
            base.DoMove(Delta);
            Accelerate();
            UpdatePos(Delta);
            Alpha -= 3;
            if (Alpha < 10)
                Dead();
        }
    }
}
