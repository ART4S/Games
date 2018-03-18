using System.Linq;

namespace AirForce
{
    public class PlayingGameState : IGameState
    {
        private readonly Game game;

        public PlayingGameState(Game game)
        {
            this.game = game;
        }

        public void EndRewind() { }
        public void Restart() { }

        public void Update()
        {
            foreach (FlyingObject obj in game.FlyingObjects)
                obj.SaveState();

            game.FlyingObjects.AddRange(game.CollisionHandler.GetNewEnemyBullets());

            foreach (FlyingObject obj in game.FlyingObjects)
                obj.Move(game.GameField, game.Ground, game.FlyingObjects);

            game.CollisionHandler.FindCollisionsAndChangeStrengths();

            game.DeadObjects.Push(game.FlyingObjects.FindAll(o => o.Strength <= 0));

            if (game.Player.Strength <= 0)
                game.GameState = new WaitingGameState(game);

            game.FlyingObjects.RemoveAll(o => o.Strength <= 0);
        }

        public void MovePlayer(Point2D movespeedModifer)
        {
            game.Player.MoveManyally(movespeedModifer, game.GameField, game.Ground);
        }

        public void PlayerFire()
        {
            game.FlyingObjects.Add(game.FlyingObjectsFactory.CreatePlayerBullet(game.GameField, game.Ground, game.Player));
        }

        public void AddNewRandomEnemy()
        {
            if (game.ObjectsForReleaseOnField.Any())
                game.FlyingObjects.AddRange(game.ObjectsForReleaseOnField.Pop());
            else
                game.FlyingObjects.Add(game.FlyingObjectsFactory.CreateRandomEnemy(game.GameField, game.Ground));
        }

        public void BeginRewind()
        {
            game.GameState = new RewindGameState(game);
        }
    }
}