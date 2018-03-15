namespace AirForce
{
    public class PlayingGameState : IGameState
    {
        private readonly GameController game;

        public PlayingGameState(GameController game)
        {
            this.game = game;
        }

        public void MovePlayer(Point2D movespeedModifer)
        {
            game.Player.MoveManyally(movespeedModifer, game.GameField, game.Ground);
        }

        public void PlayerFire()
        {
            game.FlyingObjects.Add(game.FlyingObjectsFactory.GetPlayerBullet(game.GameField, game.Ground, game.Player));
        }

        public void Restart() { }
    }
}