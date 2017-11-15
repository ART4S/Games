using System.Drawing;

namespace AirForce.AirObjects
{
    public class PlayerShip : AirObject
    {
        public override int CollisionRadius { get; }

        public PlayerShip(Point positionInSpace) : base(positionInSpace)
        {
            CollisionRadius = 5;
        }

        public override void Move(Line trajectory)
        {

        }

        public override void BumpWithOtherAirObject()
        {

        }
    }
}
