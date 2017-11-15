using System.Drawing;

namespace AirForce.AirObjects
{
    public abstract class AirObject
    {
        protected Point PositionInSpace;
        public abstract int CollisionRadius { get; }

        protected AirObject(Point positionInSpace)
        {
            PositionInSpace = positionInSpace; //
        }

        public abstract void Move(Line trajectory);
        public abstract void BumpWithOtherAirObject();
    }
}
