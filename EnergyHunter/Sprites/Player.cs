using EnergyHunter.Managers.SpriteEngine;

namespace EnergyHunter.Sprites
{
    public enum State
    {
        StandLeft, StandRight, WalkLeft, WalkRight, Die
    }

    public class Player : JumperSprite
    {
        public Player(Sprite parent) : base(parent)
        {
            CollideMode = CollideMode.Rect;
            CanCollision = true;
            SetPattern(110, 118);
            JumpSpeed = 1f;
            JumpHeight = 7.5f;
            MaxFallSpeed = 8;
            JumpState = JumpState.isJumping;
            State = State.StandRight;
        }

        bool isStayRight;
        bool isMoving;
        bool inWater = false;
        State State;
        public static int LeftEdge, RightEdge;
        public override void DoMove(float delta)
        {
            base.DoMove(delta);
            if (Y > 1200)
            {
                X = 100;
                Y = 300;
                JumpState = JumpState.isFalling;
                CanCollision = true;
                State = State.StandRight;
            }

            SetCollideRect(45, 45, 60, 110);
            if (Right + 3 < LeftEdge || Left > RightEdge + 1)
            {
                if (JumpState != JumpState.isJumping)
                    JumpState = JumpState.isFalling;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Right) && State != State.Die)
            {
                isMoving = true;
                isStayRight = true;
                State = State.WalkRight;
                if (inWater)
                    X += 2f * delta;
                else
                    X += 4f * delta;
                switch (JumpState)
                {
                    case JumpState.isNone: SetAnim("Walk.png", 0, 12, 0.4f, true, false, true); break;
                    case JumpState.isJumping: SetAnim("Jump.png", 0, 3, 0.06f, false, false, true); break;
                    case JumpState.isFalling: SetAnim("Jump.png", 2, 2, 0, false, false, true); break;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) && State != State.Die)
            {
                isMoving = true;
                isStayRight = false;
                State = State.WalkLeft;
                if (inWater)
                    X -= 2f * delta;
                else
                    X -= 4f * delta;
                switch (JumpState)
                {
                    case JumpState.isNone: SetAnim("Walk.png", 0, 12, 0.4f, true, true, true); break;
                    case JumpState.isJumping: SetAnim("Jump.png", 0, 3, 0.06f, false, true, true); break;
                    case JumpState.isFalling: SetAnim("Jump.png", 2, 2, 0, false, true, true); break;
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Left) && Keyboard.GetState().IsKeyUp(Keys.Right))
                isMoving = false;

            if (!isMoving && isStayRight && State != State.Die)
            {
                State = State.StandRight;
                if (JumpState == JumpState.isNone && State == State.StandRight)
                    SetAnim("Idle.png", 0, 12, 0.25f, true, false, true);
            }
            if (!isMoving && !isStayRight && State != State.Die)
            {
                State = State.StandLeft;
                if (JumpState == JumpState.isNone && State == State.StandLeft)
                    SetAnim("Idle.png", 0, 12, 0.25f, true, true, true);
            }

            if (JumpState == JumpState.isNone && State != State.Die)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    DoJump = true;
                    AnimPos = 0;
                    PatternIndex = 0;
                    switch (State)
                    {
                        case State.StandRight: SetAnim("Jump.png", 0, 3, 0.06f, true, false, true); break;
                        case State.StandLeft: SetAnim("Jump.png", 0, 3, 0.06f, true, true, true); break;
                    }
                }
            }
            if (Engine.Camera.X < X - 350)
                Engine.Camera.X = X - 350;

            if (Engine.Camera.X > X - 345)
                Engine.Camera.X = X - 345;


            Collision();
            Engine.Camera.X = X - 512;
        }

        public override void OnCollision(Sprite sprite)
        {
            if (sprite is Tile)
            {
                var Tile = (Tile)sprite;
                var ImageName = Tile.ImageName;
                if (ImageName == "Trap1.png")
                    if (Y + 80 > Tile.Top)
                    {
                        State = State.Die;
                        CanCollision = false;
                        JumpState = JumpState.isNone;
                        DoJump = true;
                        JumpHeight = 8;
                        JumpSpeed = 0.2f;
                        AnimPos = 0;
                        SetAnim("Dead.png", 0, 6, 0.1f, true, false, true);
                    }

                //only those tile can collision
                if (ImageName == "Ground1.png"
                 || ImageName == "Rock2.png"
                 || ImageName == "Rock1.png"
                 || ImageName == "Box1.png"
                 || ImageName == "Box2.png"
                 || ImageName == "Box3.png"
                 || ImageName == "Box4.png"
                 || ImageName == "Spring1.png")
                {
                    Tile.CanCollision = true;
                    Tile.Left = (int)Tile.X;
                    Tile.Top = (int)Tile.Y;
                    Tile.Right = (int)Tile.X + Tile.Width;
                    Tile.Bottom = (int)Tile.Y + Tile.Height;
                    LeftEdge = Tile.Left;
                    RightEdge = Tile.Right;
                }

                //Falling-- land at ground
                if (JumpState == JumpState.isFalling)
                {
                    if (ImageName == "Ground1.png"
                     || ImageName == "Rock1.png"
                     || ImageName == "Rock2.png"
                     || ImageName == "Box1.png"
                     || ImageName == "Box2.png"
                     || ImageName == "Box3.png"
                     || ImageName == "Box4.png")
                    {
                        inWater = false;
                        if (Right - 4 > Tile.Left
                        && Left + 3 < Tile.Right
                        && Bottom - 12 < Tile.Top)
                        {
                            JumpState = JumpState.isNone;
                            DoJump = false;
                            Y = Tile.Top - 102;
                            JumpSpeed = 0.3f;
                            JumpHeight = 16f;
                            MaxFallSpeed = 8.5f;
                            switch (State)
                            {
                                case State.StandLeft: SetAnim("Idle.png", 0, 12, 0.25f, true, true, true); break;
                                case State.StandRight: SetAnim("Idle.png", 0, 12, 0.25f, true, false, true); break;
                                case State.WalkLeft: SetAnim("Walk.png", 0, 12, 0.2f, true, true, true); break;
                                case State.WalkRight: SetAnim("Walk.png", 0, 12, 0.2f, true, false, true); break;
                            }
                        }
                    }
                }

                // jumping-- touch top tiles
                if (JumpState == JumpState.isJumping)
                {
                    if (ImageName == "Rock1.png"
                     || ImageName == "Rock2.png"
                     || ImageName == "Box1.png"
                     || ImageName == "Box2.png"
                     || ImageName == "Box3.png"
                     || ImageName == "Box4.png")
                    {
                        if (Right - 4 > Tile.Left
                         && Left + 3 < Tile.Right
                         && Top < Tile.Bottom - 5
                         && Bottom > Tile.Top + 8)
                        {
                            JumpState = JumpState.isFalling;
                            if (ImageName == "Box1.png"
                             || ImageName == "Box2.png"
                             || ImageName == "Box3.png"
                             || ImageName == "Box4.png")
                            {
                                Tile.Dead();
                                Map.SprayBox(Tile.X, Tile.Y);
                                Crystal.Create(Tile.X, Tile.Y);
                            }
                        }
                    }
                }
                //collision with tile
                if (ImageName == "Box1.png"
                 || ImageName == "Box2.png"
                 || ImageName == "Box3.png"
                 || ImageName == "Box4.png"
                 || ImageName == "Rock1.png"
                 || ImageName == "Rock2.png")
                {
                    if (State == State.WalkLeft)
                    {
                        if (Left + 8 > Tile.Right
                         && Top + 10 < Tile.Bottom
                         && Bottom - 8 > Tile.Top)
                            X = Tile.X + (Tile.Width - 45) - 3;
                    }
                    if (State == State.WalkRight)
                    {
                        if (Right - 8 < Tile.Left
                         && Top + 10 < Tile.Bottom
                         && Bottom - 8 > Tile.Top)
                            X = Tile.X - (PatternWidth - 45) + 6;
                    }
                }

                if (ImageName == "Water1.png"
                 || ImageName == "Water2.png"
                 || ImageName == "Water3.png")
                {
                    if (State == State.WalkLeft)
                    {
                        if (Left + 8 > Tile.Right
                         || Top + 10 < Tile.Bottom
                         || Bottom - 8 > Tile.Top)
                            inWater = true;
                        else if (Left + 8 <= Tile.Right
                            || Top + 10 >= Tile.Bottom
                            || Bottom - 8 <= Tile.Top)
                            inWater = false;
                    }
                    if (State == State.WalkRight)
                    {
                        if (Right - 8 < Tile.Left
                         || Top + 10 < Tile.Bottom
                         || Bottom - 8 > Tile.Top)
                            inWater = true;
                        else if (Right - 8 >= Tile.Left
                            || Top + 10 >= Tile.Bottom
                            || Bottom - 8 <= Tile.Top)
                            inWater = false;

                    }
                }


                //get fruit
                if (ImageName == "Crystal1.png" || ImageName == "Crystal4.png" || ImageName == "Crystal3.png" || ImageName == "Crystal2.png")
                {
                    Tile.Dead();
                    Map.SprayCrystal(Tile.X + 10, Tile.Y + 10);
                }
                //when falling and touch spring
                if (ImageName == "Spring1.png" && JumpState == JumpState.isFalling)
                {
                    Y = Tile.Top - 85;
                    JumpState = JumpState.isNone;
                    DoJump = true;
                    JumpSpeed = 0.2f;
                    JumpHeight = 22;
                    MaxFallSpeed = 8.5f;
                    AnimPos = 0;
                    PatternIndex = 0;
                    switch (State)
                    {
                        case State.WalkLeft: SetAnim("Jump.png", 0, 3, 0.06f, false, false, true); break;
                        case State.WalkRight: SetAnim("Jump.png", 0, 3, 0.06f, false, true, true); break;
                    }
                    Tile.AnimPos = 0;
                    Tile.PatternIndex = 0;
                    Tile.SetAnim("Spring1.png", 0, 6, 0.2f, false, false, true);
                }
            }
            // get green Apple
            if (sprite is Crystal)
            {
                if (((Crystal)sprite).JumpState == JumpState.isNone)
                    ((Crystal)sprite).Dead();
            }
            // jump fall and kill enemy
            if (sprite is Enemy)
            {
                var Enemy = (Enemy)sprite;

                if (Y + 80 > Enemy.Y)
                {
                    if (JumpState == JumpState.isNone || JumpState == JumpState.isJumping)
                    {

                        State = State.Die;
                        CanCollision = false;
                        DoJump = true;
                        JumpHeight = 8;
                        JumpSpeed = 0.2f;
                        AnimPos = 0;
                        SetAnim("Dead.png", 0, 6, 0.1f, true, false, true);
                    }
                }

                if (JumpState == JumpState.isFalling)
                {
                    JumpState = JumpState.isNone;
                    JumpSpeed = 0.1f;
                    JumpHeight = 7;
                    JumpSpeed = 0.2f;
                    DoJump = true;
                    Enemy.State = State.Die;
                    Enemy.CanCollision = false;
                    Enemy.DoAnimate = false;
                    Enemy.DoJump = true;
                    Enemy.FlipY = true;
                    Enemy.MaxFallSpeed = 7;
                    Enemy.JumpHeight = 7;
                    //SetAnim("Dead.png", 0, 6, 0.1f, true, false, true);
                }
            }
        }
    }
}
