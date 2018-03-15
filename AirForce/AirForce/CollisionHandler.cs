using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AirForce
{
    public class CollisionHandler
    {
        private readonly Dictionary<FlyingObjectType, FlyingObjectType[]> collisionTable;

        public CollisionHandler(Dictionary<FlyingObjectType, FlyingObjectType[]> collisionTable)
        {
            this.collisionTable = collisionTable;
        }

        #region Methods

        public List<FlyingObject> GetNewEnemyBullets(List<FlyingObject> flyingObjects, Field gameField, Ground ground, FlyingObjectsFactory factory)
        {
            List<FlyingObject> chaserShips = flyingObjects
                .Where(o => o.Type == FlyingObjectType.ChaserShip)
                .ToList();

            FlyingObject player = flyingObjects
                .Find(o => o.Type == FlyingObjectType.PlayerShip);

            List<FlyingObject> newEnemyBullets = chaserShips
                .Where(chaser => IsInFront(player, chaser))
                .Select(chaser => factory.GetEnemyBullet(gameField, ground, chaser))
                .ToList();

            return newEnemyBullets;
        }

        public void HandleCollisions(List<FlyingObject> flyingObjects, Field gameField, Ground ground)
        {
            for (int i = 0; i < flyingObjects.Count - 1; i++)
                for (int j = i + 1; j < flyingObjects.Count; j++)
                {
                    FlyingObject objA = flyingObjects[i];
                    FlyingObject objB = flyingObjects[j];

                    if (CanCollide(objA, objB) && IsIntersects(objA, objB))
                        ChangeStrength(objA, objB);
                }

            foreach (FlyingObject obj in flyingObjects)
            {
                if (IsOutOfFieldLeftBorder(obj, gameField) ||
                    IsIntersectGround(obj, ground))
                    obj.Strength = 0;

                if (obj.Type == FlyingObjectType.PlayerBullet &&
                    IsOutOfFieldRightBorder(obj, gameField))
                    obj.Strength = 0;
            }

            flyingObjects.RemoveAll(f => f.Strength <= 0 && f.Type != FlyingObjectType.PlayerShip);
                
        }

        private bool CanCollide(FlyingObject objA, FlyingObject objB)
        {
            if (!collisionTable.ContainsKey(objA.Type))
                throw new KeyNotFoundException();

            return collisionTable[objA.Type].Contains(objB.Type);
        }

        private void ChangeStrength(FlyingObject objA, FlyingObject objB)
        {
            int minStrength = Math.Min(objA.Strength, objB.Strength);
            objA.Strength -= minStrength;
            objB.Strength -= minStrength;
        }

        #endregion

        #region Static methods

        public static bool IsIntersects(FlyingObject objA, FlyingObject objB)
        {
            return IsIntersects(objA.Position, objA.Radius, objB.Position, objB.Radius);
        }

        public static bool IsIntersects(Point2D firstPosition, int firstRadius, Point2D secondPosition, int secondRadius)
        {
            return Math.Pow(firstPosition.X - secondPosition.X, 2) +
                   Math.Pow(firstPosition.Y - secondPosition.Y, 2) <=
                   Math.Pow(firstRadius + secondRadius, 2); // пересечение окружностей
        }

        public static bool IsInFront(FlyingObject self, FlyingObject other)
        {
            return IsInFront(self.Position, self.Radius, other.Position, other.Radius);
        }

        public static bool IsInFront(Point2D firstPosition, int firstRadius, Point2D secondPosition, int secondRadius)
        {
            int firstTopBorder = firstPosition.Y - firstRadius;
            int firstBottomBorder = firstPosition.Y + firstRadius;
            int firstRightBorder = firstPosition.X + firstRadius;

            int secondTopBorder = secondPosition.Y - secondRadius;
            int secondBottomBorder = secondPosition.Y + secondRadius;
            int secondLerftBorder = secondPosition.X - secondRadius;

            bool isHaveMutualX = firstRightBorder <= secondLerftBorder;
            bool isHaveMutualY = Math.Max(firstTopBorder, secondTopBorder) <= Math.Min(firstBottomBorder, secondBottomBorder);

            return isHaveMutualX && isHaveMutualY;
        }

        public static bool IsOutOfFieldLeftBorder(FlyingObject self, Field field)
        {
            return IsOutOfFieldLeftBorder(self.Position, self.Radius, field);
        }

        public static bool IsOutOfFieldLeftBorder(Point2D position, int radius, Field field)
        {
            return position.X + radius < field.TopLeftPoint.X;
        }

        public static bool IsOutOfFieldRightBorder(FlyingObject self, Field field)
        {
            return IsOutOfFieldRightBorder(self.Position, self.Radius, field);
        }

        public static bool IsOutOfFieldRightBorder(Point2D position, int radius, Field field)
        {
            return position.X - radius >= field.TopRightPoint.X;
        }

        public static bool IsIntersectFieldTopBorder(Point2D position, int radius, Field field)
        {
            return position.Y - radius < field.TopLeftPoint.Y;
        }

        public static bool IsIntersectGround(FlyingObject self, Ground ground)
        {
            return IsIntersectGround(self.Position, self.Radius, ground);
        }

        public static bool IsIntersectGround(Point2D position, int radius, Ground ground)
        {
            Rectangle objRectangle = new Rectangle(
                position - new Point2D(radius, radius),
                new Size(2 * radius, 2 * radius));

            Rectangle groundRectangle = new Rectangle(ground.Position, ground.Size);

            return objRectangle.IntersectsWith(groundRectangle);
        }

        public static bool IsOutOfField(Point2D position, int radius, Field field)
        {
            Rectangle objRectangle = new Rectangle(
                position - new Point2D(radius, radius),
                new Size(2 * radius, 2 * radius));

            return !objRectangle.IntersectsWith(field);
        }

        public static bool IsEntirelyOnField(Point2D position, int radius, Field field)
        {
            Rectangle objRectangle = new Rectangle(
                position - new Point2D(radius, radius),
                new Size(2 * radius, 2 * radius));

            Rectangle fieldRectangle = field;

            return fieldRectangle.Contains(objRectangle);
        }

        #endregion
    }
}
