namespace AirForce
{
    public interface IGameState
    {
        void MovePlayer(Point2D movespeedModifer);
        void Restart();
        void Update();
        void PlayerFire();
        void AddNewRandomEnemy();
        void BeginRewind();
        void EndRewind();
    }
}