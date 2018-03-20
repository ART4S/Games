using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    public class DodgingMover : IMover
    {
        private readonly FlyingObject flyingObject;

        public DodgingMover(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects)
        {
            List<FlyingObject> playerBullets = flyingObjects
                .FindAll(f => f.Type == FlyingObjectType.PlayerBullet);

            List<Point2D> minPathToFreeWay = GetMinPathToFreeWay(playerBullets, gameField, ground);

            flyingObject.Position = minPathToFreeWay.Any() ?
                minPathToFreeWay.First() :
                flyingObject.Position - new Point2D(flyingObject.Movespeed, 0);
        }

        private List<Point2D> GetMinPathToFreeWay(List<FlyingObject> dangerousObjects, Field gameField, Ground ground) // BFS algorithm
        {
            List<FlyingObject> objectsInRadiusOfSight = dangerousObjects
                .FindAll(o => CollisionHandler.IsIntersects(flyingObject.Position, flyingObject.RadiusOfSight, o.Position, o.Radius)); // поле зрения - окружность

            var queue = new Queue<Point2D>();
            var savePaths = new Dictionary<Point2D, Point2D>();

            savePaths[flyingObject.Position] = flyingObject.Position;
            queue.Enqueue(flyingObject.Position);

            while (queue.Any())
            {
                Point2D currentPosition = queue.Dequeue();

                bool isObjectsInFront = objectsInRadiusOfSight
                    .Any(o => CollisionHandler.IsInFront(o.Position, o.Radius, currentPosition, flyingObject.Radius));

                bool isHaveCollision = objectsInRadiusOfSight
                    .Any(o => CollisionHandler.IsIntersects(currentPosition, flyingObject.Radius, o.Position, o.Radius));

                if (!isObjectsInFront ||
                    CollisionHandler.IsOutOfFieldLeftBorder(currentPosition, flyingObject.Radius, gameField))
                    return GetRestoredPath(savePaths, currentPosition);

                if (isHaveCollision ||
                    CollisionHandler.IsIntersectGround(currentPosition, flyingObject.Radius, ground) ||
                    CollisionHandler.IsIntersectFieldTopBorder(currentPosition, flyingObject.Radius, gameField))
                    continue;

                Point2D moveUpPosition = currentPosition - new Point2D(flyingObject.Movespeed, flyingObject.Movespeed); // по диагонали вверх
                Point2D moveDownPosition = currentPosition - new Point2D(flyingObject.Movespeed, -flyingObject.Movespeed); // по дагонали вниз
                 
                if (!savePaths.ContainsKey(moveUpPosition))
                {
                    savePaths[moveUpPosition] = currentPosition;
                    queue.Enqueue(moveUpPosition);
                }

                if (!savePaths.ContainsKey(moveDownPosition))
                {
                    savePaths[moveDownPosition] = currentPosition;
                    queue.Enqueue(moveDownPosition);
                }
            }

            return new List<Point2D>();
        }

        private List<Point2D> GetRestoredPath(Dictionary<Point2D, Point2D> savePaths, Point2D endPoint)
        {
            List<Point2D> path = new List<Point2D>();
            Point2D currentPoint = endPoint;

            if (savePaths.ContainsKey(currentPoint))
            {
                while (savePaths[currentPoint] != currentPoint)
                {
                    path.Add(currentPoint);
                    currentPoint = savePaths[currentPoint];
                }
            }

            path.Reverse();

            return path;
        }
    }
}