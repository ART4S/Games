using System;
using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    public class CollisionHandler
    {
        private readonly Game game;

        public CollisionHandler(Game game)
        {
            this.game = game;
        }

        public void FindCollisionsAndChangeStrengths(RewindMacroCommand rewindMacroCommand)
        {
            for (int i = 0; i < game.ObjectsOnField.Count - 1; i++)
                for (int j = i + 1; j < game.ObjectsOnField.Count; j++)
                {
                    var objA = game.ObjectsOnField[i];
                    var objB = game.ObjectsOnField[j];

                    if (objA.Strength > 0 && objB.Strength > 0 && CanCollide(objA, objB) && objA.ToCircle2D().IsIntersect(objB.ToCircle2D()))
                        ChangeStrengths(objA, objB, rewindMacroCommand);
                }

            foreach (FlyingObject obj in game.ObjectsOnField)
            {
                Circle2D objCircle = obj.ToCircle2D();

                if (!objCircle.IsIntersect(game.Field) || objCircle.IsIntersect(game.Ground))
                    rewindMacroCommand.AddAndExecute(new SubtractStrengthCommand(obj, obj.Strength)); // устанавливаем силу 0
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

            rewindMacroCommand.AddAndExecute(new SubtractStrengthCommand(objA, minStrength));
            rewindMacroCommand.AddAndExecute(new SubtractStrengthCommand(objB, minStrength));
        }
    }
}
