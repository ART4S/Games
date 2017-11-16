using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public abstract class AirObject
    {
        public Point PositionInSpace { get; protected set; }
        public int CollisionRadius { get; }

        protected AirObject(Point positionInSpace, int collisionRadius)
        {
            PositionInSpace = positionInSpace;
            CollisionRadius = collisionRadius;
        }

        public abstract void BumpWithOtherAirObject(AirObject otherAirObject);
    }
}
