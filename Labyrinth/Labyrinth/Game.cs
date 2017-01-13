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
        private const int Inf = 100000;

        private int[,] weights;

        private Point Minotaur;
        private Point Human;
        private Point Exit;

        private readonly int n;
        private readonly int m;

        public Game(char[,] labyrinth, int n, int m)
        {
            this.n = n;
            this.m = m;

            weights = new int[n, m];

            for(int i = 0; i < n; ++i)
                for(int j = 0; j < m; ++j)
                {
                    switch (labyrinth[i, j])
                    {
                        case 'X':
                            weights[i, j] = Inf;
                            break;

                        case ' ':
                            weights[i, j] = 1;
                            break;

                        case 'W':
                            weights[i, j] = 2;
                            break;

                        case 'T':
                            weights[i, j] = 3;
                            break;

                        case 'M':
                            weights[i, j] = 1;
                            Minotaur = new Point(i, j);
                            break;

                        case 'H':
                            weights[i, j] = 1;
                            Human = new Point(i, j);
                            break;

                        case 'Q':
                            weights[i, j] = Inf + 1;
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
                        case 2:
                            e.Graphics.DrawString("W", font, Brushes.Blue, currentPoint);
                            break;

                        case 3:
                            e.Graphics.DrawString("T", font, Brushes.Green, currentPoint);
                            break;

                        case Inf:
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
    }
}
