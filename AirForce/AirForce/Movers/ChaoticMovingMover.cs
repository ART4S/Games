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

        public ChangePositionCommand Move(Field gameField, Ground ground, List<FlyingObject> objectsOnField)
        {
            var changePositionCommand = new ChangePositionCommand(flyingObject);
            Point2D shift;

            do
            {
                shift = new Point2D(
                    x: -flyingObject.Movespeed,
                    y: +flyingObject.Movespeed * random.Next(-1, 2)); // random values: -1 0 1
            }
            while (CollisionHandler.IsIntersectGround(flyingObject.Position + shift, flyingObject.Radius, ground));

            changePositionCommand.ShiftPostion(shift);

            return changePositionCommand;
        }
    }
}