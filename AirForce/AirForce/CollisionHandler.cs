using System;
using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    public partial class CollisionHandler
    {
        private readonly Game game;

        public CollisionHandler(Game game)
        {
            this.game = game;
        }

        public List<FlyingObject> GetNewEnemyBullets()
        {
            if (game.Player.Strength == 0)
                return new List<FlyingObject>();

            return game.ObjectsOnField
                .OfType<ShootingFlyingObject>()
                .Where(o => o.CanShootToTarget(game.Player))
                .Select(o => game.FlyingObjectsFactory.CreateEnemyBullet(game.GameField, game.Ground, enemy: o))
                .ToList();
        }

        public List<ChangeStrengthCommand> FindCollisionsAndGetChangeStrengthCommands()
        {
            var changeStrengthCommands = new List<ChangeStrengthCommand>();

            for (int i = 0; i < game.ObjectsOnField.Count - 1; i++)
                for (int j = i + 1; j < game.ObjectsOnField.Count; j++)
                {
                    var objA = game.ObjectsOnField[i];
                    var objB = game.ObjectsOnField[j];

                    if (objA.Strength > 0 &&
                        objB.Strength > 0 &&
                        CanCollide(objA, objB) &&
                        IsIntersects(objA, objB))
                        changeStrengthCommands.AddRange(ChangeStrengths(objA, objB));
                }

            foreach (FlyingObject obj in game.ObjectsOnField.Where(o => o.Strength > 0))
            {
                if (IsOutOfFieldLeftBorder(obj, game.GameField) ||
                    IsIntersectGround(obj, game.Ground) ||
                    obj.Type == FlyingObjectType.PlayerBullet && IsOutOfFieldRightBorder(obj, game.GameField))
                {
                    var command = new ChangeStrengthCommand(obj);
                    command.SetStrength(0);
                    changeStrengthCommands.Add(command);
                }
            }

            return changeStrengthCommands;
        }

        private bool CanCollide(FlyingObject objA, FlyingObject objB)
        {
            if (!game.CollisionTable.ContainsKey(objA.Type))
                throw new KeyNotFoundException();

            return game.CollisionTable[objA.Type].Contains(objB.Type);
        }

        private List<ChangeStrengthCommand> ChangeStrengths(FlyingObject objA, FlyingObject objB)
        {
            var changeStrengthCommands = new List<ChangeStrengthCommand>
            {
                new ChangeStrengthCommand(objA),
                new ChangeStrengthCommand(objB)
            };

            int minStrength = Math.Min(objA.Strength, objB.Strength);

            foreach (var command in changeStrengthCommands)
                command.AddStrength(-minStrength);

            return changeStrengthCommands;
        }
    }
}
