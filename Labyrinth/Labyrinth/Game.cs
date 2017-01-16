using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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

        private Map[,] defaultMap;
        private Map[,] map;

        private Dictionary<Map, int> MinotaurPenaltysTable;
        private Dictionary<Map, int> HumanPenaltysTable;

        private bool[,] HumanUsedCells;

        private Point Minotaur;
        private Point Human;
        private Point Exit;

        private int HumanPenaltyForCrossing;
        private int MinotaurPenaltyForCrossing;

        private List<Point> pathMin = new List<Point>();

        public Game(char[,] labyrinth, int n, int m)
        {
            this.n = n;
            this.m = m;

            defaultMap = new Map[n, m];

            for (int i = 0; i < n; ++i)
                for(int j = 0; j < m; ++j)
                {
                    switch (labyrinth[i, j])
                    {
                        case 'X':
                            defaultMap[i, j] = Map.Wall;
                            break;

                        case ' ':
                            defaultMap[i, j] = Map.Path;
                            break;

                        case 'W':
                            defaultMap[i, j] = Map.Water;
                            break;

                        case 'T':
                            defaultMap[i, j] = Map.Tree;
                            break;

                        case 'M':
                            defaultMap[i, j] = Map.Path;
                            defaultPositionMinotaur = new Point(i, j);
                            break;

                        case 'H':
                            defaultMap[i, j] = Map.Path;
                            defaultPositionHuman = new Point(i, j);
                            break;

                        case 'Q':
                            defaultMap[i, j] = Map.Exit;
                            Exit = new Point(i, j);
                            break;
                    }
                }

            Restart();
        }

        public void Paint(PaintEventArgs e, int cellSize)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Font font = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    PointF currentPoint = new PointF(j * cellSize + cellSize / 2 - 8, i * cellSize + cellSize / 2 - 8);
                    RectangleF currentRectangle = new RectangleF(j * cellSize, i * cellSize, cellSize, cellSize);

                    switch (map[i, j])
                    {
                        case Map.Water:
                            e.Graphics.FillRectangle(Brushes.Blue, currentRectangle);
                            break;

                        case Map.Tree:
                            e.Graphics.DrawString("T", font, Brushes.Green, currentPoint);
                            break;

                        case Map.Wall:
                            e.Graphics.FillRectangle(Brushes.Black, currentRectangle);
                            break;

                        case Map.Path:
                            //if (HumanUsedCells[i, j] && !isMinotaur(new Point(i, j)) && !isHuman(new Point(i, j)))
                            //    e.Graphics.FillEllipse(Brushes.Red, j * cellSize + cellSize / 2 - 2, i * cellSize + cellSize / 2 - 2, 5, 5);
                            break;
                    }
                }

            e.Graphics.DrawString("Q", font, Brushes.Red, new PointF(Exit.Y * cellSize + cellSize / 2 - 8, Exit.X * cellSize + cellSize / 2 - 8));
            e.Graphics.DrawString("H", font, Brushes.Orange, new PointF(Human.Y * cellSize + cellSize / 2 - 8, Human.X * cellSize + cellSize / 2 - 8));
            e.Graphics.DrawString("M", font, Brushes.Magenta, new PointF(Minotaur.Y * cellSize + cellSize / 2 - 8, Minotaur.X * cellSize + cellSize / 2 - 8));

            for (int i = 0; i <= n; i++)
                e.Graphics.DrawLine(Pens.Black, 0, i * cellSize, m * cellSize, i * cellSize);

            for (int j = 0; j <= m; j++)
                e.Graphics.DrawLine(Pens.Black, j * cellSize, 0, j * cellSize, n * cellSize);

            foreach(var elem in pathMin)
            {
                e.Graphics.FillEllipse(Brushes.Blue, elem.Y * cellSize + cellSize / 2 - 2, elem.X * cellSize + cellSize / 2 - 2, 5, 5);
            }
        }

        public void Move(Direction direction, Mode mode)
        {
            // если человек сделал шаг, шаг делает и минотавр
            if (MoveHuman(direction))
                MoveMinotaur(mode);
        }

        private bool MoveHuman(Direction direction)
        {
            if (HumanPenaltyForCrossing != 0)
            {
                HumanPenaltyForCrossing--;
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

            Point NextPoint = new Point(Human.X + dx, Human.Y + dy);

            if (isWall(NextPoint) || isTree(NextPoint))
                return false;

            if (isMinotaur(NextPoint))
            {
                Restart(); // lose
                return false;
            }

            if (isExit(NextPoint))
            {
                Restart(); // won
                return false;
            }

            HumanUsedCells[Human.X, Human.Y] = true;
            Human = NextPoint;
            HumanPenaltyForCrossing = HumanPenaltysTable[map[Human.X, Human.Y]];

            return true;
        }

        private void MoveMinotaur(Mode mode)
        {
            if (MinotaurPenaltyForCrossing != 0)
            {
                MinotaurPenaltyForCrossing--;
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
                    MinotaurMoveDijkstra();
                    break;

                case Mode.Hard:
                    break;
            }

            if (isHuman(Minotaur))
            {
                Restart(); // lose
                return;
            }

            MinotaurPenaltyForCrossing = MinotaurPenaltysTable[map[Minotaur.X, Minotaur.Y]];

            if (isTree(Minotaur))
                map[Minotaur.X, Minotaur.Y] = Map.Path;
        }

        private void MinotaurMoveRandomDirrection()
        {
            Random random = new Random();

            int[] dx = { 1,-1, 0, 0 };
            int[] dy = { 0, 0, 1,-1 };

            while (true)
            {
                int i = random.Next(dx.Length);
                Point NewPoint = new Point(Minotaur.X + dx[i], Minotaur.Y + dy[i]);

                if (!isWater(NewPoint) && !isWall(NewPoint) && !isExit(NewPoint))
                {
                    Minotaur = NewPoint;
                    return;
                }
            }
        }

        private void MinotaurMoveBfs()
        {
            var queue = new Queue<Point>();
            var usedCells = new Dictionary<Point, bool>();
            var saveRoad = new Point[n, m];

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            queue.Enqueue(Minotaur);

            while (queue.Any())
            {
                Point currentCell = queue.Dequeue();

                if (currentCell == Human)
                {
                    // восстанавливаю путь
                    while (saveRoad[currentCell.X, currentCell.Y] != Minotaur)
                        currentCell = saveRoad[currentCell.X, currentCell.Y];

                    if (!isWater(currentCell))
                        Minotaur = currentCell;

                    return;
                }

                for (int i = 0; i < dx.Length; ++i)
                {
                    Point NewPoint = new Point(currentCell.X + dx[i], currentCell.Y + dy[i]);

                    if (!isWater(NewPoint) && !isWall(NewPoint) && !isExit(NewPoint) && !usedCells.ContainsKey(NewPoint))
                    {
                        saveRoad[NewPoint.X, NewPoint.Y] = currentCell;
                        usedCells.Add(NewPoint, true);
                        queue.Enqueue(NewPoint);
                    }

                    if (isWater(NewPoint) && NewPoint == Human)
                    {
                        saveRoad[NewPoint.X, NewPoint.Y] = currentCell;
                        queue.Enqueue(NewPoint);
                    }
                }
            }
        }

        private void MinotaurMoveDijkstra()
        {
            var heap = new Heap();
            var usedCells = new Dictionary<Point, bool>();
            var saveRoad = new Point[n, m];

            int[] dx = { 1, -1, 0, 0 };
            int[] dy = { 0, 0, 1, -1 };

            heap.Add(Minotaur, MinotaurPenaltysTable[map[Minotaur.X, Minotaur.Y]] + 1);

            while (heap.Any())
            {
                Point currentCell = heap.Front();

                heap.Pop();
                pathMin.Clear();
                if (currentCell == Human)
                {
                    // восстанавливаю путь
                    while (saveRoad[currentCell.X, currentCell.Y] != Minotaur)
                    {
                        currentCell = saveRoad[currentCell.X, currentCell.Y];
                        pathMin.Add(currentCell);
                    }

                    if (!isWater(currentCell))
                        Minotaur = currentCell;

                    return;
                }

                for (int i = 0; i < dx.Length; ++i)
                {
                    Point NewPoint = new Point(currentCell.X + dx[i], currentCell.Y + dy[i]);

                    if (!isWater(NewPoint) && !isWall(NewPoint) && !isExit(NewPoint) && !usedCells.ContainsKey(NewPoint))
                    {
                        saveRoad[NewPoint.X, NewPoint.Y] = currentCell;
                        usedCells.Add(NewPoint, true);
                        heap.Add(NewPoint, MinotaurPenaltysTable[map[NewPoint.X, NewPoint.Y]] + 1);
                    }
                }
            }
        }

        private bool isWall(Point point)
        {
            return map[point.X, point.Y] == Map.Wall;
        }

        private bool isTree(Point point)
        {
            return map[point.X, point.Y] == Map.Tree;
        }

        private bool isMinotaur(Point point)
        {
            return point == Minotaur;
        }

        private bool isWater(Point point)
        {
            return map[point.X, point.Y] == Map.Water;
        }

        private bool isExit(Point point)
        {
            return point == Exit;
        }

        private bool isHuman(Point point)
        {
            return point == Human;
        }

        private void Restart()
        {
            const int Inf = 100000;

            HumanPenaltysTable = new Dictionary<Map, int>()
            {
                {Map.Path, 0},
                {Map.Water, 1},
                {Map.Tree, Inf},
                {Map.Wall, Inf},
                {Map.Exit, Inf}
            };

            MinotaurPenaltysTable = new Dictionary<Map, int>()
            {
                {Map.Path, 0},
                {Map.Tree, 1},
                {Map.Water, Inf},
                {Map.Wall, Inf},
                {Map.Exit, Inf}
            };

            HumanUsedCells = new bool[n, m];

            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    HumanUsedCells[i, j] = false;

            map = defaultMap;

            Human = defaultPositionHuman;
            Minotaur = defaultPositionMinotaur;

            HumanPenaltyForCrossing = 0;
            MinotaurPenaltyForCrossing = 0;
        }
    }
}
