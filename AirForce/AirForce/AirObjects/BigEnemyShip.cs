using System.Drawing;

namespace AirForce.AirObjects
{
    public class BigEnemyShip : AirObject
    {
        public BigEnemyShip(Point positionInSpace, int collisionRadius, int strength) : base(positionInSpace, collisionRadius, strength)
        {
        }

        public void Move()
        {
            PositionInSpace = new Point(PositionInSpace.X - 1, PositionInSpace.Y);
        }

        public override void BumpWithOtherAirObject(AirObject otherAirObject)
        {
            if (otherAirObject is PlayerShip)
                Strength = 0;
            else
                Strength--;
        }

        public void Shoot()
        {
        }
    }
}
