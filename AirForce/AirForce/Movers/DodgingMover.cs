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
            Circle2D fieldOfSight = new Circle2D(source.Position, source.RadiusOfSight);

            List<FlyingObject> objectsInFieldOfSight = dangerousObjects.FindAll(o => fieldOfSight.IsIntersect(o));

            if (objectsInFieldOfSight.Count == 0)
                return new List<Point2D>();

            var positionsQueue = new Queue<Point2D>();
            var savePaths = new Dictionary<Point2D, Point2D>();

            savePaths[source.Position] = source.Position;
            positionsQueue.Enqueue(source.Position);

            while (positionsQueue.Any())
            {
                Point2D sourceCurrentPosition = positionsQueue.Dequeue();
                Circle2D sourceCurrentCircle = new Circle2D(sourceCurrentPosition, source.Radius);

                bool isObjectsInBack = objectsInFieldOfSight.Any(o => sourceCurrentCircle.IsCircleInBack(o));
                bool isHaveCollision = objectsInFieldOfSight.Any(o => sourceCurrentCircle.IsIntersect(o));

                if (isHaveCollision || sourceCurrentCircle.IsIntersect(ground) || !field.IsContains(sourceCurrentCircle))
                    continue;

                if (!isObjectsInBack)
                    return GetRestoredPath(savePaths, sourceCurrentPosition);

                Point2D moveUpPosition = sourceCurrentPosition + new Point2D(-source.Movespeed, -source.Movespeed); // сдвиг по диагонали вверх
                Point2D moveDownPosition = sourceCurrentPosition + new Point2D(-source.Movespeed, source.Movespeed); // сдвиг по дагонали вниз

                if (!savePaths.ContainsKey(moveUpPosition))
                {
                    savePaths[moveUpPosition] = sourceCurrentPosition;
                    positionsQueue.Enqueue(moveUpPosition);
                }

                if (!savePaths.ContainsKey(moveDownPosition))
                {
                    savePaths[moveDownPosition] = sourceCurrentPosition;
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