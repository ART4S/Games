using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Threading;

namespace Ball_Breaker
{
    public partial class Game : Form
    {
        private const int maxSize = 12;
        private int ballSize;
        private int counterScore;
        private Dictionary<int, Brush> mapColor;
        private List<Point> shadedCells;
        private Point curCell;

        private int delay;
        public int Delay
        {
            get { return delay; }
            set
            {
                delay = value;
                lbl_delay.Text = "Delay: " + delay.ToString() + " ms";
            }
        }

        private GameMode mode;
        public GameMode Mode
        {
            get { return mode; }
            set
            {
                mode = value;
                lbl_mode.Text = "Game mode: " + mode.ToString();
            }
        }

        private int score;
        private int Score
        {
            get { return score; }
            set
            {
                score = value;
                lbl_score.Text = "Score: " + score.ToString();
            }
        }

        private int score_prev;

        private bool flagToReturn;
        private bool FlagToReturn
        {
            get { return flagToReturn; }
            set
            {
                flagToReturn = value;
                lbl_return.BackColor = flagToReturn ? Color.LightGreen : Color.Red;
            }
        }

        private int[,] board;
        private int[,] board_prev;

        private bool[,] used;

        public Game()
        {
            InitializeComponent();

            Delay = 500;
            Mode = GameMode.standart;

            SetLabelCounterScore();
            Start();
        }

        // Start

        private void SetLabelCounterScore()
        {
            pB_Game.Controls.Add(lbl_counterScore);

            var GPath = new GraphicsPath();
            GPath.AddEllipse(0, 0, lbl_counterScore.Width, lbl_counterScore.Height);

            lbl_counterScore.Region = new Region(GPath);
            lbl_counterScore.Visible = false;
            lbl_counterScore.Text = "";

            GPath.Dispose();
        }

        private void Start()
        {
            SetDefaultValues();
            CreateBoard();

            pB_Game.Refresh();
        }

        private void SetDefaultValues()
        {
            curCell = new Point(0, 0);
            shadedCells = new List<Point>();
            counterScore = 0;
            score_prev = 0;
            Score = 0;
            FlagToReturn = false;

            board = new int[maxSize, maxSize];
            board_prev = new int[maxSize, maxSize];
            used = new bool[maxSize, maxSize];

            mapColor = new Dictionary<int, Brush>
            {
                {1, Brushes.Red},
                {2, Brushes.Blue},
                {3, Brushes.Green},
                {4, Brushes.Yellow},
                {5, Brushes.DarkViolet}
            };

            ballSize = pB_Game.Width / maxSize;

            lbl_score_best.Text = "BEST: " + Properties.Settings.Default.score_best.ToString();
        }

        private void CreateBoard()
        {
            var rnd = new Random();
            var colorCount = mapColor.Count;

            for (int i = 0; i < maxSize; ++i)
                for (int j = 0; j < maxSize; ++j)
                {
                    board[i, j] = rnd.Next(colorCount) + 1;
                    used[i, j] = false;
                }
        }

        // Game

        private void pB_Game_MouseMove(object sender, MouseEventArgs e)
        {
            var X = e.Y / ballSize;
            var Y = e.X / ballSize;

            if (X < 0 || X >= maxSize || Y < 0 || Y >= maxSize) return;

            curCell.X = X;
            curCell.Y = Y;
        }

        private void pB_Game_Click(object sender, EventArgs e)
        {
            if (used[curCell.X, curCell.Y] == true)
            {
                if (shadedCells.Count > 1)
                {
                    FlagToReturn = true;
                    score_prev = score;
                    Score += counterScore;

                    SaveOrLoadBoard(true);

                    shiftfBoard_down();
                    pB_Game.Refresh();
                    Thread.Sleep(Delay);

                    switch (mode)
                    {
                        case GameMode.standart: shiftColumns(); break;
                        case GameMode.shift: shiftBoard_right(); break;
                        case GameMode.refill:
                        {
                            addNewLines();
                            shiftBoard_right();
                            addNewColumns();

                            break;
                        }
                        default: throw new ArgumentOutOfRangeException();
                    }

                    checkEndGame();
                }

                shadedCells_Clear();
            }
            else
            {
                shadedCells_Clear();

                if (board[curCell.X, curCell.Y] != 0)
                    searchCells(curCell.X, curCell.Y);

                counterScore = shadedCells.Count * shadedCells.Count - shadedCells.Count;
            }

            pB_Game.Refresh();
        }

        private void searchCells(int i, int j)
        {
            if (i < 0 || i >= maxSize || j < 0 || j >= maxSize || used[i, j] || board[curCell.X, curCell.Y] != board[i, j]) return;

            used[i, j] = true;
            shadedCells.Add(new Point(i, j));

            searchCells(i + 1, j);
            searchCells(i - 1, j);
            searchCells(i, j + 1);
            searchCells(i, j - 1);
        }

        private void shadedCells_Clear()
        {
            foreach(var p in shadedCells)
            {
                used[p.X, p.Y] = false;
            }

            shadedCells.Clear();
        }

        private void checkEndGame()
        {
            for (int i = 0; i < maxSize; ++i)
                for (int j = 0; j < maxSize; ++j)
                {
                    if (board[i, j] == 0) continue;
                    if ((i + 1 < maxSize && board[i, j] == board[i + 1, j]) || (j + 1 < maxSize && board[i, j] == board[i, j + 1])) return;
                }

            pB_Game.Refresh();

            MessageBox.Show("Final score: " + Score.ToString(), "Game over");
            checkBestScore();
            Start();
        }

        private void checkBestScore()
        {
            if (Properties.Settings.Default.score_best >= Score) return;

            Properties.Settings.Default.score_best = Score;
            Properties.Settings.Default.Save();
        }

        private void SaveOrLoadBoard(bool save)
        {
            for (int i = 0; i < maxSize; ++i)
                for (int j = 0; j < maxSize; ++j)
                {
                    if (save)
                        board_prev[i, j] = board[i, j];
                    else
                        board[i, j] = board_prev[i, j];
                }
        }

        private void shiftfBoard_down()
        {
            var columnsUnique = shadedCells.Select(elem => elem.Y).ToList().Distinct();

            foreach (var j in columnsUnique)
            {
                var i = maxSize - 1;

                while (used[i, j] == false) --i;

                var k = i;

                while (k >= 0)
                {
                    if (used[k, j] == false)
                    {
                        board[i, j] = board[k, j];
                        i--;
                    }

                    board[k, j] = 0;

                    k--;
                }
            }

            shadedCells_Clear();
        }

        private void shiftBoard_right()
        {
            for (int i = 0; i < maxSize; ++i)
            {
                var j = maxSize - 1;

                while (j >= 0 && board[i, j] != 0) --j;

                var k = j;

                while (k >= 0)
                {
                    if (board[i, k] != 0)
                    {
                        board[i, j] = board[i, k];
                        board[i, k] = 0;

                        j--;
                    }

                    k--;
                }
            }
        }

        private void shiftColumns()
        {
            var j = maxSize - 1;

            while (j >= 0 && board[maxSize - 1, j] != 0) --j;

            var k = j;

            while (k >= 0)
            {
                if (board[maxSize - 1, k] != 0)
                {
                    for (int i = 0; i < maxSize; ++i)
                    {
                        board[i, j] = board[i, k];
                        board[i, k] = 0;
                    }

                    j--;
                }

                k--;
            }
        }

        private void addNewColumns()
        {
            var colorCount = mapColor.Count;
            var addedNewColumn = false;
            var rnd = new Random();

            for (int j = 0; j < maxSize; ++j)
                if (board[maxSize - 1, j] == 0)
                {
                    for (int i = 0; i < maxSize; ++i)
                        board[i, j] = rnd.Next(colorCount) + 1;

                    addedNewColumn = true;
                }

            if (!addedNewColumn) return;

            pB_Game.Refresh();
            Thread.Sleep(Delay);
            shiftBoard_right();
        }

        private void addNewLines()
        {
            var colorCount = mapColor.Count;
            var addedNewLines = false;
            var rnd = new Random();

            for (int i = 0; i < maxSize; ++i)
            {
                var countEmptyCells = 0;

                for (int j = 0; j < maxSize; ++j)
                    if (board[i, j] == 0) countEmptyCells++;

                if (countEmptyCells == maxSize)
                {
                    for (int j = 0; j < maxSize; ++j)
                        board[i, j] = rnd.Next(colorCount) + 1;

                    addedNewLines = true;
                }
                else break;
            }

            if (!addedNewLines) return;

            pB_Game.Refresh();
            Thread.Sleep(Delay);

            for (int i = 0; i < maxSize; ++i)
                for (int j = 0; j < maxSize; ++j)
                    if (board[i, j] == 0)
                    {
                        shadedCells.Add(new Point(i, j));
                        used[i, j] = true;
                    }

            shiftfBoard_down();
            pB_Game.Refresh();
            Thread.Sleep(Delay);
        }

        // Paint

        private void pB_Game_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            lbl_counterScore.Visible = false;

            e.Graphics.DrawRectangle(new Pen(Color.Black), 0, 0, pB_Game.Width - 1, pB_Game.Height - 1);

            drawBalls(e);

            if (shadedCells.Count > 1) drawShadedCells(e);
        }

        private void drawBalls(PaintEventArgs e)
        {
            for (int i = 0; i < maxSize; ++i)
                for (int j = 0; j < maxSize; ++j)
                {
                    if (board[i, j] != 0)
                        e.Graphics.FillEllipse(mapColor[board[i, j]], j * ballSize, i * ballSize, ballSize, ballSize);
                }
        }

        private void drawShadedCells(PaintEventArgs e)
        {
            var pen = new Pen(Color.Black, 1);
            var i_min = maxSize + 1;

            foreach (var point in shadedCells)
            {
                var i = point.X;
                var j = point.Y;

                if (i + 1 < maxSize && used[i + 1, j] == false)
                {
                    e.Graphics.DrawLine(pen, j * ballSize, i * ballSize + ballSize, j * ballSize + ballSize, i * ballSize + ballSize);
                }

                if (i - 1 >= 0 && used[i - 1, j] == false)
                {
                    e.Graphics.DrawLine(pen, j * ballSize, i * ballSize, j * ballSize + ballSize, i * ballSize);
                }

                if (j + 1 < maxSize && used[i, j + 1] == false)
                {
                    e.Graphics.DrawLine(pen, j * ballSize + ballSize, i * ballSize, j * ballSize + ballSize, i * ballSize + ballSize);
                }

                if (j - 1 >= 0 && used[i, j - 1] == false)
                {
                    e.Graphics.DrawLine(pen, j * ballSize, i * ballSize, j * ballSize, i * ballSize + ballSize);
                }

                if (i_min > i) i_min = i;
            }

            counterScore_Paint(return_upper_left_shadedCell(i_min), e);
        }

        private Point return_upper_left_shadedCell(int i_min)
        {
            var result = new Point(i_min, maxSize);

            foreach (var point in shadedCells)
            {
                if (point.X == result.X && point.Y < result.Y)
                    result = point;
            }

            return result;
        }

        private void counterScore_Paint(Point point, PaintEventArgs e)
        {
            var shift = ballSize / 3;

            int[] di = { -1, -1, 0, 0 };
            int[] dj = { -1, 0, -1, 0 };

            for (int k = 0; k < di.Length; ++k)
            {
                var i = point.X + di[k];
                var j = point.Y + dj[k];

                if (i >= 0 && i < maxSize && j >= 0 && j < maxSize)
                {
                    lbl_counterScore.Location = new Point(j * ballSize + shift, i * ballSize + shift);
                    lbl_counterScore.Text = counterScore.ToString();
                    lbl_counterScore.Visible = true;

                    return;
                }
            }
        }

        // Control

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1: lbl_newGame_Click(sender, e); return;
                case Keys.Space: lbl_return_Click(sender, e); return;
            }
        }

        private void lbl_counterScore_Click(object sender, EventArgs e)
        {
            shadedCells_Clear();
            pB_Game.Refresh();
        }

        private void lbl_return_Click(object sender, EventArgs e)
        {
            if (FlagToReturn == false) return;

            shadedCells_Clear();
            SaveOrLoadBoard(false);
            Score = score_prev;
            FlagToReturn = false;

            pB_Game.Refresh();
        }

        private void lbl_newGame_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void lbl_settings_Click(object sender, EventArgs e)
        {
            Form_settings settings = new Form_settings(this);

            settings.ShowDialog();
            //restart();
        }

        private void lbl_aboutGame_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developer: ART4S\nYear: 2016", "About game");
        }

        private void lbl_rules_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The sence of game is removing the balls.\nYou can strike the chains contain more than one ball.\nSpace / 'Cancel' - recall last step\nYou can recall only one previous step\nThere are the several modes of game: standart, shift and refill", "Rules");
        }
    }
}
