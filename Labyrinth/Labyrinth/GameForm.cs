using System;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using Labyrinth.Properties;

namespace Labyrinth
{
    public partial class GameForm : Form
    {
        private const int CellSize = 40;

        private readonly char[,] labyrinth;
        private readonly int n;
        private readonly int m;

        private Game game;
        private Mode mode;

        public GameForm()
        {
            InitializeComponent();

            StreamReader reader = new StreamReader(Application.StartupPath + "\\Labyrinth.txt");

            int[] read = reader.ReadLine().Split().Select(int.Parse).ToArray();

            n = read[0];
            m = read[1];

            labyrinth = new char[n, m];

            for (int i = 0; i < n; ++i)
            {
                string line = reader.ReadLine();

                for (int j = 0; j < m; ++j)
                    labyrinth[i, j] = line[j];
            }

            SetSizesForm(n, m);
            mode = Mode.Hard;
            Start();
        }

        private void SetSizesForm(int n, int m)
        {
            const int indent = 16;

            StartPosition = FormStartPosition.CenterScreen;

            pictureBoxLabyrinth.Location = new System.Drawing.Point(indent, indent + menuStrip.Height);
            pictureBoxLabyrinth.Size = new Size(CellSize * m + 2 * labelVisitedСells.Width, CellSize * n);
            labelVisitedСells.Location = new System.Drawing.Point(pictureBoxLabyrinth.Location.X + pictureBoxLabyrinth.Height + indent, pictureBoxLabyrinth.Location.Y);
            ClientSize = new Size(2 * indent + pictureBoxLabyrinth.Width, 2 * indent + menuStrip.Height + pictureBoxLabyrinth.Height);
        }

        private void pB_Labyrinth_Paint(object sender, PaintEventArgs e)
        {
            game.Paint(e, CellSize);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    game.Move(Direction.Up, mode);
                    break;

                case Keys.Down:
                    game.Move(Direction.Down, mode);
                    break;

                case Keys.Left:
                    game.Move(Direction.Left, mode);
                    break;

                case Keys.Right:
                    game.Move(Direction.Right, mode);
                    break;

                default: return;
            }

            pictureBoxLabyrinth.Refresh();
        }

        private void Start()
        {
            game = new Game(labyrinth, n, m);

            pictureBoxLabyrinth.Refresh();
        }

        private void labelAboutGame_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.GameForm_labelAboutGame_Click_, "About game");
        }

        private void labelRules_Click(object sender, EventArgs e)
        {
            MessageBox.Show("!", "Rules");
        }
    }
}
