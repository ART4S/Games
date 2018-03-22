namespace AirForce
{
    public class ChangePositionCommand : IUndoCommand
    {
        private readonly FlyingObject flyingObject;
        private Point2D positionShift;

        public ChangePositionCommand(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public void ShiftPostion(Point2D shift)
        {
            positionShift = shift;
            flyingObject.Position += shift;
        }

        public void Undo()
        {
            flyingObject.Position -= positionShift;
        }
    }
}