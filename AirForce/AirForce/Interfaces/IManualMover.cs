namespace AirForce
{
    public interface IManualMover
    {
        void MoveManually(Point2D movespeedModifer, Field field, Ground ground, RewindMacroCommand rewindMacroCommand);
    }
}