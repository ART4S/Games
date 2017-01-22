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
        private const int CellSize = 20;

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

            game.VictoryEvent += GameOnVictoryEvent;
            game.LoseEvent += GameOnLoseEvent;

            labelVisitedСells.Text = Resources.usedCellsCounterText + game.UsedCellsCounter;
            labelCurrentMode.Text = Resources.currentModeText + currentMode.ToRussianString();

            pictureBoxGameField.Refresh();
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

            labelVisitedСells.Text = Resources.usedCellsCounterText + game.UsedCellsCounter;
        }

        private void pictureBoxGameField_Paint(object sender, PaintEventArgs e)
        {
            game.Paint(e, CellSize);
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

        private void GameOnLoseEvent(object sender, EventArgs eventArgs)
        {
            string loseMessage = Settings.Default.loseMessages[new Random().Next(Settings.Default.loseMessages.Count)];

            MessageBox.Show(loseMessage, @"Поражение");

            Start();
        }

        private void GameOnVictoryEvent(object sender, EventArgs eventArgs)
        {
            string wonMessage = Settings.Default.wonMessages[new Random().Next(Settings.Default.wonMessages.Count)];

            MessageBox.Show(wonMessage, @"Победа");

            Start();
        }
    }
}
