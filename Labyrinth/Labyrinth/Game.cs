using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Labyrinth
{
    public class Game
    {
        private const int Inf = int.MaxValue;

        private readonly int height;
        private readonly int width;

        private readonly Mode mode;

        private readonly Terrain[,] defaultGameField;
        private Terrain[,] gameField;

        private bool[,] humanUsedCells;

        private readonly Dictionary<Terrain, int> minotaurPenaltiesTable;
        private readonly Dictionary<Terrain, int> humanPenaltiesTable;

        private int humanPenalty;
        private int minotaurPenalty;

        private readonly Point minotaurDefaultPoint;
        private readonly Point humanDefaultPoint;

        private readonly Point exitPoint;
        private Point minotaurPoint;
        private Point humanPoint;

        public int UsedCellsCounter { get; private set; }

        public Game(char[,] gameField, int height, int width, Mode mode)
        {
            this.height = height;
            this.width = width;
            this.mode = mode;

            defaultGameField = new Terrain[height, width];

            for (int i = 0; i < height; i++)
                for(int j = 0; j < width; j++)
                {
                    switch (gameField[i, j])
                    {
                        case 'X':
                            defaultGameField[i, j] = Terrain.Wall;
                            break;

                        case ' ':
                            defaultGameField[i, j] = Terrain.Empty;
                            break;

                        case 'W':
                            defaultGameField[i, j] = Terrain.Water;
                            break;

                        case 'T':
                            defaultGameField[i, j] = Terrain.Tree;
                            break;

                        case 'M':
                            defaultGameField[i, j] = Terrain.Empty;
                            minotaurDefaultPoint = new Point(i, j);
                            break;

                        case 'H':
                            defaultGameField[i, j] = Terrain.Empty;
                            humanDefaultPoint = new Point(i, j);
                            break;

                        case 'Q':
                            defaultGameField[i, j] = Terrain.Exit;
                            exitPoint = new Point(i, j);
                            break;
                    }
                }

            humanPenaltiesTable = new Dictionary<Terrain, int>()
            {
                {Terrain.Empty, 1},
                {Terrain.Water, 2},
                {Terrain.Tree, Inf},
                {Terrain.Wall, Inf},
                {Terrain.Exit, 1}
            };

            minotaurPenaltiesTable = new Dictionary<Terrain, int>()
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

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    PointF pointForDrawingSymbol = new PointF(j * cellSize + cellSize / 2 - 8, i * cellSize + cellSize / 2 - 8);
                    RectangleF rectangleForDrawingDot = new RectangleF(j * cellSize + cellSize / 2 - 2, i * cellSize + cellSize / 2 - 2, 5, 5);

                    RectangleF currentRectangle = new RectangleF(j * cellSize, i * cellSize, cellSize, cellSize);
                    Point currentPoint = new Point(i, j);

                    switch (gameField[i, j])
                    {
                        case Terrain.Water:
                            e.Graphics.FillRectangle(Brushes.Blue, currentRectangle);

                            if (humanUsedCells[i, j] && currentPoint != humanPoint)
                                e.Graphics.FillEllipse(Brushes.Red, rectangleForDrawingDot);

                            break;

                        case Terrain.Tree:
                            e.Graphics.DrawString("T", font, Brushes.Green, pointForDrawingSymbol);
                            break;

                        case Terrain.Wall:
                            e.Graphics.FillRectangle(Brushes.Black, currentRectangle);
                            break;

                        case Terrain.Empty:
                            if (humanUsedCells[i, j] && currentPoint != minotaurPoint && currentPoint != humanPoint)
                                e.Graphics.FillEllipse(Brushes.Red, rectangleForDrawingDot);

                            break;
                    }
                }

            if (minotaurPoint != exitPoint)
                e.Graphics.DrawString("Q", font, Brushes.Red, new PointF(exitPoint.Y * cellSize + cellSize / 2 - 8, exitPoint.X * cellSize + cellSize / 2 - 8));

            e.Graphics.DrawString("H", font, Brushes.Orange, new PointF(humanPoint.Y * cellSize + cellSize / 2 - 8, humanPoint.X * cellSize + cellSize / 2 - 8));
            e.Graphics.DrawString("M", font, Brushes.Magenta, new PointF(minotaurPoint.Y * cellSize + cellSize / 2 - 8, minotaurPoint.X * cellSize + cellSize / 2 - 8));

            for (int i = 0; i <= height; i++)
                e.Graphics.DrawLine(Pens.Black, 0, i * cellSize, width * cellSize, i * cellSize);

            for (int j = 0; j <= width; j++)
                e.Graphics.DrawLine(Pens.Black, j * cellSize, 0, j * cellSize, height * cellSize);
        }

        public void Move(Direction direction)
        {
            if (MoveHuman(direction))
                MoveMinotaur();
        }

        private bool MoveHuman(Direction direction)
        {
            if (humanPenalty != 1)
            {
                humanPenalty--;
                return true;
            }

            Point nextPoint = humanPoint + direction.ToPoint();
            Terrain nextPointType = gameField[nextPoint.X, nextPoint.Y];

            if (humanPenaltiesTable[nextPointType] == Inf)
                return false;

            if (nextPoint == exitPoint)
            {
                Restart(); // won
                return false;
            }

            if (nextPoint == minotaurPoint)
            {
                Restart(); // lose
                return false;
            }

            humanPoint = nextPoint;
            humanPenalty = humanPenaltiesTable[gameField[humanPoint.X, humanPoint.Y]];

            if (humanUsedCells[humanPoint.X, humanPoint.Y] == false)
            {
                humanUsedCells[humanPoint.X, humanPoint.Y] = true;
                UsedCellsCounter++;
            }

            return true;
        }

        private void MoveMinotaur()
        {
            if (minotaurPenalty != 1)
            {
                minotaurPenalty--;
                return;
            }

            var pathfinder = new Pathfinder(gameField);

            switch (mode)
            {
                case Mode.EazyCrazy:
                    minotaurPoint = pathfinder.FindPathWithDfs(minotaurPenaltiesTable, minotaurPoint, humanPoint).First();
                    break;

                case Mode.Eazy:
                    minotaurPoint = pathfinder.FindPathWithBfs(minotaurPenaltiesTable, minotaurPoint, humanPoint).First();
                    break;

                case Mode.Normal:
                    minotaurPoint = pathfinder.FindPathWithDijkstra(minotaurPenaltiesTable, minotaurPoint, humanPoint).First();
                    break;

                case Mode.Hard:
                    minotaurPoint = pathfinder.FindPathWithSmartDijkstra(minotaurPenaltiesTable, humanPenaltiesTable, minotaurPoint, humanPoint, exitPoint).First();
                    break;
            }

            if (minotaurPoint == humanPoint)
            {
                Restart(); // lose
                return;
            }

            minotaurPenalty = minotaurPenaltiesTable[gameField[minotaurPoint.X, minotaurPoint.Y]];

            if (gameField[minotaurPoint.X, minotaurPoint.Y] == Terrain.Tree)
                gameField[minotaurPoint.X, minotaurPoint.Y] = Terrain.Empty;
        }

        private void Restart()
        {
            gameField = new Terrain[height, width];

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    gameField[i, j] = defaultGameField[i, j];

            humanPoint = humanDefaultPoint;
            minotaurPoint = minotaurDefaultPoint;

            humanPenalty = 1;
            minotaurPenalty = 1;

            humanUsedCells = new bool[height, width];

            humanUsedCells[humanPoint.X, humanPoint.Y] = true;
            UsedCellsCounter = 1;
        }
    }
}
