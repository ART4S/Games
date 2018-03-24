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

            UndoLastMacroCommand();
            AddRisenObjectsOnFieldAndRemoveFromDead();

            List<FlyingObject> objectsOnStartPositions = game.ObjectsOnField
                .FindAll(o => o.Position == o.StartPosition && o.Type != FlyingObjectType.PlayerShip);

            List<FlyingObject> newObjectsForReleaseOnField = GetNewObjectsForReleaseOnField(objectsOnStartPositions);

            game.ObjectsOnField.RemoveAll(objectsOnStartPositions.Contains);

            if (newObjectsForReleaseOnField.Any())
                game.ObjectsPendingReleaseOnField.Add(newObjectsForReleaseOnField);
        }

        private void UndoLastMacroCommand()
        {
            RewindMacroCommand rewindMacroCommand = game.RewindMacroCommands.Last();
            rewindMacroCommand.Undo();
            game.RewindMacroCommands.Remove(rewindMacroCommand);
        }

        private void AddRisenObjectsOnFieldAndRemoveFromDead()
        {
            List<FlyingObject> risenObjects = game.DeadObjects.FindAll(o => o.Strength > 0);

            game.ObjectsOnField.AddRange(risenObjects);
            game.DeadObjects.RemoveAll(risenObjects.Contains);
        }

        private List<FlyingObject> GetNewObjectsForReleaseOnField(List<FlyingObject> objectsOnStartPositions)
        {
            if (objectsOnStartPositions.Count == 0)
                return new List<FlyingObject>();

            List<FlyingObject> bulletsOnStartPositions = objectsOnStartPositions
                .FindAll(o => o.Type == FlyingObjectType.PlayerBullet || o.Type == FlyingObjectType.EnemyBullet);

            List<FlyingObject> newObjectsForReleaseOnField = objectsOnStartPositions
                .Except(bulletsOnStartPositions)
                .ToList();

            return newObjectsForReleaseOnField;
        }

        public void EndRewind()
        {
            game.State = new PlayingGameState(game);
        }
    }
}