using System;
using AirForce.Enums;

namespace AirForce.AirObjects.EnemyAI
{
    public sealed class Bird : EnemyAI
    {
        private readonly Random random = new Random();

        public Bird(Point2D position, Action<EnemyAI> objectDeathMethod)
            : base(position, 15, 1, Properties.Resources.bird, objectDeathMethod)
        {
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            if (otherAirObject is PlayerShip)
                OnObjectDeathEvent(this);
        }

        public override void Move(Line groundLine)
        {
            Direction newMovingDirection =
                FindMovingDirrection(groundLine);

            switch (newMovingDirection)
            {
                case Direction.Up:
                    Position = new Point2D(Position.X, Position.Y - MovespeedShift);
                    break;
                case Direction.Down:
                    Position = new Point2D(Position.X, Position.Y + MovespeedShift);
                    break;
                default:
                    throw new ArgumentException();
            }

            if (Position.X + Radius >= 0)
                Position = new Point2D(Position.X - MovespeedShift, Position.Y);
            else
                OnObjectDeathEvent(this);
        }

        private Direction FindMovingDirrection(Line groundLine)
        {
            int minFlyBorderY = 2 * Position.Y - groundLine.FirstPoint.Y;
            int maxFlyBorderY = groundLine.FirstPoint.Y;

            int birdTopBorderY = Position.Y - Radius;
            int birdBottomBorderY = Position.Y + Radius;

            while (true)
            {
                switch ((Direction)random.Next(0, 2))
                {
                    case Direction.Up:
                        if (birdTopBorderY - MovespeedShift > minFlyBorderY)
                            return Direction.Up;
                        break;

                    case Direction.Down:
                        if (birdBottomBorderY + MovespeedShift < maxFlyBorderY)
                            return Direction.Down;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
