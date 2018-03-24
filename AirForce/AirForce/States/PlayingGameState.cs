using System.Collections.Generic;
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

        public void Update(Point2D playerMovespeedModifer)
        {
            var rewindMacroCommand = new RewindMacroCommand();

            game.ObjectsOnField.AddRange(game.CollisionHandler.GetNewEnemyBullets(rewindMacroCommand));
            MoveObjects(playerMovespeedModifer, rewindMacroCommand);
            game.CollisionHandler.FindCollisionsAndChangeStrengths(rewindMacroCommand);

            List<FlyingObject> deadObjects = game
                .ObjectsOnField
                .FindAll(o => o.Type != FlyingObjectType.PlayerShip && o.Strength <= 0);

            TransferFromFieldToDeadList(deadObjects);
            AddEnemyOnField(rewindMacroCommand);

            game.RewindMacroCommands.Add(rewindMacroCommand);

            if (IsGameOver())
                game.State = new WaitingGameState(game);
        }

        private void MoveObjects(Point2D playerMovespeedModifer, RewindMacroCommand rewindMacroCommand)
        {
            foreach (FlyingObject obj in game.ObjectsOnField)
                obj.Move(game.Field, game.Ground, game.ObjectsOnField, rewindMacroCommand);

            game.Player.MoveManyally(playerMovespeedModifer, game.Field, game.Ground, rewindMacroCommand);
        }

        private void TransferFromFieldToDeadList(List<FlyingObject> objects)
        {
            foreach (FlyingObject obj in objects)
            {
                game.ObjectsOnField.Remove(obj);
                game.DeadObjects.Add(obj);
            }
        }

        private void AddEnemyOnField(RewindMacroCommand rewindMacroCommand)
        {
            game.EnemiesCreatingCooldown.Tick(rewindMacroCommand);

            if (!game.EnemiesCreatingCooldown.IsCollapsed)
                return;

            if (game.ObjectsPendingReleaseOnField.Any())
            {
                game.ObjectsOnField.AddRange(game.ObjectsPendingReleaseOnField.Last());
                game.ObjectsPendingReleaseOnField.RemoveAt(game.ObjectsPendingReleaseOnField.Count - 1);
            }
            else
                game.ObjectsOnField.Add(game.FlyingObjectsFactory.CreateRandomEnemy(game.Field, game.Ground));
        }

        private bool IsGameOver()
        {
            return game.Player.Strength <= 0;
        }

        public void PlayerFire()
        {
            game.ObjectsOnField.Add(game.FlyingObjectsFactory.CreatePlayerBullet(game.Field, game.Ground, game.Player));
        }

        public void BeginRewind()
        {
            game.State = new RewindGameState(game);
        }
    }
}