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
    public partial class Form_Game : Form
    {
        private readonly string DefaultPath = Application.StartupPath.ToString();
        private Game Labyrinth;
        private const int CellSize = 20;

        public Form_Game()
        {
            InitializeComponent();
            InputLabyrinth();
            pB_Labyrinth.Refresh();       
        }

        private void InputLabyrinth()
        {
            StreamReader reader = new StreamReader(DefaultPath + "\\Labyrinth.txt");

            int[] elements = reader.ReadLine().Split().Select(x => int.Parse(x)).ToArray();

            int n = elements[0];
            int m = elements[1];

            List<List<char>> labyrinth = new List<List<char>>();

            for(int i = 0; i < n; ++i)
                labyrinth.Add(new List<char>(reader.ReadLine()));

            Labyrinth = new Game(labyrinth, n, m);

            SetSizesForm(n, m);
        }

        private void SetSizesForm(int n, int m)
        {
            const int indent = 16;

            StartPosition = FormStartPosition.CenterScreen;

            pB_Labyrinth.Location = new Point(indent, indent);
            pB_Labyrinth.Size = new Size(CellSize * m, CellSize * n);

            ClientSize = new Size(2 * indent + pB_Labyrinth.Width, 2 * indent + pB_Labyrinth.Height);
        }

        private void pB_Labyrinth_Paint(object sender, PaintEventArgs e)
        {
            Labyrinth.Paint(e, CellSize);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
        }
    }

    public class Game
    {
        private const int Inf = 100000;

        private List<List<int>> Labyrinth;

        private Point Minotaur;
        private Point Human;
        private Point Exit;

        private int N;
        private int M;

        public Game(List<List<char>> labyrinth, int n, int m)
        {
            Labyrinth = new List<List<int>>();

            N = n;
            M = m;

            for (int i = 0; i < N; ++i)
            {
                Labyrinth.Add(new List<int>());

                for (int j = 0; j < M; ++j)
                {
                    switch (labyrinth[i][j])
                    {
                        case 'X':
                            Labyrinth[i].Add(Inf);
                            break;

                        case ' ':
                            Labyrinth[i].Add(1);
                            break;

                        case 'W':
                            Labyrinth[i].Add(2);
                            break;

                        case 'T':
                            Labyrinth[i].Add(3);
                            break;

                        case 'M':
                            Labyrinth[i].Add(1);
                            Minotaur = new Point(i, j);
                            break;

                        case 'H':
                            Labyrinth[i].Add(1);
                            Human = new Point(i, j);
                            break;

                        case 'Q':
                            Labyrinth[i].Add(Inf);
                            Exit = new Point(i, j);
                            break;
                    }
                }
            }
        }

        public void Paint(PaintEventArgs e, int CellSize)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold);

            for (int i = 0; i < N; ++i)
                for (int j = 0; j < M; ++j)
                {
                    if (new Point(i, j) == Human)
                        e.Graphics.DrawString("H", font, Brushes.Orange, new PointF(j * CellSize + CellSize / 6, i * CellSize + CellSize / 6));

                    if (new Point(i, j) == Minotaur)
                        e.Graphics.DrawString("M", font, Brushes.Magenta, new PointF(j * CellSize + CellSize / 6, i * CellSize + CellSize / 6));

                    if (Labyrinth[i][j] == 2)
                        e.Graphics.DrawString("W", font, Brushes.Blue, new PointF(j * CellSize + CellSize / 6, i * CellSize + CellSize / 6));

                    if (Labyrinth[i][j] == 3)
                        e.Graphics.DrawString("T", font, Brushes.Green, new PointF(j * CellSize + CellSize / 6, i * CellSize + CellSize / 6));

                    if (Labyrinth[i][j] == Inf)
                    {
                        if (new Point(i, j) == Exit)
                            e.Graphics.DrawString("Q", font, Brushes.Red, new PointF(j * CellSize + CellSize / 6, i * CellSize + CellSize / 6));
                        else
                            e.Graphics.FillRectangle(Brushes.Black, j * CellSize, i * CellSize, CellSize, CellSize);
                    }
                }

            for (int i = 0; i <= N; ++i)
                e.Graphics.DrawLine(Pens.Black, 0, i * CellSize, M * CellSize, i * CellSize);

            for (int j = 0; j <= M; ++j)
                e.Graphics.DrawLine(Pens.Black, j * CellSize, 0, j * CellSize, M * CellSize);
        }
    }
}
