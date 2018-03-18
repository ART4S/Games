using System.Collections.Generic;

namespace AirForce
{
    public class ShiftOnDirrectionMover : IMover
    {
        private readonly FlyingObject flyingObject;
        private readonly Point2D shift;

        public ShiftOnDirrectionMover(FlyingObject flyingObject, Point2D shift)
        {
            this.flyingObject = flyingObject;
            this.shift = shift;
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects)
        {
            flyingObject.Position += new Point2D(flyingObject.Movespeed * shift.X, flyingObject.Movespeed * shift.Y);
        }
    }
}