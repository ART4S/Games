using System;
using System.Collections.Generic;

namespace AirForce
{
    public class FlyingSaucerMover : IMover
    {
        private readonly FlyingObject flyingSaucer;
        private readonly Random random = new Random();

        public FlyingSaucerMover(FlyingObject flyingSaucer)
        {
            this.flyingSaucer = flyingSaucer;
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects = null)
        {
            Point2D newPosition;
            do
            {
                 newPosition = flyingSaucer.Position + new Point2D(
                     x: -flyingSaucer.Movespeed,
                     y: +flyingSaucer.Movespeed * random.Next(-1, 2)); // random values: -1 0 1
            }
            while (CollisionHandler.IsIntersectGround(newPosition, flyingSaucer.Radius, ground));

            flyingSaucer.Position = newPosition;
        }

        public void UndoMove()
        {
            throw new NotImplementedException();
        }
    }
}