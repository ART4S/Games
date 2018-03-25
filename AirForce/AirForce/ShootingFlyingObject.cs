using System.Drawing;

namespace AirForce
{
    public class ShootingFlyingObject : FlyingObject
    {
        private readonly Coooldown shootingCooldown = new Coooldown(maxValue: 80, isCollapsed: true);

        public ShootingFlyingObject(FlyingObjectType type, Point2D position, int radius, int movespeed, int strength, int radiusOfSight, Image image)
            : base(type, position, radius, movespeed, strength, radiusOfSight, image)
        {
        }

        public bool CanShootToTarget(FlyingObject target, RewindMacroCommand rewindMacroCommand)
        {
            Circle2D thisCircle = this;

            if (!thisCircle.IsCircleInBack(target))
            {
                shootingCooldown.SetOneTickToCollapse(rewindMacroCommand);
                return false;
            }

            shootingCooldown.Tick(rewindMacroCommand);
            return shootingCooldown.IsCollapsed;
        }        
    }
}