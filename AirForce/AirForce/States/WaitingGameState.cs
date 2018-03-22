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
        public void BeginRewind() { }
        public void EndRewind() { }

        public void Restart()
        {
            game.DeadObjects.Clear();
            game.ObjectsForReleaseOnField.Clear();
            game.ObjectsOnField.Clear();
            game.UndoActionsMacroCommands.Clear();

            game.Player = game.FlyingObjectsFactory.CreatePlayerShip(game.GameField, game.Ground);
            game.GameState = new PlayingGameState(game);
        }
    }
}