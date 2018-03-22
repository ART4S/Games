namespace AirForce
{
    public interface IManualMover
    {
        ChangePositionCommand MoveManually(Point2D movespeedModifer, Field gameField, Ground ground);
    }
}