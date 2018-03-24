namespace AirForce
{
    public class ChangePositionCommand : IUndoCommand
    {
        private readonly FlyingObject source;
        private Point2D deltaPosition;

        public ChangePositionCommand(FlyingObject source)
        {
            this.source = source;
        }

        public void ShiftPostion(Point2D shift)
        {
            deltaPosition = shift;
            source.Position += deltaPosition;
        }

        public void Undo()
        {
            source.Position -= deltaPosition;
        }
    }
}