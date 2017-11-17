using System.Drawing;

namespace AirForce.AirObjects
{
    public class EnemyShip : AirObject
    {
        public EnemyShip(Point positionInSpace, int collisionRadius, int strength) : base(positionInSpace, collisionRadius, strength)
        {
        }

        public void Move()
        {
        }

        public override void BumpWithOtherAirObject(AirObject otherAirObject)
        {
        }

        public void DodgeShipsBullet()
        {
            
        }
    }
}
