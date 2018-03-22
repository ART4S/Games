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

        public void Update()
        {
            var undoActionsMacroCommand = new UndoActionsMacroCommand();

            game.ObjectsOnField.AddRange(game.CollisionHandler.GetNewEnemyBullets());

            //

            foreach (FlyingObject obj in game.ObjectsOnField)
            {
                ChangePositionCommand moveCommand = obj.Move(game.GameField, game.Ground, game.ObjectsOnField);
                undoActionsMacroCommand.AddCommand(moveCommand);
            }

            List<ChangeStrengthCommand> changeStrengthCommands = game.CollisionHandler
                .FindCollisionsAndGetChangeStrengthCommands();

            foreach (var command in changeStrengthCommands)
                undoActionsMacroCommand.AddCommand(command);

            game.UndoActionsMacroCommands.Add(undoActionsMacroCommand);


            //

            List<FlyingObject> deadObjects = game.ObjectsOnField.FindAll(o => o.Type != FlyingObjectType.PlayerShip && o.Strength <= 0);

            foreach (FlyingObject obj in deadObjects)
            {
                game.DeadObjects.Add(obj);
                game.ObjectsOnField.Remove(obj);
            }

            if (game.EnemiesCreatingCooldown.Tick())
            {
                if (game.ObjectsForReleaseOnField.Any())
                {
                    game.ObjectsOnField.AddRange(game.ObjectsForReleaseOnField.Last());
                    game.ObjectsForReleaseOnField.RemoveAt(game.ObjectsForReleaseOnField.Count - 1);
                }
                else
                    game.ObjectsOnField.Add(game.FlyingObjectsFactory.CreateRandomEnemy(game.GameField, game.Ground));
            }

            if (game.Player.Strength <= 0)
                game.GameState = new WaitingGameState(game);
        }

        public void MovePlayer(Point2D movespeedModifer)
        {
            var undoActionsMacroCommand = new UndoActionsMacroCommand();

            undoActionsMacroCommand.AddCommand(command: game.Player.MoveManyally(movespeedModifer, game.GameField, game.Ground));

            game.UndoActionsMacroCommands.Add(undoActionsMacroCommand);
            game.Player.MoveManyally(movespeedModifer, game.GameField, game.Ground);
        }

        public void PlayerFire()
        {
            game.ObjectsOnField.Add(game.FlyingObjectsFactory.CreatePlayerBullet(game.GameField, game.Ground, game.Player));
        }

        public void BeginRewind()
        {
            game.GameState = new RewindGameState(game);
        }
    }
}