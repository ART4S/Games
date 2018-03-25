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
            Point2D shift = new Point2D(
                x: source.Movespeed * movespeedModifer.X,
                y: source.Movespeed * movespeedModifer.Y);

            if (CollisionHandler.IsEntirelyOnField(source.Position + shift, source.Radius, field))
                rewindMacroCommand.AddAndExecute(new ShiftPositionCommand(source, shift));
        }
    }
}