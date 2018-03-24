namespace AirForce
{
    public class WaitingGameState : IGameState
    {
        private readonly Game game;

        public WaitingGameState(Game game)
        {
            this.game = game;
        }

        public void Update(Point2D playerMovespeedModifer) { }
        public void PlayerFire() { }
        public void BeginRewind() { }
        public void EndRewind() { }

        public void Restart()
        {
            game.DeadObjects.Clear();
            game.ObjectsPendingReleaseOnField.Clear();
            game.ObjectsOnField.Clear();
            game.RewindMacroCommands.Clear();

            game.Player = game.FlyingObjectsFactory.CreatePlayerShip(game.Field, game.Ground);
            game.State = new PlayingGameState(game);
        }
    }
}