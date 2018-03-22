
namespace AirForce
{
    public class ManualMover : IManualMover
    {
        private readonly FlyingObject flyingObject;

        public ManualMover(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public ChangePositionCommand MoveManually(Point2D movespeedModifer, Field gameField, Ground ground)
        {
            var changePositionCommand = new ChangePositionCommand(flyingObject);
            Point2D shift = new Point2D(
                x: flyingObject.Movespeed * movespeedModifer.X,
                y: flyingObject.Movespeed * movespeedModifer.Y);

            if (CollisionHandler.IsEntirelyOnField(flyingObject.Position + shift, flyingObject.Radius, gameField))
                changePositionCommand.ShiftPostion(shift);

            return changePositionCommand;
        }
    }
}