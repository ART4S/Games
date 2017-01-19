using System.Collections.Generic;
using System.Linq;

namespace Labyrinth
{
    class WaveAlgorithm
    {
        private const int Inf = int.MaxValue;

        private readonly Terrain[,] mapTerrains;
        private readonly Point[] arrayDirections;
        
        public WaveAlgorithm(Terrain[,] mapTerrains)
        {
            this.mapTerrains = mapTerrains;

            arrayDirections = new[]
            {
                new Point(1, 0),
                new Point(0, 1),
                new Point(-1, 0),
                new Point(0, -1)
            };
        }

        public List<Point> GetRoadWithDfs(Dictionary<Terrain, int> tableWeigths, Point firstPoint, Point secondPoint)
        {
            var stack = new Stack<Point>();
            var usedCells = new HashSet<Point>();
            var saveRoads = new Dictionary<Point, Point>();

            stack.Push(firstPoint);

            while (stack.Any())
            {
                Point currentPoint = stack.Pop();

                usedCells.Add(currentPoint);

                foreach (Point direction in arrayDirections)
                {
                    Point newPoint = currentPoint + direction;

                    if (tableWeigths[mapTerrains[newPoint.X, newPoint.Y]] == Inf || usedCells.Contains(newPoint))
                        continue;

                    saveRoads[newPoint] = currentPoint;
                    stack.Push(newPoint);
                }
            }

            return RecoveryRoadBetweenPoints(saveRoads, firstPoint, secondPoint);
        }

        public List<Point> GetRoadWithBfs(Dictionary<Terrain, int> tableWeigths, Point firstPoint, Point secondPoint)
        {
            var queue = new Queue<Point>();
            var usedCells = new HashSet<Point>();
            var saveRoads = new Dictionary<Point, Point>();

            queue.Enqueue(firstPoint);

            while (queue.Any())
            {
                Point currentPoint = queue.Dequeue();

                usedCells.Add(currentPoint);

                foreach (Point direction in arrayDirections)
                {
                    Point newPoint = currentPoint + direction;

                    if (tableWeigths[mapTerrains[newPoint.X, newPoint.Y]] == Inf || usedCells.Contains(newPoint))
                        continue;

                    saveRoads[newPoint] = currentPoint;
                    queue.Enqueue(newPoint);
                }
            }

            return RecoveryRoadBetweenPoints(saveRoads, firstPoint, secondPoint);
        }

        public List<Point> GetRoadWithDijkstra(Dictionary<Terrain, int> tableWeigths, Point firstPoint, Point secondPoint)
        {
            var heap = new Heap();
            var usedCells = new HashSet<Point>();
            var saveRoads = new Dictionary<Point, Point>();
            var distanceTo = new Dictionary<Point, int>();

            distanceTo[firstPoint] = 0;

            heap.Add(firstPoint, distanceTo[firstPoint]);

            while (heap.Any())
            {
                Point currentPoint = heap.Pop();
                int currentPointWeight = tableWeigths[mapTerrains[currentPoint.X, currentPoint.Y]];

                usedCells.Add(currentPoint);

                foreach (Point direction in arrayDirections)
                {
                    Point newPoint = currentPoint + direction;

                    if (tableWeigths[mapTerrains[newPoint.X, newPoint.Y]] == Inf)
                        continue;

                    if (!distanceTo.ContainsKey(newPoint) || distanceTo[newPoint] > distanceTo[currentPoint] + currentPointWeight)
                    {
                        distanceTo[newPoint] = distanceTo[currentPoint] + currentPointWeight;
                        saveRoads[newPoint] = currentPoint;
                    }

                    if (!usedCells.Contains(newPoint))
                        heap.Add(newPoint, distanceTo[newPoint]);
                }
            }

            return RecoveryRoadBetweenPoints(saveRoads, firstPoint, secondPoint);
        }

        public List<Point> GetRoadWithSmartDijkstra(Dictionary<Terrain, int> tableWeigthsFirstPoint, Dictionary<Terrain, int> tableWeigthsSecondPoint, Point firstPoint, Point secondPointStart, Point secondPointFinish)
        {
            List<Point> minRoadSecondPoint = GetRoadWithDijkstra(tableWeigthsSecondPoint, secondPointStart, secondPointFinish);

            if (!minRoadSecondPoint.Contains(secondPointStart))
                minRoadSecondPoint.Add(secondPointStart);

            if (minRoadSecondPoint.Any(point => point == firstPoint))
                return GetRoadWithDijkstra(tableWeigthsFirstPoint, firstPoint, secondPointStart);

            var minRoadToMinRoadSecondPoint = new List<Point>() { firstPoint };
            var minSumWeigths = int.MaxValue;

            foreach (var point in minRoadSecondPoint)
            {
                if (tableWeigthsFirstPoint[mapTerrains[point.X, point.Y]] == Inf)
                    continue;

                List<Point> minRoadToPointInRoadSecondPoint = GetRoadWithDijkstra(tableWeigthsFirstPoint, firstPoint, point);

                int sumWeigths = minRoadToPointInRoadSecondPoint.Sum(p => tableWeigthsFirstPoint[mapTerrains[p.X, p.Y]]);

                if (sumWeigths < minSumWeigths)
                {
                    minSumWeigths = sumWeigths;
                    minRoadToMinRoadSecondPoint = minRoadToPointInRoadSecondPoint;
                }
            }

            return minRoadToMinRoadSecondPoint;
        }

        private List<Point> RecoveryRoadBetweenPoints(Dictionary<Point, Point> saveRoads, Point firstPoint, Point secondPoint)
        {
            var road = new List<Point>();

            if (saveRoads.ContainsKey(secondPoint))
            {
                Point currentPoint = secondPoint;

                while (saveRoads[currentPoint] != firstPoint)
                {
                    road.Add(currentPoint);
                    currentPoint = saveRoads[currentPoint];
                }

                road.Add(currentPoint);
            }

            road.Reverse();

            return road.Any() ? road : new List<Point>() { firstPoint };
        }
    }
}
