using System;

namespace AirForce.AirObjects.EnemyAI
{
    public sealed class Bird : EnemyAI
    {
        private readonly Random random = new Random();

        public Bird(Point2D position, int radius, int movespeedShift, Action<EnemyAI> objectDeathMethod)
            : base(position, radius, movespeedShift, Properties.Resources.bird, objectDeathMethod)
        {
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            if (otherAirObject is PlayerShip)
                OnObjectDeathEvent(this);
        }

        public override void Move(Line groundLine)
        {
            Point2D nextPosition;

            do
                nextPosition = new Point2D
                {
                    X = Position.X,
                    Y = Position.Y + MovespeedShift * random.Next(-1, 2) // random values: -1 0 1
                };
            while (!IsNextPositionAboveGroundLine(nextPosition, groundLine));

            Position = nextPosition;

            if (IsPositionBehindGameFieldLeftBorder())
                OnObjectDeathEvent(this);
            else
                Position = new Point2D(Position.X - MovespeedShift, Position.Y);
        }
    }
}
