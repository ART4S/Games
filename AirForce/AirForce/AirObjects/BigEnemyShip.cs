using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public sealed class BigEnemyShip : EnemyAirObject
    {
        public BigEnemyShip(Point positionInSpace, Action<AirObject> deathObjectMethod)
            : base(positionInSpace, 50, Program.Random.Next(5, 9), 2, Properties.Resources.bomber_ship, deathObjectMethod)
        {
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            Strength--;

            if (Strength == 0)
                OnDeathObjectEvent();
        }

        public override void Move(Line groundLine)
        {
            if (PositionInSpace.X + Radius >= 0)
                PositionInSpace = new Point(PositionInSpace.X - MovespeedShift, PositionInSpace.Y);
            else
                OnDeathObjectEvent();
        }
    }
}
