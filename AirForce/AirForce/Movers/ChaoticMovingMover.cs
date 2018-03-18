using System;
using System.Collections.Generic;

namespace AirForce
{
    public class ChaoticMovingMover : IMover
    {
        private readonly FlyingObject flyingObject;
        private readonly Random random = new Random();

        public ChaoticMovingMover(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects)
        {
            Point2D newPosition;
            do
            {
                newPosition = flyingObject.Position + new Point2D(
                                  x: -flyingObject.Movespeed,
                                  y: +flyingObject.Movespeed * random.Next(-1, 2)); // random values: -1 0 1
            }
            while (CollisionHandler.IsIntersectGround(newPosition, flyingObject.Radius, ground));

            flyingObject.Position = newPosition;
        }
    }
}