using System.Drawing;
using System.Collections.Generic;

namespace AirForce.AirObjects
{
    public sealed class PlayerBullet : AirObject
    {
        public PlayerBullet(Point2D position, int radius, int movespeed)
            : base(position, radius, movespeed, Properties.Resources.player_bullet)
        {
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            Position += new Point2D(Movespeed, 0);

            if (IsPositionOutOfGameFieldRightBorder(Position, gameFieldSize))
                Durability = 0;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case BigShip _:
                case ChaserShip _:
                case Meteor _:
                    Durability = 0;
                    break;
            }
        }
    }
}
