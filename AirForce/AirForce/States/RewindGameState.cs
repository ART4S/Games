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
        public void PlayerFire() { }
        public void BeginRewind() { }

        public void Update(Point2D playerMovespeedModifer)
        {
            if (game.RewindMacroCommands.Count == 0)
            {
                game.State = new WaitingGameState(game);
                return;
            }

            RewindMacroCommand rewindMacroCommand = game.RewindMacroCommands.Last();
            rewindMacroCommand.UndoActions();
            game.RewindMacroCommands.Remove(rewindMacroCommand);

            List<FlyingObject> risenObjects = game.DeadObjects.FindAll(o => o.Strength > 0);

            game.ObjectsOnField.AddRange(risenObjects);
            game.DeadObjects.RemoveAll(o => risenObjects.Contains(o));

            List<FlyingObject> objectsOnStartPositions = game.ObjectsOnField
                .FindAll(o => o.Position == o.StartPosition && o.Type != FlyingObjectType.PlayerShip);

            if (objectsOnStartPositions.Count == 0)
                return;

            List<FlyingObject> bulletsOnStartPositions = objectsOnStartPositions
                .FindAll(o => o.Type == FlyingObjectType.PlayerBullet || o.Type == FlyingObjectType.EnemyBullet);

            List<FlyingObject> newObjectsForReleaseOnField = objectsOnStartPositions
                .Except(bulletsOnStartPositions)
                .ToList();

            game.ObjectsOnField.RemoveAll(o => objectsOnStartPositions.Contains(o));

            if (newObjectsForReleaseOnField.Any())
                game.ObjectsPendingReleaseOnField.Add(newObjectsForReleaseOnField);
        }

        public void EndRewind()
        {
            game.State = new PlayingGameState(game);
        }
    }
}