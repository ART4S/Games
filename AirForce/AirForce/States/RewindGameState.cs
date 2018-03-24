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

            List<FlyingObject> risenObjects = game.DeadObjects.FindAll(o => o.Strength > 0);

            TransferFromDeadListToField(risenObjects);

            List<FlyingObject> objectsOnStartPositions = game.ObjectsOnField
                .FindAll(o => o.Position == o.StartPosition && o.Type != FlyingObjectType.PlayerShip);

            TransferFromFieldToPendingRleaseList(objectsOnStartPositions);
        }

        private void UndoLastMacroCommand()
        {
            RewindMacroCommand rewindMacroCommand = game.RewindMacroCommands.Last();
            rewindMacroCommand.Undo();
            game.RewindMacroCommands.Remove(rewindMacroCommand);
        }

        private void TransferFromDeadListToField(List<FlyingObject> objects)
        {
            game.ObjectsOnField.AddRange(objects);
            game.DeadObjects.RemoveAll(objects.Contains);
        }

        private void TransferFromFieldToPendingRleaseList(List<FlyingObject> objects)
        {
            List<FlyingObject> newObjectsForReleaseOnField = GetNewObjectsForReleaseOnField(objects);

            if (newObjectsForReleaseOnField.Any())
                game.ObjectsPendingReleaseOnField.Add(newObjectsForReleaseOnField);

            game.ObjectsOnField.RemoveAll(objects.Contains);
        }

        private List<FlyingObject> GetNewObjectsForReleaseOnField(List<FlyingObject> objects)
        {
            if (objects.Count == 0)
                return new List<FlyingObject>();

            List<FlyingObject> bullets = objects
                .FindAll(o => o.Type == FlyingObjectType.PlayerBullet || o.Type == FlyingObjectType.EnemyBullet);

            List<FlyingObject> newObjectsForReleaseOnField = objects
                .Except(bullets)
                .ToList();

            return newObjectsForReleaseOnField;
        }

        public void EndRewind()
        {
            game.State = new PlayingGameState(game);
        }
    }
}