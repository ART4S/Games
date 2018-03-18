using System.Drawing;

namespace AirForce
{
    public class ShootingFlyingObject : FlyingObject
    {
        private readonly Coooldown shootingCooldown = new Coooldown(currentValue: 80, maxValue: 80);

        public ShootingFlyingObject(FlyingObjectType type, Point2D position, int radius, int movespeed, int strength, Image image)
            : base(type, position, radius, movespeed, strength, image)
        {
        }

        public bool CanShootToTarget(FlyingObject target)
        {
            if (CollisionHandler.IsInFront(self: target, other: this))
                return shootingCooldown.Tick();

            shootingCooldown.SetOnTick();
            return false;
        }        
    }
}