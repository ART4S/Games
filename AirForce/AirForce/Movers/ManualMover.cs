namespace AirForce
{
    public class ManualMover : IManualMover
    {
        private readonly FlyingObject source;

        public ManualMover(FlyingObject source)
        {
            this.source = source;
        }

        public void MoveManually(Point2D movespeedModifer, Rectangle2D field, Rectangle2D ground, RewindMacroCommand rewindMacroCommand)
        {
            var shift = new Point2D(
                x: source.Movespeed * movespeedModifer.X,
                y: source.Movespeed * movespeedModifer.Y);

            var newObject = new Circle2D(source.Position + shift, source.Radius);

            if (field.IsContains(newObject))
                rewindMacroCommand.AddAndExecute(new ShiftPositionCommand(source, shift));
        }
    }
}