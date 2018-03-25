using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    public class DodgingMover : IMover
    {
        private readonly FlyingObject source;

        public DodgingMover(FlyingObject source)
        {
            this.source = source;
        }

        public void Move(Rectangle2D field, Rectangle2D ground, List<FlyingObject> objectsOnField, RewindMacroCommand rewindMacroCommand)
        {
            List<FlyingObject> playerBullets = objectsOnField
                .FindAll(f => f.Type == FlyingObjectType.PlayerBullet);

            List<Point2D> minPathToFreeWay = GetMinPathToFreeWay(playerBullets, field, ground);

            Point2D shift = minPathToFreeWay.Any()
                ? minPathToFreeWay.First() - source.Position
                : new Point2D(-source.Movespeed, 0);

            rewindMacroCommand.AddAndExecute(new ShiftPositionCommand(source, shift));
        }

        private List<Point2D> GetMinPathToFreeWay(List<FlyingObject> dangerousObjects, Rectangle2D field, Rectangle2D ground) // BFS algorithm
        {
            List<FlyingObject> objectsInRadiusOfSight = dangerousObjects
                .FindAll(o => CollisionHandler.IsIntersects(source.Position, source.RadiusOfSight, o.Position, o.Radius)); // поле зрения - окружность

            if (objectsInRadiusOfSight.Count == 0)
                return new List<Point2D>();

            var positionsQueue = new Queue<Point2D>();
            var savePaths = new Dictionary<Point2D, Point2D>();

            savePaths[source.Position] = source.Position;
            positionsQueue.Enqueue(source.Position);

            while (positionsQueue.Any())
            {
                Point2D currentPosition = positionsQueue.Dequeue();

                bool isObjectsInFront = objectsInRadiusOfSight
                    .Any(o => CollisionHandler.IsInFront(o.Position, o.Radius, currentPosition, source.Radius));

                bool isHaveCollision = objectsInRadiusOfSight
                    .Any(o => CollisionHandler.IsIntersects(currentPosition, source.Radius, o.Position, o.Radius));

                if (isHaveCollision ||
                    CollisionHandler.IsIntersectGround(currentPosition, source.Radius, ground) ||
                    CollisionHandler.IsIntersectFieldTopBorder(currentPosition, source.Radius, field))
                    continue;

                if (!isObjectsInFront ||
                    CollisionHandler.IsOutOfFieldLeftBorder(currentPosition, source.Radius, field))
                    return GetRestoredPath(savePaths, currentPosition);

                Point2D moveUpPosition = currentPosition + new Point2D(-source.Movespeed, -source.Movespeed); // сдвиг по диагонали вверх
                Point2D moveDownPosition = currentPosition + new Point2D(-source.Movespeed, source.Movespeed); // сдвиг по дагонали вниз

                if (!savePaths.ContainsKey(moveUpPosition))
                {
                    savePaths[moveUpPosition] = currentPosition;
                    positionsQueue.Enqueue(moveUpPosition);
                }

                if (!savePaths.ContainsKey(moveDownPosition))
                {
                    savePaths[moveDownPosition] = currentPosition;
                    positionsQueue.Enqueue(moveDownPosition);
                }
            }

            return new List<Point2D>();
        }

        private List<Point2D> GetRestoredPath(Dictionary<Point2D, Point2D> savePaths, Point2D endPoint)
        {
            List<Point2D> path = new List<Point2D>();
            Point2D currentPoint = endPoint;

            while (savePaths.ContainsKey(currentPoint) && savePaths[currentPoint] != currentPoint)
            {
                path.Add(currentPoint);
                currentPoint = savePaths[currentPoint];
            }

            path.Reverse();

            return path;
        }
    }
}