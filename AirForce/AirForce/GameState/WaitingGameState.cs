namespace AirForce
{
    public class WaitingGameState : IGameState
    {
        private readonly GameController game;

        public WaitingGameState(GameController game)
        {
            this.game = game;
        }

        public void Restart()
        {
            game.FlyingObjects.Clear();
            game.Player = game.FlyingObjectsFactory.GetPlayerShip(game.GameField, game.Ground);
            game.GameState = new PlayingGameState(game);
        }

        public void MovePlayer(Point2D movespeedModifer) { }

        public void PlayerFire() { }
    }
}