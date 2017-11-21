using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirForce.AirObjects.Bullets;

namespace AirForce.AirObjects.EnemyAI
{
    public sealed class Meteor : EnemyAI
    {
        private int strength;

        public Meteor(Point2D position, Action<EnemyAI> objectDeathMethod) : base(position, 70, 3, Properties.Resources.meteor, objectDeathMethod)
        {
            strength = new Random().Next(5, 9);
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            if (otherAirObject is Bird)
                return;

            strength--;

            if (strength == 0)
                OnObjectDeathEvent(this);
        }

        public override void Move(Line groundLine)
        {
            Point2D nextPosition = new Point2D(Position.X - 2 * MovespeedShift, Position.Y + MovespeedShift);

            if (nextPosition.X + Radius < 0 || !IsNextPositionAboveGroundLine(nextPosition, groundLine))
                OnObjectDeathEvent(this);
            else
                Position = nextPosition;
        }
    }
}
