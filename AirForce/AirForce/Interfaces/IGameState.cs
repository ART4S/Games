namespace AirForce
{
    public interface IGameState
    {
        void MovePlayer(Point2D movespeedModifer);
        void Restart();
        void PlayerFire();
    }
}