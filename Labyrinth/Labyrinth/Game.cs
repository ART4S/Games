﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Net.Mime;

namespace Labyrinth
{
    public class Game
    {
        const int Inf = 100000;

        private readonly int n;
        private readonly int m;

        private readonly Point defaultPositionMinotaur;
        private readonly Point defaultPositionHuman;

        private readonly Terrain[,] defaultMap;
        private Terrain[,] map;

        private readonly Dictionary<Terrain, int> minotaurPenaltysTable;
        private readonly Dictionary<Terrain, int> humanPenaltysTable;

        private bool[,] humanUsedCells;

        private Point minotaur;
        private Point human;
        private Point exit;

        private int humanPenaltyForCrossing;
        private int minotaurPenaltyForCrossing;

        private  List<Point> pathMinHuman = new List<Point>();
        private List<Point> pathMinMinotaur = new List<Point>();

        public Game(char[,] labyrinth, int n, int m)
        {
            this.n = n;
            this.m = m;

            defaultMap = new Terrain[n, m];

            for (int i = 0; i < n; i++)
                for(int j = 0; j < m; j++)
                {
                    switch (labyrinth[i, j])
                    {
                        case 'X':
                            defaultMap[i, j] = Terrain.Wall;
                            break;

                        case ' ':
                            defaultMap[i, j] = Terrain.Path;
                            break;

                        case 'W':
                            defaultMap[i, j] = Terrain.Water;
                            break;

                        case 'T':
                            defaultMap[i, j] = Terrain.Tree;
                            break;

                        case 'M':
                            defaultMap[i, j] = Terrain.Path;
                            defaultPositionMinotaur = new Point(i, j);
                            break;

                        case 'H':
                            defaultMap[i, j] = Terrain.Path;
                            defaultPositionHuman = new Point(i, j);
                            break;

                        case 'Q':
                            defaultMap[i, j] = Terrain.Exit;
                            exit = new Point(i, j);
                            break;
                    }
                }

            humanPenaltysTable = new Dictionary<Terrain, int>()
            {
                {Terrain.Path, 1},
                {Terrain.Water, 2},
                {Terrain.Tree, Inf},
                {Terrain.Wall, Inf},
                {Terrain.Exit, Inf}
            };

            minotaurPenaltysTable = new Dictionary<Terrain, int>()
            {
                {Terrain.Path, 1},
                {Terrain.Tree, 2},
                {Terrain.Water, Inf},
                {Terrain.Wall, Inf},
                {Terrain.Exit, Inf}
            };

            Restart();
        }

        public void Paint(PaintEventArgs e, int cellSize)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Font font = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    PointF pointForDrawingSymbol = new PointF(j * cellSize + cellSize / 2 - 8, i * cellSize + cellSize / 2 - 8);
                    RectangleF rectangleForDrawingDot = new RectangleF(j * cellSize + cellSize / 2 - 2, i * cellSize + cellSize / 2 - 2, 5, 5);

                    RectangleF currentRectangle = new RectangleF(j * cellSize, i * cellSize, cellSize, cellSize);
                    Point currentPoint = new Point(i, j);

                    switch (map[i, j])
                    {
                        case Terrain.Water:
                            e.Graphics.FillRectangle(Brushes.Blue, currentRectangle);

                            if (humanUsedCells[i, j] && currentPoint != human)
                                e.Graphics.FillEllipse(Brushes.Red, rectangleForDrawingDot);

                            break;

                        case Terrain.Tree:
                            e.Graphics.DrawString("T", font, Brushes.Green, pointForDrawingSymbol);
                            break;

                        case Terrain.Wall:
                            e.Graphics.FillRectangle(Brushes.Black, currentRectangle);
                            break;

                        case Terrain.Path:
                            //if (humanUsedCells[i, j] && currentPoint != minotaur && currentPoint != human)
                            //    e.Graphics.FillEllipse(Brushes.Red, rectangleForDrawingDot);

                            break;
                    }
                }

            e.Graphics.DrawString("Q", font, Brushes.Red, new PointF(exit.Y * cellSize + cellSize / 2 - 8, exit.X * cellSize + cellSize / 2 - 8));
            e.Graphics.DrawString("H", font, Brushes.Orange, new PointF(human.Y * cellSize + cellSize / 2 - 8, human.X * cellSize + cellSize / 2 - 8));
            e.Graphics.DrawString("M", font, Brushes.Magenta, new PointF(minotaur.Y * cellSize + cellSize / 2 - 8, minotaur.X * cellSize + cellSize / 2 - 8));

            for (int i = 0; i <= n; i++)
                e.Graphics.DrawLine(Pens.Black, 0, i * cellSize, m * cellSize, i * cellSize);

            for (int j = 0; j <= m; j++)
                e.Graphics.DrawLine(Pens.Black, j * cellSize, 0, j * cellSize, n * cellSize);

            foreach (var elem in pathMinHuman)
            {
                e.Graphics.FillEllipse(Brushes.Green, elem.Y * cellSize + cellSize / 2 - 2, elem.X * cellSize + cellSize / 2 - 2, 5, 5);
            }
            foreach (var elem in pathMinMinotaur)
            {
                e.Graphics.FillEllipse(Brushes.Red, elem.Y * cellSize + cellSize / 2 - 2, elem.X * cellSize + cellSize / 2 - 2, 5, 5);
            }
        }

        public void Move(Direction direction, Mode mode)
        {
            pathMinHuman.Clear();
            pathMinMinotaur.Clear();
            if (MoveHuman(direction))
                MoveMinotaur(mode);
        }

        private bool MoveHuman(Direction direction)
        {
            if (humanPenaltyForCrossing != 1)
            {
                humanPenaltyForCrossing--;
                return true;
            }

            int dx = 0;
            int dy = 0;

            switch (direction)
            {
                case Direction.UP:
                    dx = -1;
                    break;

                case Direction.DOWN:
                    dx = 1;
                    break;

                case Direction.LEFT:
                    dy = -1;
                    break;

                case Direction.RIGHT:
                    dy = 1;
                    break;
            }

            Point nextPoint = new Point(human.X + dx, human.Y + dy);
            Terrain nextPointType = map[nextPoint.X, nextPoint.Y];

            if (nextPointType == Terrain.Wall || nextPointType == Terrain.Tree)
                return false;

            if (nextPoint == minotaur)
            {
                Restart(); // lose
                return false;
            }

            if (nextPoint == exit)
            {
                Restart(); // won
                return false;
            }

            humanUsedCells[human.X, human.Y] = true;
            human = nextPoint;
            humanPenaltyForCrossing = humanPenaltysTable[map[human.X, human.Y]];

            return true;
        }

        private void MoveMinotaur(Mode mode)
        {
            if (minotaurPenaltyForCrossing != 1)
            {
                minotaurPenaltyForCrossing--;
                return;
            }

            switch (mode)
            {
                case Mode.EazyCrazy:
                    MinotaurMoveRandomDirrection();
                    break;

                case Mode.Eazy:
                    MinotaurMoveBfs();
                    break;

                case Mode.Normal:
                    //var MinRoad = SearchMinimalPath(minotaurPenaltysTable, map, minotaur, human);

                    //if (MinRoad.Any())
                    //    minotaur = MinRoad[0];

                    //pathMinHuman = MinRoad;
                    MinotaurMoveDijkstra();
                    Solve();
                    break;

                case Mode.Hard:
                    break;
            }

            if (minotaur == human)
            {
                Restart(); // lose
                return;
            }

            minotaurPenaltyForCrossing = minotaurPenaltysTable[map[minotaur.X, minotaur.Y]];

            if (map[minotaur.X, minotaur.Y] == Terrain.Tree)
                map[minotaur.X, minotaur.Y] = Terrain.Path;
        }

        private void MinotaurMoveRandomDirrection()
        {
            Random random = new Random();

            int[] dx = { 1,-1, 0, 0 };
            int[] dy = { 0, 0, 1,-1 };

            while (true)
            {
                int i = random.Next(dx.Length);
                Point newPoint = new Point(minotaur.X + dx[i], minotaur.Y + dy[i]);
                Terrain newPointType = map[newPoint.X, newPoint.Y];

                if (newPointType != Terrain.Water && newPointType != Terrain.Wall && newPoint != exit)
                {
                    minotaur = newPoint;
                    return;
                }
            }
        }

        private void MinotaurMoveBfs()
        {
            var queue = new Queue<Point>();
            var usedCells = new HashSet<Point>();
            var saveRoad = new Dictionary<Point, Point>();

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            queue.Enqueue(minotaur);

            while (queue.Any())
            {
                Point currentPoint = queue.Dequeue();

                if (currentPoint == human)
                {
                    // восстанавливаю путь
                    while (saveRoad[currentPoint] != minotaur)
                        currentPoint = saveRoad[currentPoint];

                    if (map[currentPoint.X, currentPoint.Y] != Terrain.Water)
                        minotaur = currentPoint;

                    return;
                }

                for (int i = 0; i < dx.Length; i++)
                {
                    Point newPoint = new Point(currentPoint.X + dx[i], currentPoint.Y + dy[i]);
                    Terrain newPointType = map[newPoint.X, newPoint.Y];

                    if (newPointType != Terrain.Water && newPointType != Terrain.Wall && newPoint != exit && !usedCells.Contains(newPoint))
                    {
                        saveRoad[newPoint] = currentPoint;
                        usedCells.Add(newPoint);
                        queue.Enqueue(newPoint);
                    }

                    if (newPointType == Terrain.Water && newPoint == human)
                    {
                        saveRoad[newPoint] = currentPoint;
                        queue.Enqueue(newPoint);
                    }
                }
            }
        }

        private void MinotaurMoveDijkstra()
        {
            var heap = new Heap();
            var usedCells = new HashSet<Point>();
            var saveRoad = new Dictionary<Point, Point>();
            var distanceTo = new Dictionary<Point, int>();

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            distanceTo[minotaur] = 0;

            heap.Add(minotaur, distanceTo[minotaur]);

            while (heap.Any())
            {
                Point currentPoint = heap.Pop();
                int currentPointWeight = minotaurPenaltysTable[map[currentPoint.X, currentPoint.Y]];

                usedCells.Add(currentPoint);

                for (int i = 0; i < dx.Length; i++)
                {
                    Point newPoint = new Point(currentPoint.X + dx[i], currentPoint.Y + dy[i]);
                    Terrain newPointType = map[newPoint.X, newPoint.Y];

                    if (!distanceTo.ContainsKey(newPoint) || distanceTo[newPoint] > distanceTo[currentPoint] + currentPointWeight)
                    {
                        distanceTo[newPoint] = distanceTo[currentPoint] + currentPointWeight;
                        saveRoad[newPoint] = currentPoint;
                    }

                    if (!usedCells.Contains(newPoint) && newPointType != Terrain.Water && newPointType != Terrain.Wall && newPoint != exit)
                        heap.Add(newPoint, distanceTo[newPoint]);
                }

            }

            // восстанавливаю маршрут
            if (!saveRoad.ContainsKey(human))
                return;

            Point curPoint = human;

            while (saveRoad[curPoint] != minotaur)
            {
                //pathMinHuman.Add(curPoint);
                curPoint = saveRoad[curPoint];
            }

            if (map[curPoint.X, curPoint.Y] != Terrain.Water)
                minotaur = curPoint;
        }

        private List<Point> SearchMinimalPath(Dictionary<Terrain, int> weigthsTable, Terrain[,] mapTerrains, Point startPoint, Point finishPoint)
        {
            var heap = new Heap();
            var usedCells = new HashSet<Point>();
            var saveRoad = new Dictionary<Point, Point>();
            var distanceTo = new Dictionary<Point, int>();
            
            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            distanceTo[startPoint] = 0;
            
            heap.Add(startPoint, distanceTo[startPoint]);

            while (heap.Any())
            {
                Point currentPoint = heap.Pop();
                int currentPointWeight = weigthsTable[map[currentPoint.X, currentPoint.Y]];

                usedCells.Add(currentPoint);

                for (int i = 0; i < dx.Length; i++)
                {
                    Point newPoint = new Point(currentPoint.X + dx[i], currentPoint.Y + dy[i]);

                    if (!distanceTo.ContainsKey(newPoint) || distanceTo[newPoint] > distanceTo[currentPoint] + currentPointWeight)
                    {
                        distanceTo[newPoint] = distanceTo[currentPoint] + currentPointWeight;
                        saveRoad[newPoint] = currentPoint;
                    }

                    if (!usedCells.Contains(newPoint) && weigthsTable[mapTerrains[newPoint.X, newPoint.Y]] != Inf)
                        heap.Add(newPoint, distanceTo[newPoint]);
                }

            }

            var MinimalRoad = new List<Point>();

            if (saveRoad.ContainsKey(finishPoint))
            {
                Point currentPoint = finishPoint;

                while (saveRoad[currentPoint] != startPoint)
                {                
                    currentPoint = saveRoad[currentPoint];
                    MinimalRoad.Add(currentPoint);
                }
            }

            MinimalRoad.Reverse();

            return MinimalRoad;
        }

        private void Solve()
        {
            List<Point> humanMinRoad = SearchMinimalPath(humanPenaltysTable, map, human, exit);

            pathMinHuman = humanMinRoad;

            if (!humanMinRoad.Any())
                return;

            List<Point> mn = new List<Point>();
            int minSumWeigths = 10000000;

            foreach (var point in humanMinRoad)
            {
                List<Point> minotaurMinRoadToPoint = SearchMinimalPath(minotaurPenaltysTable, map, minotaur, point);

                int sum = 0;

                foreach (var point1 in minotaurMinRoadToPoint)
                    sum += minotaurPenaltysTable[map[point1.X, point1.Y]];

                if (sum < minSumWeigths && minotaurMinRoadToPoint.Any())
                {
                    minSumWeigths = sum;
                    mn = new List<Point>(minotaurMinRoadToPoint);
                }
            }

            pathMinMinotaur = mn;
        }

        //private void MinotaurMoveDijkstra()
        //{
        //    var heap = new Heap();
        //    var usedCells = new HashSet<Point>();
        //    var saveRoad = new Dictionary<Point, Point>();
        //    var distanceTo = new Dictionary<Point, int>();

        //    int[] dx = { 1, -1, 0, 0 };
        //    int[] dy = { 0, 0, 1, -1 };

        //    distanceTo[minotaur] = 0;

        //    heap.Add(minotaur, distanceTo[minotaur]);

        //    while (heap.Any())
        //    {
        //        Point currentPoint = heap.Pop();
        //        int currentPointWeight = minotaurPenaltysTable[map[currentPoint.X, currentPoint.Y]];

        //        usedCells.Add(currentPoint);

        //        for (int i = 0; i < dx.Length; i++)
        //        {
        //            Point newPoint = new Point(currentPoint.X + dx[i], currentPoint.Y + dy[i]);
        //            Terrain newPointType = map[newPoint.X, newPoint.Y];

        //            if (newPointType == Terrain.Wall || newPoint == exit)
        //                continue;

        //            if ((!distanceTo.ContainsKey(newPoint) || distanceTo[newPoint] > distanceTo[currentPoint] + currentPointWeight) && (newPoint == human || newPointType != Terrain.Water))
        //            {
        //                distanceTo[newPoint] = distanceTo[currentPoint] + currentPointWeight;

        //                if (!saveRoad.ContainsKey(newPoint))
        //                    saveRoad.Add(newPoint, currentPoint);
        //                else
        //                    saveRoad[newPoint] = currentPoint;
        //            }

        //            if (!usedCells.Contains(newPoint) && newPointType != Terrain.Water)
        //                heap.Add(newPoint, distanceTo[newPoint]);
        //        }

        //    }

        //    // восстанавливаю маршрут
        //    Point curPoint = human;

        //    while (saveRoad[curPoint] != minotaur)
        //    {
        //        pathMinHuman.Add(curPoint);
        //        curPoint = saveRoad[curPoint];
        //    }

        //    if (map[curPoint.X, curPoint.Y] != Terrain.Water)
        //        minotaur = curPoint;
        //}

        private void Restart()
        {
            map = new Terrain[n, m];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    map[i, j] = defaultMap[i, j];

            humanUsedCells = new bool[n, m];

            human = defaultPositionHuman;
            minotaur = defaultPositionMinotaur;

            humanPenaltyForCrossing = 1;
            minotaurPenaltyForCrossing = 1;
        }
    }
}
