using System;
using System.Drawing;
using System.Collections.Generic;

namespace AirForce.AirObjects
{
    public sealed class FlyingSaucer : AirObject
    {
        private readonly Random random = new Random();

        public FlyingSaucer(Point2D position, int radius, int movespeed)
            : base(position, radius, movespeed, Properties.Resources.flying_saucer)
        {
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            Point2D nextPosition;

            do
                nextPosition = new Point2D
                {
                    X = Position.X - Movespeed,
                    Y = Position.Y + Movespeed * random.Next(-1, 2) // random values: -1 0 1
                };
            while (IsPositionOutOfGroundLine(nextPosition, groundLine));

            Position = nextPosition;

            if (IsBodyOutOfGameFieldLeftBorder(Position))
                Durability = 0;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            if (otherAirObject is PlayerShip)
                Durability = 0;
        }
    }
}
