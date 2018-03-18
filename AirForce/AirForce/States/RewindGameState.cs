using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    public class RewindGameState : IGameState
    {
        private readonly Game game;

        public RewindGameState(Game game)
        {
            this.game = game;
        }

        public void Restart() { }
        public void AddNewRandomEnemy() { }
        public void MovePlayer(Point2D movespeedModifer) { }
        public void PlayerFire() { }
        public void BeginRewind() { }

        public void Update()
        {
            if (game.DeadObjects.Any())
                game.FlyingObjects.AddRange(game.DeadObjects.Pop());

            foreach (FlyingObject obj in game.FlyingObjects)
                obj.RestorePreviousState();

            List<FlyingObject> objectsOnStartPositions = game.FlyingObjects
                .FindAll(o => o.Type != FlyingObjectType.PlayerShip && !o.CanRestorePreviousState());

            if (objectsOnStartPositions.Any())
            {
                List<FlyingObject> bulletsOnStartPositions = objectsOnStartPositions
                    .FindAll(o => o.Type == FlyingObjectType.PlayerBullet || o.Type == FlyingObjectType.EnemyBullet);

                List<FlyingObject> newObjectsForReleaseOnField = objectsOnStartPositions
                        .Except(bulletsOnStartPositions)
                        .ToList();

                if (newObjectsForReleaseOnField.Any())
                    game.ObjectsForReleaseOnField.Push(newObjectsForReleaseOnField);

                foreach (FlyingObject obj in objectsOnStartPositions)
                    game.FlyingObjects.Remove(obj);
            }

            if (game.FlyingObjects.All(o => !o.CanRestorePreviousState()))
                game.GameState = new WaitingGameState(game);
        }

        public void EndRewind()
        {
            game.GameState = new PlayingGameState(game);
        }
    }
}