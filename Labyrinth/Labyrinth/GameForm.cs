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
        private const int CellSize = 25;

        private readonly char[,] gameField;

        private readonly int gameFieldHeight;
        private readonly int gameFieldWidth;

        private Game game;
        private Mode currentMode;

        public GameForm()
        {
            InitializeComponent();

            currentMode = Mode.EazyCrazy;

            StreamReader reader = new StreamReader(Application.StartupPath + "\\Labyrinth.txt");

            int[] read = reader.ReadLine().Split().Select(int.Parse).ToArray();

            gameFieldHeight = read[0];
            gameFieldWidth = read[1];
            gameField = new char[gameFieldHeight, gameFieldWidth];

            for (int i = 0; i < gameFieldHeight; ++i)
            {
                string line = reader.ReadLine();

                for (int j = 0; j < gameFieldWidth; ++j)
                    gameField[i, j] = line[j];
            }

            SetSizesElementsInForm();
            Start();
        }

        private void SetSizesElementsInForm()
        {
            const int indent = 16;

            StartPosition = FormStartPosition.CenterScreen;

            pictureBoxGameField.Location = new System.Drawing.Point(indent, menuStrip.Height + indent);
            pictureBoxGameField.Size = new Size(gameFieldWidth * CellSize, gameFieldHeight * CellSize);

            labelVisitedСells.Location = new System.Drawing.Point(pictureBoxGameField.Location.X + pictureBoxGameField.Width + indent, pictureBoxGameField.Location.Y);
            labelCurrentMode.Location = new System.Drawing.Point(labelVisitedСells.Location.X, labelVisitedСells.Location.Y + labelVisitedСells.Height + indent);

            ClientSize = new Size(7 * indent + pictureBoxGameField.Width + labelVisitedСells.Width, 2 * indent + pictureBoxGameField.Height + menuStrip.Height);
        }

        private void Start()
        {
            game = new Game(gameField, gameFieldHeight, gameFieldWidth, currentMode);

            labelVisitedСells.Text = Resources.usedCellsCounterText + game.UsedCellsCounter;
            labelCurrentMode.Text = Resources.currentModeText + currentMode.ToRussianString();

            pictureBoxGameField.Refresh();
        }

        private void pictureBoxGameField_Paint(object sender, PaintEventArgs e)
        {
            game.Paint(e, CellSize);
        }

        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    game.Move(Direction.Up);
                    break;

                case Keys.Down:
                    game.Move(Direction.Down);
                    break;

                case Keys.Left:
                    game.Move(Direction.Left);
                    break;

                case Keys.Right:
                    game.Move(Direction.Right);
                    break;

                default: return;
            }

            pictureBoxGameField.Refresh();

            Random random = new Random();
            string loseMessage = Settings.Default.loseMessages[random.Next(Settings.Default.loseMessages.Count)];
            string wonMessage = Settings.Default.wonMessages[random.Next(Settings.Default.wonMessages.Count)];

            switch (game.GameState)
            {
                case State.NotFinished:
                    labelVisitedСells.Text = Resources.usedCellsCounterText + game.UsedCellsCounter;
                    break;

                case State.Win:
                    MessageBox.Show(wonMessage, @"Победа");
                    Start();
                    break;

                case State.Lose:
                    MessageBox.Show(loseMessage, @"Поражение");
                    Start();
                    break;
            }
        }

        private void labelNewGame_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void labelAboutGame_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.aboutGameText, @"Об игре");
        }

        private void labelRules_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.rulesText, @"Правила");
        }

        private void labelEazyCrazy_Click(object sender, EventArgs e)
        {
            currentMode = Mode.EazyCrazy;
            Start();
        }

        private void labelEazy_Click(object sender, EventArgs e)
        {
            currentMode = Mode.Eazy;
            Start();
        }

        private void labelNormal_Click(object sender, EventArgs e)
        {
            currentMode = Mode.Normal;
            Start();
        }

        private void labelHard_Click(object sender, EventArgs e)
        {
            currentMode = Mode.Hard;
            Start();
        }
    }
}
