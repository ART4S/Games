using System.Collections.Generic;
using System.Drawing;

namespace AirForce.AirObjects
{
    public sealed class BigShip : AirObject
    {
        public BigShip(Point2D position, int radius, int movespeedShift)
            : base(position, radius, movespeedShift, Properties.Resources.big_enemy_ship)
        {
            Durability = 3;
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            Position = new Point2D(Position.X - MovespeedShift, Position.Y);

            if (IsPositionOutOfGameFieldLeftBorder(Position))
                Durability = 0;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case Meteor _:
                case PlayerShip _:
                    Durability = 0;
                    break;

                case PlayerBullet _:
                    Durability--;
                    break;
            }
        }
    }
}
