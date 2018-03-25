using System.Collections.Generic;
using System.Linq;
using AirForce.Commands;

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

        public void Update()
        {
            var rewindMacroCommand = new RewindMacroCommand();

            AddEnemyBullets(rewindMacroCommand);
            MoveObjects(rewindMacroCommand);
            FindCollisionsAndRemoveDeadObjects(rewindMacroCommand);
            AddEnemy(rewindMacroCommand);

            game.RewindMacroCommands.Add(rewindMacroCommand);

            if (game.IsOver())
                game.State = new WaitingGameState(game);
        }

        private void AddEnemyBullets(RewindMacroCommand rewindMacroCommand)
        {
            List<FlyingObject> newEnemyBullets = game.CollisionHandler.GetNewEnemyBullets(rewindMacroCommand);

            foreach (FlyingObject bullet in newEnemyBullets)
                rewindMacroCommand.AddAndExecute(new AddItemToListCommand(bullet, game.ObjectsOnField));
        }

        private void MoveObjects(RewindMacroCommand rewindMacroCommand)
        {
            foreach (FlyingObject obj in game.ObjectsOnField)
                obj.Move(game.Field, game.Ground, game.ObjectsOnField, rewindMacroCommand);
        }

        private void FindCollisionsAndRemoveDeadObjects(RewindMacroCommand rewindMacroCommand)
        {
            game.CollisionHandler.FindCollisionsAndChangeStrengths(rewindMacroCommand);

            List<FlyingObject> deadObjects = game.ObjectsOnField.FindAll(o => o.Strength <= 0);

            foreach (FlyingObject obj in deadObjects)
                rewindMacroCommand.AddAndExecute(new RemoveItemFromListCommand(obj, game.ObjectsOnField));
        }

        private void AddEnemy(RewindMacroCommand rewindMacroCommand)
        {
            game.EnemiesCreatingCooldown.Tick(rewindMacroCommand);

            if (!game.EnemiesCreatingCooldown.IsCollapsed)
                return;

            FlyingObject enemy = game.FlyingObjectsFactory.CreateRandomEnemy(game.Field, game.Ground);

            rewindMacroCommand.AddAndExecute(new AddItemToListCommand(enemy, game.ObjectsOnField));
        }

        public void MovePlayer(Point2D movespeedModifer)
        {
            RewindMacroCommand rewindMacroCommand = GetLastRewindMacroCommand();

            game.Player?.MoveManyally(movespeedModifer, game.Field, game.Ground, rewindMacroCommand);
        }

        public void PlayerFire()
        {
            RewindMacroCommand rewindMacroCommand = GetLastRewindMacroCommand();
            FlyingObject playerBullet = game.FlyingObjectsFactory.CreatePlayerBullet(game.Field, game.Ground, game.Player);

            rewindMacroCommand.AddAndExecute(new AddItemToListCommand(playerBullet, game.ObjectsOnField));
        }

        public RewindMacroCommand GetLastRewindMacroCommand()
        {
            RewindMacroCommand rewindMacroCommand;

            if (game.RewindMacroCommands.Any())
                rewindMacroCommand = game.RewindMacroCommands.Last();
            else
            {
                rewindMacroCommand = new RewindMacroCommand();
                game.RewindMacroCommands.Add(rewindMacroCommand);
            }

            return rewindMacroCommand;
        }

        public void BeginRewind()
        {
            game.State = new RewindGameState(game);
        }
    }
}