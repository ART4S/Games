using System;
using System.Collections.Generic;
using AirForce.AirObjects.Bullets;

namespace AirForce.AirObjects.EnemyAI
{
    public sealed class Meteor : EnemyAI
    {
        private int strength;

        public Meteor(Point2D position, int radius, int movespeedShift, Action<EnemyAI> objectDeathMethod)
            : base(position, radius, movespeedShift, Properties.Resources.meteor, objectDeathMethod)
        {
            strength = new Random().Next(5, 9);
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case PlayerShip _:
                case ChaserShip _:
                case BigShip _:
                case Bullet _:
                    strength--;
                    break;
            }

            if (strength == 0)
                OnObjectDeathEvent(this);
        }

        public override void Move(Line groundLine, HashSet<Bullet> bullets)
        {
            Point2D nextPosition = new Point2D(Position.X - 2 * MovespeedShift, Position.Y + MovespeedShift);

            if (IsPositionOutOfGameFieldLeftBorder() || !IsNextPositionAboveGroundLine(nextPosition, groundLine))
                OnObjectDeathEvent(this);
            else
                Position = nextPosition;
        }
    }
}
