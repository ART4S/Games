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

        public ChangePositionCommand Move(Field gameField, Ground ground, List<FlyingObject> objectsOnField)
        {
            var changePositionCommand = new ChangePositionCommand(flyingObject);

            List<FlyingObject> playerBullets = objectsOnField
                .FindAll(f => f.Type == FlyingObjectType.PlayerBullet);

            List<Point2D> minPathToFreeWay = GetMinPathToFreeWay(playerBullets, gameField, ground);

            changePositionCommand.ShiftPostion(minPathToFreeWay.Any()
                ? minPathToFreeWay.First()
                : new Point2D(-flyingObject.Movespeed, 0));

            return changePositionCommand;
        }

        private List<Point2D> GetMinPathToFreeWay(List<FlyingObject> dangerousObjects, Field gameField, Ground ground) // BFS algorithm
        {
            List<FlyingObject> objectsInRadiusOfSight = dangerousObjects
                .FindAll(o => CollisionHandler.IsIntersects(flyingObject.Position, flyingObject.RadiusOfSight, o.Position, o.Radius)); // поле зрения - окружность

            var shiftsQueue = new Queue<Point2D>();
            var savePaths = new Dictionary<Point2D, Point2D>();

            savePaths[new Point2D()] = new Point2D();
            shiftsQueue.Enqueue(new Point2D());

            while (shiftsQueue.Any())
            {
                Point2D currentShift = shiftsQueue.Dequeue();
                Point2D currentPosition = flyingObject.Position + currentShift;

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

                Point2D moveUpShift = new Point2D(-flyingObject.Movespeed, -flyingObject.Movespeed); // сдвиг по диагонали вверх
                Point2D moveDownShift = new Point2D(-flyingObject.Movespeed, flyingObject.Movespeed); // сдвиг по дагонали вниз
                 
                if (!savePaths.ContainsKey(moveUpShift))
                {
                    savePaths[moveUpShift] = currentShift;
                    shiftsQueue.Enqueue(moveUpShift);
                }

                if (!savePaths.ContainsKey(moveDownShift))
                {
                    savePaths[moveDownShift] = currentShift;
                    shiftsQueue.Enqueue(moveDownShift);
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