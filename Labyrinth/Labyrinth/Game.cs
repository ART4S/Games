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
        private Map[,] weights;

        private Point Minotaur;
        private Point Human;
        private Point Exit;

        private readonly int n;
        private readonly int m;

        public Game(char[,] labyrinth, int n, int m)
        {
            this.n = n;
            this.m = m;

            weights = new Map[n, m];

            for(int i = 0; i < n; ++i)
                for(int j = 0; j < m; ++j)
                {
                    switch (labyrinth[i, j])
                    {
                        case 'X':
                            weights[i, j] = Map.Wall;
                            break;

                        case ' ':
                            weights[i, j] = Map.Path;
                            break;

                        case 'W':
                            weights[i, j] = Map.Water;
                            break;

                        case 'T':
                            weights[i, j] = Map.Tree;
                            break;

                        case 'M':
                            weights[i, j] = Map.Path;
                            Minotaur = new Point(i, j);
                            break;

                        case 'H':
                            weights[i, j] = Map.Path;
                            Human = new Point(i, j);
                            break;

                        case 'Q':
                            weights[i, j] = Map.Exit;
                            Exit = new Point(i, j);
                            break;
                    }
                }
        }

        public void Paint(PaintEventArgs e, int cellSize)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    PointF currentPoint = new PointF(j * cellSize + cellSize / 6, i * cellSize + cellSize / 6);

                    switch (weights[i, j])
                    {
                        case Map.Water:
                            e.Graphics.DrawString("W", font, Brushes.Blue, currentPoint);
                            break;

                        case Map.Tree:
                            e.Graphics.DrawString("T", font, Brushes.Green, currentPoint);
                            break;

                        case Map.Wall:
                            e.Graphics.FillRectangle(Brushes.Black, j * cellSize, i * cellSize, cellSize, cellSize);
                            break;

                    }
                }

            e.Graphics.DrawString("Q", font, Brushes.Red, new PointF(Exit.Y * cellSize + cellSize / 6, Exit.X * cellSize + cellSize / 6));
            e.Graphics.DrawString("H", font, Brushes.Orange, new PointF(Human.Y * cellSize + cellSize / 6, Human.X * cellSize + cellSize / 6));
            e.Graphics.DrawString("M", font, Brushes.Magenta, new PointF(Minotaur.Y * cellSize + cellSize / 6, Minotaur.X * cellSize + cellSize / 6));

            for (int i = 0; i <= n; i++)
                e.Graphics.DrawLine(Pens.Black, 0, i * cellSize, m * cellSize, i * cellSize);

            for (int j = 0; j <= m; j++)
                e.Graphics.DrawLine(Pens.Black, j * cellSize, 0, j * cellSize, n * cellSize);
        }

        public void Move(Direction direction)
        {
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

            if (isNotAbroad(Human.X + dx, Human.Y + dy) && isNotWall(Human.X + dx, Human.Y + dy) && isNotTree(Human.X + dx, Human.Y + dy))
            {
                if (isMinotaur(Human.X + dx, Human.Y + dy))
                {
                    // TODO: !
                }

                if (isExit(Human.X + dx, Human.Y + dy))
                {
                    // TODO: !
                }

                Human = new Point(Human.X + dx, Human.Y + dy);
            }
        }

        private bool isNotAbroad(int x, int y)
        {
            return x >= 0 && y >= 0 && x < n && y < m;
        }

        private bool isNotWall(int x, int y)
        {
            return weights[x, y] != Map.Wall;
        }

        private bool isNotTree(int x, int y)
        {
            return weights[x, y] != Map.Tree;
        }

        private bool isMinotaur(int x, int y)
        {
            return x == Minotaur.X && y == Minotaur.Y;
        }

        private bool isExit(int x, int y)
        {
            return x == Exit.X && y == Exit.Y;
        }
    }
}
