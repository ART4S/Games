using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Labyrinth
{
    public class Game
    {
        private const int MaxPenalty = int.MaxValue;
        private const int MinPenalty = 1;

        private readonly int height;
        private readonly int width;

        private readonly Terrain[,] defaultGameField;
        private Terrain[,] gameField;

        private bool[,] humanUsedCells;
        
        private readonly Dictionary<Terrain, int> minotaurPenaltiesTable;
        private readonly Dictionary<Terrain, int> humanPenaltiesTable;

        private int humanPenaltyForCellCrossing;
        private int minotaurPenaltyForCellCrossing;

        private readonly Point minotaurDefaultPoint;
        private readonly Point humanDefaultPoint;

        private readonly Point exitPoint;
        private Point minotaurPoint;
        private Point humanPoint;

        private readonly Pathfinder pathfinder;

        public Mode Mode { get; private set; }
        public int UsedCellsCounter { get; private set; }

        public event EventHandler<EventArgs> VictoryEvent;
        public event EventHandler<EventArgs> LoseEvent;

        public Game(char[,] gameField, int height, int width, Mode mode)
        {
            this.height = height;
            this.width = width;
         
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
                {Terrain.Empty, MinPenalty},
                {Terrain.Water, MinPenalty + 1},
                {Terrain.Tree, MaxPenalty},
                {Terrain.Wall, MaxPenalty},
                {Terrain.Exit, MinPenalty}
            };

            minotaurPenaltiesTable = new Dictionary<Terrain, int>()
            {
                {Terrain.Empty, MinPenalty},
                {Terrain.Tree, MinPenalty + 1},
                {Terrain.Water, MaxPenalty},
                {Terrain.Wall, MaxPenalty},
                {Terrain.Exit, MinPenalty}
            };

            pathfinder = new Pathfinder(MaxPenalty);

            Restart(mode);
        }

        public void Restart(Mode mode)
        {
            Mode = mode;

            gameField = new Terrain[height, width];

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    gameField[i, j] = defaultGameField[i, j];

            humanPoint = humanDefaultPoint;
            minotaurPoint = minotaurDefaultPoint;

            humanPenaltyForCellCrossing = MinPenalty;
            minotaurPenaltyForCellCrossing = MinPenalty;

            humanUsedCells = new bool[height, width];

            humanUsedCells[humanPoint.X, humanPoint.Y] = true;
            UsedCellsCounter = 1;
        }

        public void Paint(Graphics graphics, int cellSize)
        {          
            Font font = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    RectangleF rectangleForDrawingDot = new RectangleF(j * cellSize + cellSize / 2 - 2, i * cellSize + cellSize / 2 - 2, 5, 5);
                    RectangleF currentRectangle = new RectangleF(j * cellSize, i * cellSize, cellSize, cellSize);
                    Point currentPoint = new Point(i, j);

                    switch (gameField[i, j])
                    {
                        case Terrain.Water:
                            graphics.FillRectangle(Brushes.Blue, currentRectangle);

                            if (humanUsedCells[i, j] && currentPoint != humanPoint)
                                graphics.FillEllipse(Brushes.Red, rectangleForDrawingDot);

                            break;

                        case Terrain.Tree:
                            graphics.FillRectangle(Brushes.Green, currentRectangle);
                            break;

                        case Terrain.Wall:
                            graphics.FillRectangle(Brushes.Black, currentRectangle);
                            break;

                        case Terrain.Empty:
                            if (humanUsedCells[i, j] && currentPoint != minotaurPoint && currentPoint != humanPoint)
                                graphics.FillEllipse(Brushes.Red, rectangleForDrawingDot);

                            break;
                    }
                }

            Func<Point, PointF> getPointForDrawingSymbol = point => new PointF(point.Y * cellSize + cellSize / 2 - 8, point.X * cellSize + cellSize / 2 - 8);

            if (minotaurPoint != exitPoint)
                graphics.DrawString("Q", font, Brushes.Red, getPointForDrawingSymbol(exitPoint));

            graphics.DrawString("H", font, Brushes.Orange, getPointForDrawingSymbol(humanPoint));
            graphics.DrawString("M", font, Brushes.Magenta, getPointForDrawingSymbol(minotaurPoint));
            
            for (int i = 0; i <= height; i++)
                graphics.DrawLine(Pens.Black, 0, i * cellSize, width * cellSize, i * cellSize);

            for (int j = 0; j <= width; j++)
                graphics.DrawLine(Pens.Black, j * cellSize, 0, j * cellSize, height * cellSize);
        }

        public void Move(Direction direction)
        {
            if (TryMoveHumanPoint(direction))
                MoveMinotaurPoint();
        }

        private bool TryMoveHumanPoint(Direction direction)
        {
            Point nextPoint = humanPoint + direction.ToPoint();
            Terrain nextPointType = gameField[nextPoint.X, nextPoint.Y];

            if (humanPenaltyForCellCrossing != MinPenalty)
            {
                humanPenaltyForCellCrossing--;
                return true;
            }

            if (humanPenaltiesTable[nextPointType] == MaxPenalty)
                return false;

            if (nextPoint == exitPoint)
            {
                Restart(Mode);
                VictoryEvent(this, EventArgs.Empty);

                return false;
            }

            if (nextPoint == minotaurPoint)
            {
                Restart(Mode);
                LoseEvent(this, EventArgs.Empty);

                return false;
            }

            humanPoint = nextPoint;
            humanPenaltyForCellCrossing = humanPenaltiesTable[nextPointType];

            if (!humanUsedCells[humanPoint.X, humanPoint.Y])
            {
                humanUsedCells[humanPoint.X, humanPoint.Y] = true;
                UsedCellsCounter++;
            }

            return true;
        }

        private void MoveMinotaurPoint()
        {
            if (minotaurPenaltyForCellCrossing != MinPenalty)
            {
                if (gameField[minotaurPoint.X, minotaurPoint.Y] == Terrain.Tree)
                    gameField[minotaurPoint.X, minotaurPoint.Y] = Terrain.Empty;

                minotaurPenaltyForCellCrossing--;

                return;
            }

            switch (Mode)
            {
                case Mode.EazyCrazy:
                    minotaurPoint = pathfinder.FindPathWithDfs(gameField, minotaurPenaltiesTable, minotaurPoint, humanPoint).First();
                    break;

                case Mode.Eazy:
                    minotaurPoint = pathfinder.FindPathWithBfs(gameField, minotaurPenaltiesTable, minotaurPoint, humanPoint).First();
                    break;

                case Mode.Normal:
                    minotaurPoint = pathfinder.FindPathWithDijkstra(gameField, minotaurPenaltiesTable, minotaurPoint, humanPoint).First();
                    break;

                case Mode.Hard:
                    minotaurPoint = pathfinder.FindPathWithSmartDijkstra(gameField, minotaurPenaltiesTable, humanPenaltiesTable, minotaurPoint, humanPoint, exitPoint).First();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(Mode), Mode, null);
            }

            minotaurPenaltyForCellCrossing = minotaurPenaltiesTable[gameField[minotaurPoint.X, minotaurPoint.Y]];

            if (minotaurPoint == humanPoint)
            {
                Restart(Mode);
                LoseEvent(this, EventArgs.Empty);
            }
        }
    }
}
