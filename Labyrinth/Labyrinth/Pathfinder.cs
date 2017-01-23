using System;
using System.Collections.Generic;
using System.Linq;

namespace Labyrinth
{
    public class Pathfinder
    {
        private readonly int maxWeigths;
        private readonly Point[] directionsArray;
                    
        public Pathfinder(int maxWeigths)
        {
            this.maxWeigths = maxWeigths;

            directionsArray = Enum.GetValues(typeof(Direction))
                .Cast<Direction>()
                .Select(direction => direction.ToPoint())
                .ToArray();
        }

        public List<Point> FindPathWithDfs(Terrain[,] terrainsMap, Dictionary<Terrain, int> weigthsTable, Point firstPoint, Point secondPoint)
        {
            var stack = new Stack<Point>();
            var usedCells = new HashSet<Point>();
            var savePaths = new Dictionary<Point, Point>();

            stack.Push(firstPoint);

            while (stack.Any())
            {
                Point currentPoint = stack.Pop();

                usedCells.Add(currentPoint);

                foreach (Point direction in directionsArray)
                {
                    Point newPoint = currentPoint + direction;

                    if (weigthsTable[terrainsMap[newPoint.X, newPoint.Y]] == maxWeigths || usedCells.Contains(newPoint))
                        continue;

                    savePaths[newPoint] = currentPoint;

                    stack.Push(newPoint);
                }
            }

            return RecoveryPathBetweenPoints(savePaths, firstPoint, secondPoint);
        }

        public List<Point> FindPathWithBfs(Terrain[,] terrainsMap, Dictionary<Terrain, int> weigthsTable, Point firstPoint, Point secondPoint)
        {
            var queue = new Queue<Point>();
            var usedCells = new HashSet<Point>();
            var savePaths = new Dictionary<Point, Point>();

            queue.Enqueue(firstPoint);

            while (queue.Any())
            {
                Point currentPoint = queue.Dequeue();

                if (currentPoint == secondPoint)
                    break;

                usedCells.Add(currentPoint);

                foreach (Point direction in directionsArray)
                {
                    Point newPoint = currentPoint + direction;

                    if (weigthsTable[terrainsMap[newPoint.X, newPoint.Y]] == maxWeigths || usedCells.Contains(newPoint))
                        continue;

                    savePaths[newPoint] = currentPoint;

                    queue.Enqueue(newPoint);
                }
            }

            return RecoveryPathBetweenPoints(savePaths, firstPoint, secondPoint);
        }

        public List<Point> FindPathWithDijkstra(Terrain[,] terrainsMap, Dictionary<Terrain, int> weigthsTable, Point firstPoint, Point secondPoint)
        {
            var heap = new Heap<Point, int>();
            var usedCells = new HashSet<Point>();
            var savePaths = new Dictionary<Point, Point>();
            var distanceTo = new Dictionary<Point, int>();

            distanceTo[firstPoint] = 0;

            heap.Add(firstPoint, distanceTo[firstPoint]);

            while (heap.Any())
            {
                Point currentPoint = heap.Pop();
                int currentPointWeight = weigthsTable[terrainsMap[currentPoint.X, currentPoint.Y]];

                usedCells.Add(currentPoint);

                foreach (Point direction in directionsArray)
                {
                    Point newPoint = currentPoint + direction;

                    if (weigthsTable[terrainsMap[newPoint.X, newPoint.Y]] == maxWeigths)
                        continue;

                    if (!distanceTo.ContainsKey(newPoint) || distanceTo[newPoint] > distanceTo[currentPoint] + currentPointWeight)
                    {
                        distanceTo[newPoint] = distanceTo[currentPoint] + currentPointWeight;
                        savePaths[newPoint] = currentPoint;
                    }

                    if (!usedCells.Contains(newPoint))
                        heap.Add(newPoint, distanceTo[newPoint]);
                }
            }

            return RecoveryPathBetweenPoints(savePaths, firstPoint, secondPoint);
        }

        public List<Point> FindPathWithSmartDijkstra(Terrain[,] terrainsMap, Dictionary<Terrain, int> firstPointWeigthsTable, Dictionary<Terrain, int> secondPointWeigthsTable, Point firstPoint, Point secondPoint, Point secondPointFinish)
        {
            List<Point> minPathSecondPoint = FindPathWithDijkstra(terrainsMap, secondPointWeigthsTable, secondPoint, secondPointFinish);

            if (!minPathSecondPoint.Contains(secondPoint))
                minPathSecondPoint.Add(secondPoint);

            if (minPathSecondPoint.Any(point => point == firstPoint))
                return FindPathWithDijkstra(terrainsMap, firstPointWeigthsTable, firstPoint, secondPoint);

            var heap = new Heap<Point, int>();
            var usedCells = new HashSet<Point>();
            var distanceTo = new Dictionary<Point, int>();

            distanceTo[firstPoint] = 0;

            heap.Add(firstPoint, distanceTo[firstPoint]);

            while (heap.Any())
            {
                Point currentPoint = heap.Pop();
                int currentPointWeight = firstPointWeigthsTable[terrainsMap[currentPoint.X, currentPoint.Y]];

                usedCells.Add(currentPoint);

                foreach (Point direction in directionsArray)
                {
                    Point newPoint = currentPoint + direction;

                    if (firstPointWeigthsTable[terrainsMap[newPoint.X, newPoint.Y]] == maxWeigths)
                        continue;

                    if (!distanceTo.ContainsKey(newPoint) || distanceTo[newPoint] > distanceTo[currentPoint] + currentPointWeight)
                        distanceTo[newPoint] = distanceTo[currentPoint] + currentPointWeight;

                    if (!usedCells.Contains(newPoint))
                        heap.Add(newPoint, distanceTo[newPoint]);
                }
            }

            int minDistanceToPathSecondPoint = int.MaxValue;
            Point minPointOnPathSecondPoint = secondPoint;

            foreach (Point point in minPathSecondPoint)
            {
                if (distanceTo.ContainsKey(point) && minDistanceToPathSecondPoint > distanceTo[point])
                {
                    minDistanceToPathSecondPoint = distanceTo[point];
                    minPointOnPathSecondPoint = point;
                }
            }

            return FindPathWithDijkstra(terrainsMap, firstPointWeigthsTable, firstPoint, minPointOnPathSecondPoint);
        }

        private List<Point> RecoveryPathBetweenPoints(Dictionary<Point, Point> savePaths, Point firstPoint, Point secondPoint)
        {
            var path = new List<Point>();

            if (savePaths.ContainsKey(secondPoint))
            {
                while (savePaths[secondPoint] != firstPoint)
                {
                    path.Add(secondPoint);
                    secondPoint = savePaths[secondPoint];
                }

                path.Add(secondPoint);
            }

            path.Reverse();

            return path.Any() ? path : new List<Point>() { firstPoint };
        }
    }
}
