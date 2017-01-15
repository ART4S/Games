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

namespace Labyrinth
{
    public class Game
    {
        private readonly int n;
        private readonly int m;

        private readonly Map[,] defaultWeights;

        private readonly Point defaultPositionMinotaur;
        private readonly Point defaultPositionHuman;

        private Map[,] weights;

        private bool[,] HumanUsedCells;

        private Point Minotaur;
        private Point Human;
        private Point Exit;

        private int PenaltyForCrossing;

        public Game(char[,] labyrinth, int n, int m)
        {
            this.n = n;
            this.m = m;

            defaultWeights = new Map[n, m];

            for(int i = 0; i < n; ++i)
                for(int j = 0; j < m; ++j)
                {
                    switch (labyrinth[i, j])
                    {
                        case 'X':
                            defaultWeights[i, j] = Map.Wall;
                            break;

                        case ' ':
                            defaultWeights[i, j] = Map.Path;
                            break;

                        case 'W':
                            defaultWeights[i, j] = Map.Water;
                            break;

                        case 'T':
                            defaultWeights[i, j] = Map.Tree;
                            break;

                        case 'M':
                            defaultWeights[i, j] = Map.Path;
                            defaultPositionMinotaur = new Point(i, j);
                            break;

                        case 'H':
                            defaultWeights[i, j] = Map.Path;
                            defaultPositionHuman = new Point(i, j);
                            break;

                        case 'Q':
                            defaultWeights[i, j] = Map.Exit;
                            Exit = new Point(i, j);
                            break;
                    }
                }

            Restart();
        }

        public void Paint(PaintEventArgs e, int cellSize)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    PointF currentPoint = new PointF(j * cellSize + cellSize / 2 - 8, i * cellSize + cellSize / 2 - 8);
                    RectangleF currentRectangle = new RectangleF(j * cellSize, i * cellSize, cellSize, cellSize);

                    switch (weights[i, j])
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
                            if (HumanUsedCells[i, j] && !isMinotaur(new Point(i, j)) && !isHuman(new Point(i, j)))
                                e.Graphics.FillEllipse(Brushes.Red, j * cellSize + cellSize / 2 - 2, i * cellSize + cellSize / 2 - 2, 5, 5);
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
        }

        public void Move(Direction direction, Mode mode)
        {
            MoveHuman(direction);
            MoveMinotaur(mode);
        }
        // TODO: !
        private void MoveHuman(Direction direction)
        {
            if (PenaltyForCrossing != 1)
            {
                PenaltyForCrossing--;
                return;
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
                return;

            if (isMinotaur(NextPoint))
            {
                Restart();
                // lose
                return;
            }

            if (isExit(NextPoint))
            {
                Restart();
                // won
                return;
            }

            HumanUsedCells[Human.X, Human.Y] = true;
            Human = NextPoint;
            PenaltyForCrossing = (int)weights[Human.X, Human.Y];
        }
        // TODO: !
        private void MoveMinotaur(Mode mode)
        {
            switch (mode)
            {
                case Mode.Eazy_Crazy:
                    break;

                case Mode.Eazy:
                    break;

                case Mode.Normal:
                    break;

                case Mode.Smart_Hard:
                    break;
            }
        }

        private bool isWall(Point point)
        {
            return weights[point.X, point.Y] == Map.Wall;
        }

        private bool isTree(Point point)
        {
            return weights[point.X, point.Y] == Map.Tree;
        }

        private bool isMinotaur(Point point)
        {
            return point == Minotaur;
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
            HumanUsedCells = new bool[n, m];

            for (int i = 0; i < n; ++i)
                for (int j = 0; j < m; ++j)
                    HumanUsedCells[i, j] = false;

            weights = defaultWeights;
            Human = defaultPositionHuman;
            Minotaur = defaultPositionMinotaur;
            PenaltyForCrossing = 1;
        }
    }
}
