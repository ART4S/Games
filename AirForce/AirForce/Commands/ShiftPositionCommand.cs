namespace AirForce
{
    public class ShiftPositionCommand : ICommand
    {
        private readonly FlyingObject source;
        private readonly Point2D deltaPosition;

        public ShiftPositionCommand(FlyingObject source, Point2D deltaPosition)
        {
            this.source = source;
            this.deltaPosition = deltaPosition;
        }

        public void Execute()
        {
            source.Position += deltaPosition;
        }

        public void Undo()
        {
            source.Position -= deltaPosition;
        }
    }
}