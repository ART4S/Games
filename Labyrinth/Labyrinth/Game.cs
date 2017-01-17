using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Labyrinth
{
    public class Game
    {
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

        private readonly List<Point> pathMin = new List<Point>();

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

            const int inf = 100000;

            humanPenaltysTable = new Dictionary<Terrain, int>()
            {
                {Terrain.Path, 0},
                {Terrain.Water, 1},
                {Terrain.Tree, inf},
                {Terrain.Wall, inf},
                {Terrain.Exit, inf}
            };

            minotaurPenaltysTable = new Dictionary<Terrain, int>()
            {
                {Terrain.Path, 0},
                {Terrain.Tree, 1},
                {Terrain.Water, inf},
                {Terrain.Wall, inf},
                {Terrain.Exit, inf}
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
                            if (humanUsedCells[i, j] && currentPoint != minotaur && currentPoint != human)
                                e.Graphics.FillEllipse(Brushes.Red, rectangleForDrawingDot);

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

            foreach (var elem in pathMin)
            {
                e.Graphics.FillEllipse(Brushes.Blue, elem.Y * cellSize + cellSize / 2 - 2, elem.X * cellSize + cellSize / 2 - 2, 5, 5);
            }
        }

        public void Move(Direction direction, Mode mode)
        {
            pathMin.Clear();
            if (MoveHuman(direction))
                MoveMinotaur(mode);
        }

        private bool MoveHuman(Direction direction)
        {
            if (humanPenaltyForCrossing != 0)
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
            Terrain point = map[nextPoint.X, nextPoint.Y];

            if (point == Terrain.Wall || point == Terrain.Tree)
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
            if (minotaurPenaltyForCrossing != 0)
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
                    MinotaurMoveD();
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
                Point currentCell = queue.Dequeue();

                if (currentCell == human)
                {
                    // восстанавливаю путь
                    while (saveRoad[currentCell] != minotaur)
                        currentCell = saveRoad[currentCell];

                    if (map[currentCell.X, currentCell.Y] != Terrain.Water)
                        minotaur = currentCell;

                    return;
                }

                for (int i = 0; i < dx.Length; i++)
                {
                    Point newPoint = new Point(currentCell.X + dx[i], currentCell.Y + dy[i]);
                    Terrain newPointType = map[newPoint.X, newPoint.Y];

                    if (newPointType != Terrain.Water && newPointType != Terrain.Wall && newPoint != exit && !usedCells.Contains(newPoint))
                    {
                        saveRoad[newPoint] = currentCell;
                        usedCells.Add(newPoint);
                        queue.Enqueue(newPoint);
                    }

                    if (newPointType == Terrain.Water && newPoint == human)
                    {
                        saveRoad[newPoint] = currentCell;
                        queue.Enqueue(newPoint);
                    }
                }
            }
        }

        private void MinotaurMoveD()
        {
            var queue = new SortedDictionary<Point, int>();
            var usedCells = new HashSet<Point>();
            var saveRoad = new Dictionary<Point, Point>();

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            queue.Add(minotaur, minotaurPenaltysTable[map[minotaur.X, minotaur.Y]]);

            while (queue.Any())
            {
                Point currentCell = queue.First().Key;

                queue.Remove(currentCell);

                if (currentCell == human)
                {
                    while (saveRoad[currentCell] != minotaur)
                        currentCell = saveRoad[currentCell];

                    if (map[currentCell.X, currentCell.Y] != Terrain.Water)
                        minotaur = currentCell;
                }

                for (int i = 0; i < dx.Length; i++)
                {
                    Point newPoint = new Point(currentCell.X + dx[i], currentCell.Y + dy[i]);
                    Terrain newPointType = map[newPoint.X, newPoint.Y];

                    if (!usedCells.Contains(newPoint))
                    {
                        saveRoad[newPoint] = currentCell;
                        usedCells.Add(newPoint);
                        queue.Add(newPoint, minotaurPenaltysTable[map[newPoint.X, newPoint.Y]]);
                    }

                    if (newPoint == human)
                    {
                        saveRoad[newPoint] = currentCell;
                        queue.Add(newPoint, minotaurPenaltysTable[map[newPoint.X, newPoint.Y]]);
                    }
                }
            }
        }

        private void Restart()
        {
            map = new Terrain[n, m];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    map[i, j] = defaultMap[i, j];

            humanUsedCells = new bool[n, m];

            human = defaultPositionHuman;
            minotaur = defaultPositionMinotaur;

            humanPenaltyForCrossing = 0;
            minotaurPenaltyForCrossing = 0;
        }
    }
}
