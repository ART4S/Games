namespace AirForce
{
    public interface IManualMover
    {
        void MoveManually(Point2D movespeedModifer, Field gameField, Ground ground);
    }
}