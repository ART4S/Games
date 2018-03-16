using System.Drawing;

namespace AirForce
{
    public class ShootingFlyingObject : FlyingObject
    {
        private const int ShootingCooldownMaxValue = 80;
        private int shootingCooldown = ShootingCooldownMaxValue + 1;

        public ShootingFlyingObject(FlyingObjectType type, Point2D position, int radius, int movespeed, int strength, Image image)
            : base(type, position, radius, movespeed, strength, image)
        {
        }

        public bool CanMakeShoot(FlyingObject target)
        {
            if (CollisionHandler.IsInFront(target, this))
            {
                shootingCooldown++;

                if (shootingCooldown > ShootingCooldownMaxValue)
                {
                    shootingCooldown = 0;
                    return true;
                }
            }

            return false;
        }        
    }
}