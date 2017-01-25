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
        private Game game;

        public GameForm()
        {
            InitializeComponent();

            string[] file = File.ReadAllLines(Application.StartupPath + "\\Labyrinth.txt");

            int[] sizesLine = file[0].Split().Select(int.Parse).ToArray();

            int height = sizesLine[0];
            int width = sizesLine[1];

            char[,] gameField = new char[height, width];

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    gameField[i, j] = file[i + 1][j];

            SetSizesElementsInForm(height, width);
            Start(gameField, height, width);
        }

        private void SetSizesElementsInForm(int gameFieldHeight, int gameFieldWidth)
        {
            const int indent = 16;

            StartPosition = FormStartPosition.CenterScreen;

            pictureBoxGameField.Location = new System.Drawing.Point(indent, menuStrip.Height + indent);
            pictureBoxGameField.Size = new Size(gameFieldWidth * CellSize, gameFieldHeight * CellSize);

            labelVisitedСells.Location = new System.Drawing.Point(pictureBoxGameField.Location.X + pictureBoxGameField.Width + indent, pictureBoxGameField.Location.Y);
            labelCurrentMode.Location = new System.Drawing.Point(labelVisitedСells.Location.X, labelVisitedСells.Location.Y + labelVisitedСells.Height + indent);

            ClientSize = new Size(7 * indent + pictureBoxGameField.Width + labelVisitedСells.Width, 2 * indent + pictureBoxGameField.Height + menuStrip.Height);
        }

        private void Start(char[,] gameField, int gameFieldHeight, int gameFieldWidth)
        {
            game = new Game(gameField, gameFieldHeight, gameFieldWidth, Mode.EazyCrazy);

            game.VictoryEvent += GameOnVictoryEvent;
            game.LoseEvent += GameOnLoseEvent;

            LabelsInfoRefresh();
            pictureBoxGameField.Refresh();
        }

        private void Restart(Mode mode)
        {
            game.Restart(mode);

            LabelsInfoRefresh();
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

            LabelsInfoRefresh();
            pictureBoxGameField.Refresh();
        }

        private void pictureBoxGameField_Paint(object sender, PaintEventArgs e)
        {
            game.Paint(e.Graphics, CellSize);
        }

        private void labelNewGame_Click(object sender, EventArgs e)
        {
            Restart(game.Mode);
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
            Restart(Mode.EazyCrazy);
        }

        private void labelEazy_Click(object sender, EventArgs e)
        {
            Restart(Mode.Eazy);
        }

        private void labelNormal_Click(object sender, EventArgs e)
        {
            Restart(Mode.Normal);
        }

        private void labelHard_Click(object sender, EventArgs e)
        {
            Restart(Mode.Hard);
        }

        private void GameOnLoseEvent(object sender, EventArgs eventArgs)
        {
            string loseMessage = Settings.Default.loseMessages[new Random().Next(Settings.Default.loseMessages.Count)];

            MessageBox.Show(loseMessage, @"Поражение");

            Restart(game.Mode);
        }

        private void GameOnVictoryEvent(object sender, EventArgs eventArgs)
        {
            string wonMessage = Settings.Default.wonMessages[new Random().Next(Settings.Default.wonMessages.Count)];

            MessageBox.Show(wonMessage, @"Победа");

            Restart(game.Mode);
        }

        private void LabelsInfoRefresh()
        {
            labelVisitedСells.Text = Resources.usedCellsCounterText + game.UsedCellsCounter;
            labelCurrentMode.Text = Resources.currentModeText + game.Mode.ToRussianString();
        }
    }
}
