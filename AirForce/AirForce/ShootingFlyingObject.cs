using System.Drawing;

namespace AirForce
{
    public class ShootingFlyingObject : FlyingObject
    {
        private readonly Coooldown shootingCooldown = new Coooldown(maxValue: 80, isElapsed: true);

        public ShootingFlyingObject(FlyingObjectType type, Point2D position, int radius, int movespeed, int strength, int radiusOfSight, Image image)
            : base(type, position, radius, movespeed, strength, radiusOfSight, image)
        {
        }

        public bool CanShootToTarget(FlyingObject target, RewindMacroCommand rewindMacroCommand)
        {
            Circle2D thisCircle = ToCircle2D();
            Circle2D targetCircle = target.ToCircle2D();

            if (!thisCircle.IsCircleInBack(targetCircle))
            {
                shootingCooldown.SetOneTickToElapse(rewindMacroCommand);
                return false;
            }

            shootingCooldown.Tick(rewindMacroCommand);
            return shootingCooldown.IsElapsed;
        }        
    }
}