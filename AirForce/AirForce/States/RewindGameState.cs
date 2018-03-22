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
        public void MovePlayer(Point2D movespeedModifer) { }
        public void PlayerFire() { }
        public void BeginRewind() { }

        public void Update()
        {
            if (game.UndoActionsMacroCommands.Count == 0)
                return;

            UndoActionsMacroCommand undoActionsMacroCommand = game.UndoActionsMacroCommands.Last();
            game.UndoActionsMacroCommands.Remove(undoActionsMacroCommand);

            undoActionsMacroCommand.UndoActions();

            List<FlyingObject> risenObjects = game.DeadObjects.FindAll(o => o.Strength > 0);

            game.ObjectsOnField.AddRange(risenObjects);

            foreach (FlyingObject obj in risenObjects)
                game.DeadObjects.Remove(obj);

            List<FlyingObject> objectsOnStartPositions = game.ObjectsOnField.FindAll(o => o.Position == o.StartPosition);

            if (objectsOnStartPositions.Count == 0)
                return;

            List<FlyingObject> bulletsOnStartPositions = objectsOnStartPositions
                .FindAll(o => o.Type == FlyingObjectType.PlayerBullet || o.Type == FlyingObjectType.EnemyBullet);

            List<FlyingObject> newObjectsForReleaseOnField = objectsOnStartPositions
                .Except(bulletsOnStartPositions)
                .ToList();

            foreach (FlyingObject obj in objectsOnStartPositions)
                game.ObjectsOnField.Remove(obj);

            if (newObjectsForReleaseOnField.Any())
                game.ObjectsForReleaseOnField.Add(newObjectsForReleaseOnField);

            if (game.ObjectsOnField.All(o => o.Position == o.StartPosition))
                game.GameState = new WaitingGameState(game);
        }

        public void EndRewind()
        {
            game.GameState = new PlayingGameState(game);
        }
    }
}