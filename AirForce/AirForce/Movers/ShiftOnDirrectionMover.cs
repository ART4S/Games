using System.Collections.Generic;

namespace AirForce
{
    public class ShiftOnDirrectionMover : IMover
    {
        private readonly FlyingObject flyingObject;
        private readonly Point2D shiftOnDirrection;

        public ShiftOnDirrectionMover(FlyingObject flyingObject, Point2D shiftOnDirrection)
        {
            this.flyingObject = flyingObject;
            this.shiftOnDirrection = shiftOnDirrection;
        }

        public ChangePositionCommand Move(Field gameField, Ground ground, List<FlyingObject> objectsOnField)
        {
            var changePositionCommand = new ChangePositionCommand(flyingObject);
            Point2D shift = new Point2D(
                x: flyingObject.Movespeed * shiftOnDirrection.X,
                y: flyingObject.Movespeed * shiftOnDirrection.Y);

            changePositionCommand.ShiftPostion(shift);

            return changePositionCommand;
        }
    }
}