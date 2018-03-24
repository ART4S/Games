namespace AirForce
{
    public class ManualMover : IManualMover
    {
        private readonly FlyingObject source;

        public ManualMover(FlyingObject source)
        {
            this.source = source;
        }

        public void MoveManually(Point2D movespeedModifer, Field field, Ground ground, RewindMacroCommand rewindMacroCommand)
        {
            Point2D shift = new Point2D(
                x: source.Movespeed * movespeedModifer.X,
                y: source.Movespeed * movespeedModifer.Y);

            var shiftPositionCommand = new ChangePositionCommand(source);

            if (CollisionHandler.IsEntirelyOnField(source.Position + shift, source.Radius, field))
                shiftPositionCommand.ShiftPostion(shift);

            rewindMacroCommand.AddCommand(shiftPositionCommand);
        }
    }
}