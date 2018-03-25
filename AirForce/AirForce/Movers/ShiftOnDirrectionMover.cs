using System.Collections.Generic;

namespace AirForce
{
    public class ShiftOnDirrectionMover : IMover
    {
        private readonly FlyingObject source;
        private readonly Point2D shiftVector;

        public ShiftOnDirrectionMover(FlyingObject source, Point2D shiftVector)
        {
            this.source = source;
            this.shiftVector = shiftVector;
        }

        public void Move(Rectangle2D field, Rectangle2D ground, List<FlyingObject> objectsOnField, RewindMacroCommand rewindMacroCommand)
        {
            Point2D shift = new Point2D(
                x: source.Movespeed * shiftVector.X,
                y: source.Movespeed * shiftVector.Y);

            rewindMacroCommand.AddAndExecute(new ShiftPositionCommand(source, shift));
        }
    }
}