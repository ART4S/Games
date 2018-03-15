using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    public class ChaserShipMover : IMover
    {
        private readonly FlyingObject chaser;

        public ChaserShipMover(FlyingObject chaser)
        {
            this.chaser = chaser;
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects)
        {
            var playerBullets = flyingObjects
                .Where(f => f.Type == FlyingObjectType.PlayerBullet)
                .ToList();

            List<Point2D> minPathToFreeTrajectory = GetMinPathToFreeTrajectory(playerBullets, gameField, ground);

            chaser.Position = minPathToFreeTrajectory.Any() ?
                minPathToFreeTrajectory.First() :
                chaser.Position - new Point2D(chaser.Movespeed, 0);
        }

        private List<Point2D> GetMinPathToFreeTrajectory(List<FlyingObject> dangerousObjects, Field gameField, Ground ground) // BFS algorithm
        {
            var queue = new Queue<Point2D>();
            var savePaths = new Dictionary<Point2D, Point2D>{{ chaser.Position, chaser.Position }};

            queue.Enqueue(chaser.Position);

            while (queue.Any())
            {
                Point2D currentPosition = queue.Dequeue();

                bool isDangerousObjectsInFront = dangerousObjects
                    .Any(o => CollisionHandler.IsInFront(o.Position, o.Radius, currentPosition, chaser.Radius));

                bool isHaveCollision = dangerousObjects
                    .Any(o => CollisionHandler.IsIntersects(currentPosition, chaser.Radius, o.Position, o.Radius));

                if (!isDangerousObjectsInFront ||
                    CollisionHandler.IsOutOfField(currentPosition, chaser.Radius, gameField))
                    return RestorePath(savePaths, currentPosition);

                if (isHaveCollision ||
                    CollisionHandler.IsIntersectGround(currentPosition, chaser.Radius, ground) ||
                    CollisionHandler.IsIntersectFieldTopBorder(currentPosition, chaser.Radius, gameField))
                    continue;

                Point2D moveUpPosition = currentPosition - new Point2D(chaser.Movespeed, chaser.Movespeed); // отнять от x, отнять от y
                Point2D moveDownPosition = currentPosition - new Point2D(chaser.Movespeed, -chaser.Movespeed); // отнять от x, прибавить к y

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

        private List<Point2D> RestorePath(Dictionary<Point2D, Point2D> savePaths, Point2D endPoint)
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