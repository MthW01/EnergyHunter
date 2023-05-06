namespace EnergyHunter
{
    public class Tile : AnimatedSprite
    {
        public Tile(Sprite Parent) : base(Parent)
        {
            CanCollision = true;
        }
        public override void DoMove(float Delta)
        {
            base.DoMove(Delta);
            SetCollideRect(0, 0, Width, Height);
        }
    }
}
