using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public sealed class BigEnemyShip : EnemyAirObject
    {
        public BigEnemyShip(Point positionInSpace, Action<EnemyAirObject> deathObjectMethod) : base(positionInSpace, 50, Program.Random.Next(5, 9), 2, Properties.Resources.bomber_ship, deathObjectMethod)
        {
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case PlayerShip _:
                    OnDeathThisObjectEvent(this);
                    break;

                case Bullet _:
                    break;
            }
        }

        public override void Move(Line groundLine)
        {
            if (PositionInSpace.X + Radius >= 0) // 0
                PositionInSpace = new Point(PositionInSpace.X - MovespeedShift, PositionInSpace.Y);
            else
                OnDeathThisObjectEvent(this);
        }
    }
}
