namespace EnergyHunter
{
    public class Enemy : JumperSprite
    {
        public Enemy(Sprite parent) : base(parent)
        {
            CanCollision = true;
            CollideMode = CollideMode.Rect;
            State = State.WalkLeft;
            JumpState = JumpState.isFalling;
        }
        public State State;
        public override void DoMove(float delta)
        {
            base.DoMove(delta);
            SetCollideRect(0, 0, Width, Height);

            if (ImageName == "Enemy2.png") SetCollideRect(31, 18, 71, 78);
            if (State != State.Die)
            {
                switch (State)
                {
                    case State.WalkLeft: X -= Speed * delta; SetAnim(ImageName, 0, AnimCount, AnimSpeed, true, false, true); break;
                    case State.WalkRight: X += Speed * delta; SetAnim(ImageName, 0, AnimCount, AnimSpeed, true, true, true); break;
                }
            }
            if (Y > 900)
                Dead();
            Collision();
        }
        public override void OnCollision(Sprite sprite)
        {
            if (sprite is Tile)
            {
                var Tile = (Tile)sprite;
                if (JumpState == JumpState.isFalling)
                {
                    if (Tile.ImageName == "Ground1.png" || Tile.ImageName == "Rock2.png")
                    {
                        JumpState = JumpState.isNone;
                        Y = Tile.Y - 40;
                        if (ImageName == "Enemy3.png")
                            Y = Tile.Y - 55;
                        if (ImageName == "Enemy2.png")
                            Y = Tile.Y - 75;
                        if (ImageName == "Enemy1.png")
                            Y = Tile.Y - 49;

                    }
                }
                if (Tile.ImageName == "Test1.png")
                {
                    if (State == State.WalkLeft)
                    {
                        X += 5;
                        State = State.WalkRight;
                    }
                    else
                    {
                        X -= 5;
                        State = State.WalkLeft;
                    }
                }
            }
        }
    }

}
