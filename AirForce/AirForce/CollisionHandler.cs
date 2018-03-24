﻿using System;
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

        public List<FlyingObject> GetNewEnemyBullets(RewindMacroCommand rewindMacroCommand)
        {
            if (game.Player.Strength == 0)
                return new List<FlyingObject>();

            return game.ObjectsOnField
                .OfType<ShootingFlyingObject>()
                .Where(o => o.CanShootToTarget(game.Player, rewindMacroCommand))
                .Select(o => game.FlyingObjectsFactory.CreateEnemyBullet(game.Field, game.Ground, o))
                .ToList();
        }

        public void FindCollisionsAndChangeStrengths(RewindMacroCommand rewindMacroCommand)
        {
            for (int i = 0; i < game.ObjectsOnField.Count - 1; i++)
                for (int j = i + 1; j < game.ObjectsOnField.Count; j++)
                {
                    var objA = game.ObjectsOnField[i];
                    var objB = game.ObjectsOnField[j];

                    if (objA.Strength > 0 && objB.Strength > 0 && CanCollide(objA, objB) && IsIntersects(objA, objB))
                        ChangeStrengths(objA, objB, rewindMacroCommand);
                }

            foreach (FlyingObject obj in game.ObjectsOnField)
            {
                if (IsOutOfFieldLeftBorder(obj, game.Field) ||
                    IsIntersectGround(obj, game.Ground) ||
                    obj.Type == FlyingObjectType.PlayerBullet && IsOutOfFieldRightBorder(obj, game.Field))
                {
                    var setStrengthCommand = new ChangeStrengthCommand(obj);
                    setStrengthCommand.SetStrength(0);
                    rewindMacroCommand.AddCommand(setStrengthCommand);
                }
            }
        }

        private bool CanCollide(FlyingObject objA, FlyingObject objB)
        {
            if (!game.CollisionTable.ContainsKey(objA.Type))
                throw new KeyNotFoundException();

            return game.CollisionTable[objA.Type].Contains(objB.Type);
        }

        private void ChangeStrengths(FlyingObject objA, FlyingObject objB, RewindMacroCommand rewindMacroCommand)
        {
            int minStrength = Math.Min(objA.Strength, objB.Strength);

            ChangeStrengthCommand objAaddStrengthCommand = new ChangeStrengthCommand(objA);
            objAaddStrengthCommand.AddStrength(-minStrength);
            rewindMacroCommand.AddCommand(objAaddStrengthCommand);

            ChangeStrengthCommand objBaddStrengthCommand = new ChangeStrengthCommand(objB);
            objBaddStrengthCommand.AddStrength(-minStrength);
            rewindMacroCommand.AddCommand(objBaddStrengthCommand);
        }
    }
}
