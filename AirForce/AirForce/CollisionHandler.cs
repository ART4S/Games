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

            return game.FlyingObjects
                .OfType<ShootingFlyingObject>()
                .Where(o => o.CanShootToTarget(game.Player))
                .Select(o => game.FlyingObjectsFactory.CreateEnemyBullet(game.GameField, game.Ground, o))
                .ToList();
        }

        public void FindCollisionsAndChangeStrengths()
        {
            for (int i = 0; i < game.FlyingObjects.Count - 1; i++)
                for (int j = i + 1; j < game.FlyingObjects.Count; j++)
                {
                    var objA = game.FlyingObjects[i];
                    var objB = game.FlyingObjects[j];

                    if (CanCollide(objA, objB) && IsIntersects(objA, objB))
                        ChangeStrength(objA, objB);
                }

            foreach (FlyingObject obj in game.FlyingObjects)
            {
                if (IsOutOfFieldLeftBorder(obj, game.GameField) || IsIntersectGround(obj, game.Ground))
                    obj.Strength = 0;

                if (obj.Type == FlyingObjectType.PlayerBullet && IsOutOfFieldRightBorder(obj, game.GameField))
                    obj.Strength = 0;
            }             
        }

        private bool CanCollide(FlyingObject objA, FlyingObject objB)
        {
            if (!game.CollisionTable.ContainsKey(objA.Type))
                throw new KeyNotFoundException();

            return game.CollisionTable[objA.Type].Contains(objB.Type);
        }

        private void ChangeStrength(FlyingObject objA, FlyingObject objB)
        {
            int minStrength = Math.Min(objA.Strength, objB.Strength);
            objA.Strength -= minStrength;
            objB.Strength -= minStrength;
        }
    }
}
