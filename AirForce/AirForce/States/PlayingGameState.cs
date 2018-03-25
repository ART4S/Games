using System.Collections.Generic;
using System.Drawing;
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
        public void Paint(Graphics graphics) { }
        public void IncreaseSpeed() { }
        public void DecreaseSpeed() { }

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
            List<FlyingObject> newEnemyBullets = GetNewEnemyBullets(rewindMacroCommand);

            foreach (FlyingObject bullet in newEnemyBullets)
                rewindMacroCommand.AddAndExecute(new AddObjectToGameCommand(bullet, game));
        }

        private List<FlyingObject> GetNewEnemyBullets(RewindMacroCommand rewindMacroCommand)
        {
            if (game.Player.Strength == 0)
                return new List<FlyingObject>();

            return game.ObjectsOnField
                .OfType<ShootingFlyingObject>()
                .Where(o => o.CanShootToTarget(game.Player, rewindMacroCommand))
                .Select(o => game.FlyingObjectsFactory.CreateEnemyBullet(game.Field, game.Ground, o))
                .ToList();
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
                rewindMacroCommand.AddAndExecute(new RemoveObjectFromGameCommand(obj, game));
        }

        private void AddEnemy(RewindMacroCommand rewindMacroCommand)
        {
            game.EnemiesCreatingCooldown.Tick(rewindMacroCommand);

            if (!game.EnemiesCreatingCooldown.IsCollapsed)
                return;

            FlyingObject enemy = game.FlyingObjectsFactory.CreateRandomEnemy(game.Field, game.Ground);

            rewindMacroCommand.AddAndExecute(new AddObjectToGameCommand(enemy, game));
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

            rewindMacroCommand.AddAndExecute(new AddObjectToGameCommand(playerBullet, game));
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