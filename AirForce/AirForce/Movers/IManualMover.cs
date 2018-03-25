namespace AirForce
{
    public interface IManualMover
    {
        void MoveManually(Point2D movespeedModifer, Rectangle2D field, Rectangle2D ground, RewindMacroCommand rewindMacroCommand);
    }
}