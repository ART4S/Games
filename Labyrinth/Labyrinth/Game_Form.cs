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
    public partial class Game_Form : Form
    {
        private readonly string defaultPath = Application.StartupPath.ToString();
        private Game labyrinth;
        private const int CellSize = 30;
        private Mode mode = Mode.Eazy;

        public Game_Form()
        {
            InitializeComponent();
            InputLabyrinth();
            pB_Labyrinth.Refresh();       
        }

        private void InputLabyrinth()
        {
            StreamReader reader = new StreamReader(defaultPath + "\\Labyrinth.txt");

            int[] elements = reader.ReadLine().Split().Select(int.Parse).ToArray();

            int n = elements[0];
            int m = elements[1];

            char[,] labyrinth = new char[n, m];

            for(int i = 0; i < n; ++i)
            {
                string line = reader.ReadLine();

                for (int j = 0; j < m; ++j)
                    labyrinth[i, j] = line[j];
            }

            this.labyrinth = new Game(labyrinth, n, m);

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
            labyrinth.Paint(e, CellSize);
        }

        // TODO: я тут
        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    labyrinth.Move(Direction.UP, mode);
                    break;

                case Keys.Down:
                    labyrinth.Move(Direction.DOWN, mode);
                    break;

                case Keys.Left:
                    labyrinth.Move(Direction.LEFT, mode);
                    break;

                case Keys.Right:
                    labyrinth.Move(Direction.RIGHT, mode);
                    break;

                default: return;
            }

            pB_Labyrinth.Refresh();
        }
    }
}
