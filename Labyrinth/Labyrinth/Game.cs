using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Labyrinth
{
    public class Game
    {
        private const int Inf = int.MaxValue;

        private readonly int n;
        private readonly int m;

        private readonly Point defaultPositionMinotaur;
        private readonly Point defaultPositionHuman;

        private readonly Terrain[,] defaultMap;
        private Terrain[,] map;

        private readonly Dictionary<Terrain, int> minotaurTablePenalties;
        private readonly Dictionary<Terrain, int> humanTablePenalties;

        private bool[,] humanUsedCells;

        private readonly Point exit;

        private Point minotaur;
        private Point human;

        private int humanPenaltyForCrossing;
        private int minotaurPenaltyForCrossing;

        private List<Point> pathMinMinotaur = new List<Point>();
        private List<Point> pathMinHuman = new List<Point>();

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
                            defaultMap[i, j] = Terrain.Empty;
                            break;

                        case 'W':
                            defaultMap[i, j] = Terrain.Water;
                            break;

                        case 'T':
                            defaultMap[i, j] = Terrain.Tree;
                            break;

                        case 'M':
                            defaultMap[i, j] = Terrain.Empty;
                            defaultPositionMinotaur = new Point(i, j);
                            break;

                        case 'H':
                            defaultMap[i, j] = Terrain.Empty;
                            defaultPositionHuman = new Point(i, j);
                            break;

                        case 'Q':
                            defaultMap[i, j] = Terrain.Exit;
                            exit = new Point(i, j);
                            break;
                    }
                }

            humanTablePenalties = new Dictionary<Terrain, int>()
            {
                {Terrain.Empty, 1},
                {Terrain.Water, 2},
                {Terrain.Tree, Inf},
                {Terrain.Wall, Inf},
                {Terrain.Exit, 1}
            };

            minotaurTablePenalties = new Dictionary<Terrain, int>()
            {
                {Terrain.Empty, 1},
                {Terrain.Tree, 2},
                {Terrain.Water, Inf},
                {Terrain.Wall, Inf},
                {Terrain.Exit, 1}
            };

            Restart();
        }

        public void Paint(PaintEventArgs e, int cellSize)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

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

                        case Terrain.Empty:
                            //if (humanUsedCells[i, j] && currentPoint != minotaur && currentPoint != human)
                            //    e.Graphics.FillEllipse(Brushes.Red, rectangleForDrawingDot);

                            break;
                    }
                }

            if (minotaur != exit)
                e.Graphics.DrawString("Q", font, Brushes.Red, new PointF(exit.Y * cellSize + cellSize / 2 - 8, exit.X * cellSize + cellSize / 2 - 8));

            e.Graphics.DrawString("H", font, Brushes.Orange, new PointF(human.Y * cellSize + cellSize / 2 - 8, human.X * cellSize + cellSize / 2 - 8));
            e.Graphics.DrawString("M", font, Brushes.Magenta, new PointF(minotaur.Y * cellSize + cellSize / 2 - 8, minotaur.X * cellSize + cellSize / 2 - 8));

            for (int i = 0; i <= n; i++)
                e.Graphics.DrawLine(Pens.Black, 0, i * cellSize, m * cellSize, i * cellSize);

            for (int j = 0; j <= m; j++)
                e.Graphics.DrawLine(Pens.Black, j * cellSize, 0, j * cellSize, n * cellSize);

            foreach (var point in pathMinHuman)
            {
                e.Graphics.FillEllipse(Brushes.Green, point.Y * cellSize + cellSize / 2 - 2, point.X * cellSize + cellSize / 2 - 2, 5, 5);
            }

            foreach (var point in pathMinMinotaur)
            {
                e.Graphics.FillEllipse(Brushes.Red, point.Y * cellSize + cellSize / 2 - 2, point.X * cellSize + cellSize / 2 - 2, 5, 5);
            }
        }

        public void Move(Direction direction, Mode mode)
        {
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

            Point directionPoint;

            switch (direction)
            {
                case Direction.Up:
                    directionPoint = new Point(-1, 0);
                    break;

                case Direction.Down:
                    directionPoint = new Point(1, 0);
                    break;

                case Direction.Left:
                    directionPoint = new Point(0, -1);
                    break;

                case Direction.Right:
                    directionPoint = new Point(0, 1);
                    break;

                default:
                    directionPoint = new Point();
                    break;
            }

            Point nextPoint = human + directionPoint;
            Terrain nextPointType = map[nextPoint.X, nextPoint.Y];

            if (nextPointType == Terrain.Wall || nextPointType == Terrain.Tree)
                return false;

            if (nextPoint == exit)
            {
                Restart(); // won
                return false;
            }

            if (nextPoint == minotaur)
            {
                Restart(); // lose
                return false;
            }

            humanUsedCells[human.X, human.Y] = true;
            human = nextPoint;
            humanPenaltyForCrossing = humanTablePenalties[map[human.X, human.Y]];

            return true;
        }

        private void MoveMinotaur(Mode mode)
        {
            if (minotaurPenaltyForCrossing != 1)
            {
                minotaurPenaltyForCrossing--;
                return;
            }

            var waveAlgorithm = new WaveAlgorithm(map);

            switch (mode)
            {
                case Mode.EazyCrazy:
                    minotaur = waveAlgorithm.GetRoadWithDfs(minotaurTablePenalties, minotaur, human).First();
                    pathMinHuman = waveAlgorithm.GetRoadWithDfs(humanTablePenalties, human, exit);
                    pathMinMinotaur = waveAlgorithm.GetRoadWithDfs(minotaurTablePenalties, minotaur, human);
                    break;

                case Mode.Eazy:
                    minotaur = waveAlgorithm.GetRoadWithBfs(minotaurTablePenalties, minotaur, human).First();
                    pathMinHuman = waveAlgorithm.GetRoadWithBfs(humanTablePenalties, human, exit);
                    pathMinMinotaur = waveAlgorithm.GetRoadWithBfs(minotaurTablePenalties, minotaur, human);
                    break;

                case Mode.Normal:
                    minotaur = waveAlgorithm.GetRoadWithDijkstra(minotaurTablePenalties, minotaur, human).First();
                    pathMinHuman = waveAlgorithm.GetRoadWithDijkstra(humanTablePenalties, human, exit);
                    pathMinMinotaur = waveAlgorithm.GetRoadWithDijkstra(minotaurTablePenalties, minotaur, human);
                    break;

                case Mode.Hard:
                    minotaur = waveAlgorithm.GetRoadWithSmartDijkstra(minotaurTablePenalties, humanTablePenalties, minotaur, human, exit).First();
                    pathMinHuman = waveAlgorithm.GetRoadWithDijkstra(humanTablePenalties, human, exit);
                    pathMinMinotaur = waveAlgorithm.GetRoadWithSmartDijkstra(minotaurTablePenalties, humanTablePenalties, minotaur, human, exit);
                    break;
            }

            if (minotaur == human)
            {
                Restart(); // lose
                return;
            }

            minotaurPenaltyForCrossing = minotaurTablePenalties[map[minotaur.X, minotaur.Y]];

            if (map[minotaur.X, minotaur.Y] == Terrain.Tree)
                map[minotaur.X, minotaur.Y] = Terrain.Empty;
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

            humanPenaltyForCrossing = 1;
            minotaurPenaltyForCrossing = 1;
        }
    }
}
