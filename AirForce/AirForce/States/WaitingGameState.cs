namespace AirForce
{
    public class WaitingGameState : IGameState
    {
        private readonly Game game;

        public WaitingGameState(Game game)
        {
            this.game = game;
        }

        public void Update() { }
        public void MovePlayer(Point2D movespeedModifer) { }
        public void PlayerFire() { }
        public void EndRewind() { }

        public void BeginRewind()
        {
            if (game.IsOver())
                game.State = new RewindGameState(game);
        }
    }
}