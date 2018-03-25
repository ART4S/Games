namespace AirForce
{
    public interface IGameState
    {
        void Update();
        void MovePlayer(Point2D movespeedModifer);
        void PlayerFire();
        void BeginRewind();
        void EndRewind();
    }
}