namespace AirForce
{
    public interface IGameState
    {
        void Update(Point2D playerMovespeedModifer);
        void Restart();
        void PlayerFire();
        void BeginRewind();
        void EndRewind();
    }
}