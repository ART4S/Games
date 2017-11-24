using System.Collections.Generic;
using System.Drawing;

namespace AirForce.AirObjects
{
    public sealed class EnemyBullet : AirObject
    {
        public EnemyBullet(Point2D position, int radius, int movespeed)
            : base(position, radius, movespeed, Properties.Resources.enemy_bullet)
        {
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            Position -= new Point2D(Movespeed, 0);

            if (IsPositionOutOfGameFieldLeftBorder(Position))
                Durability = 0;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            if (otherAirObject is PlayerShip || otherAirObject is Meteor)
                Durability = 0;
        }
    }
}
