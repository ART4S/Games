using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MinesweeperForm : Form
    {
        private Image imageFlag = Properties.Resources.flag,
                      imageKrest = Properties.Resources.krest,
                      imageMine = Properties.Resources.mine,
                      imageVictory = Properties.Resources.Nyan_gary;

        private const int maxSizeI = 16,
                          maxSizeJ = 30,
                          CellSize = 30;

        private GameStatus status;
        private Cell[,] Board = new Cell[maxSizeI, maxSizeJ];
        private int maxi, maxj, mines, level, minesLeft, amountClosedCells, time;
        private Point shadedСell;
        private MainMenu menu;
        private bool signalForExit = true;
                
        public MinesweeperForm(int level, MainMenu menu)
        {
            this.level = level;
            this.menu = menu;

            InitializeComponent();
            setDifficultyLevel();
            settingForm();
            restart();
        }

        // графическая структура

        private void settingForm()
        {
            int indent = 8;
            StartPosition = FormStartPosition.CenterScreen;

            pictureBoxBoard.Size = new Size(maxj*CellSize, maxi*CellSize);
            ClientSize = new Size(2*indent + pictureBoxBoard.Width, 4*indent + labelMinesLeft.Height + labelStartGame.Height + pictureBoxBoard.Height);

            labelMinesLeft.Location = new Point(indent, indent);
            labelTime.Location = new Point(ClientSize.Width - labelTime.Width - indent, indent);
            labelStartGame.Location = new Point(indent, ClientSize.Height - labelStartGame.Height - indent);
            labelMainMenu.Location = new Point(ClientSize.Width - labelMainMenu.Width - indent, ClientSize.Height - labelMainMenu.Height - indent);

            pictureBoxBoard.Location = new Point(indent, 2*indent + labelMinesLeft.Height);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            time++;

            if (time == 86400) time = 0;

            int h = time / 3600 % 24,
                m = time / 60 % 60,
                s = time % 60;

            if (h < 10) labelTime.Text = "0" + h;
            else labelTime.Text = h.ToString();

            if (m < 10) labelTime.Text += ":0" + m;
            else labelTime.Text += ":" + m;

            if (s < 10) labelTime.Text += ":0" + s;
            else labelTime.Text += ":" + s;
        }

        private void pictureBoxBoard_Paint(object sender, PaintEventArgs e)
        {
            int Width = pictureBoxBoard.Width,
                Height = pictureBoxBoard.Height;

            Pen pen = new Pen(Color.Black, 2),
                penShadedCell = new Pen(Color.Black, pen.Width + 2);

            // labelMinesLeft
            if(status == GameStatus.game) labelMinesLeft.Text = minesLeft.ToString();

            // рамка
            e.Graphics.DrawRectangle(pen, pen.Width/2, pen.Width/2, Width - pen.Width, Height - pen.Width);

            // линии
            drawLines(e, pen);

            // состояния клетки
            drawCells(e, pen);

            // курсор
            if(Board[shadedСell.X, shadedСell.Y].Status != CellStatus.open)
                e.Graphics.DrawRectangle(pen, shadedСell.Y*CellSize + penShadedCell.Width/2, shadedСell.X*CellSize + penShadedCell.Width/2, CellSize - penShadedCell.Width, CellSize - penShadedCell.Width);

            pen.Dispose();
            penShadedCell.Dispose();
        }

        private void drawLines(PaintEventArgs e, Pen pen)
        {
            for (var d = 1; d < maxj; ++d)
            {
                if (d < maxi) e.Graphics.DrawLine(pen, 0, d*CellSize, Width, d*CellSize);

                e.Graphics.DrawLine(pen, d*CellSize, 0, d*CellSize, Height);
            }
        }

        private void drawCells(PaintEventArgs e, Pen pen)
        {
            Font font = new Font("Consolas", 10, FontStyle.Bold);

            for (int i = 0; i < maxi; ++i)
                for (int j = 0; j < maxj; ++j)
                    switch (Board[i, j].Status)
                    {
                        // для клетки с флагом
                        case CellStatus.flag:
                            if (status == GameStatus.gameover)
                            {
                                if (Board[i, j].Mine)
                                {
                                    e.Graphics.DrawImage(imageMine,  j*CellSize + pen.Width / 2, i*CellSize + pen.Width / 2, CellSize - pen.Width, CellSize - pen.Width);
                                    e.Graphics.DrawImage(imageKrest, j* CellSize + pen.Width / 2, i*CellSize + pen.Width / 2, CellSize - pen.Width, CellSize - pen.Width);
                                }
                                else if (Board[i, j].SumMines != 0)
                                    e.Graphics.DrawString(Board[i, j].SumMines.ToString(), font, Brushes.Black, j*CellSize + 9, i*CellSize + 6);
                            }
                            else
                                e.Graphics.DrawImage(imageFlag, j*CellSize, i*CellSize, CellSize, CellSize);
                            break;

                        // для открытой клетки
                        case CellStatus.open:
                            if (Board[i, j].SumMines != 0)
                                e.Graphics.DrawString(Board[i, j].SumMines.ToString(), font, Brushes.Black, j*CellSize + 9, i*CellSize + 6);
                            break;

                        // для закрытой клетки
                        case CellStatus.close:
                            if (status == GameStatus.gameover)
                            { 
                                if (Board[i, j].Mine)
                                {
                                    e.Graphics.DrawImage(imageMine, j*CellSize + pen.Width/2, i*CellSize + pen.Width/2, CellSize - pen.Width, CellSize - pen.Width);

                                    if (labelMinesLeft.Text == "YOU WON!")
                                        e.Graphics.DrawImage(imageKrest, j*CellSize + pen.Width/2, i*CellSize + pen.Width/2, CellSize - pen.Width, CellSize - pen.Width);
                                }
                                else if (Board[i, j].SumMines != 0)
                                        e.Graphics.DrawString(Board[i, j].SumMines.ToString(), font, Brushes.Black, j*CellSize + 9, i*CellSize + 6);
                            }
                            else
                                e.Graphics.FillRectangle(Brushes.LightSeaGreen, j*CellSize + pen.Width/2, i*CellSize + pen.Width/2, CellSize - pen.Width, CellSize - pen.Width);
                            break;
                    }
        }

        private void pictureBoxBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (status == GameStatus.game)
            {
                int X = e.Y / CellSize,
                    Y = e.X / CellSize;

                if (X < maxi && Y < maxj && X >= 0 && Y >= 0)
                {
                    shadedСell = new Point(X, Y);
                    pictureBoxBoard.Refresh();
                }
            }
        }

        private void pictureBoxBoard_MouseClick(object sender, MouseEventArgs e)
        {
            if (status == GameStatus.game && !timer.Enabled) timer.Start();

            if (status == GameStatus.game)
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if (Board[shadedСell.X, shadedСell.Y].Status == CellStatus.close)
                        {
                            if (checkLose(shadedСell.X, shadedСell.Y)) return;
                            openingCell(shadedСell.X, shadedСell.Y);
                            checkVictory();
                        }
                        return;

                    case MouseButtons.Right:
                        switch (Board[shadedСell.X, shadedСell.Y].Status)
                        {
                            case CellStatus.flag: Board[shadedСell.X, shadedСell.Y].Status = CellStatus.close; minesLeft++; return;
                            case CellStatus.close: Board[shadedСell.X, shadedСell.Y].Status = CellStatus.flag; minesLeft--; return;
                        }
                        return;

                    case MouseButtons.Middle:
                        if (Board[shadedСell.X, shadedСell.Y].Status == CellStatus.open && Board[shadedСell.X, shadedСell.Y].SumMines == amountFlags())
                            for (int dx = -1; dx <= 1; ++dx)
                                for (int dy = -1; dy <= 1; ++dy)
                                    if ((shadedСell.X + dx) < maxi && (shadedСell.Y + dy) < maxj && (shadedСell.X + dx) >= 0 && (shadedСell.Y + dy) >= 0)
                                        if (Board[shadedСell.X + dx, shadedСell.Y + dy].Status == CellStatus.close)
                                        {
                                            if (checkLose(shadedСell.X + dx, shadedСell.Y + dy)) return;
                                            openingCell(shadedСell.X + dx, shadedСell.Y + dy);
                                            checkVictory();
                                        }
                        return;
                }
        }

            // вспомогательные функции

        private int amountFlags()
        {
            int flagsInRadius = 0;

            for (int dx = -1; dx <= 1; ++dx)
                for (int dy = -1; dy <= 1; ++dy)
                    if ((shadedСell.X + dx) < maxi && (shadedСell.Y + dy) < maxj && (shadedСell.X + dx) >= 0 && (shadedСell.Y + dy) >= 0)
                        if (Board[shadedСell.X + dx, shadedСell.Y + dy].Status == CellStatus.flag)
                            flagsInRadius++;

            return flagsInRadius;
        }

        private bool checkLose(int x, int y)
        {
            if (Board[x, y].Mine)
            {
                timer.Stop();
                status = GameStatus.gameover;
                labelMinesLeft.Text = "YOU LOSE!";
                pictureBoxBoard.Refresh();
                return true;
            }
            return false;
        }

        private void checkVictory()
        {
            if (amountClosedCells == mines)
            {
                timer.Stop();
                status = GameStatus.gameover;
                labelMinesLeft.Text = "YOU WON!";
                if (level < 3) pictureBoxBoard.Image = imageVictory;
            }
        }

            // StartGame, MainMenu

        private void labelStartGame_MouseClick(object sender, MouseEventArgs e)
        {
            restart();
            Refresh();
        }

        private void labelMainMenu_MouseClick(object sender, MouseEventArgs e)
        {
            signalForExit = false;
            Close();
        }

        private void labelsLower_MouseHover(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Font = new Font(label.Font.Name, label.Font.Size, FontStyle.Bold);
        }

        private void labelsLower_MouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Font = new Font(label.Font.Name, label.Font.Size, FontStyle.Regular);
        }


        // логическая структура

        private void setDifficultyLevel()
        {
            switch (level)
            {
                case 1: maxi = 9;  maxj = 9;  mines = 10; break;
                case 2: maxi = 16; maxj = 16; mines = 40; break;
                case 3: maxi = 16; maxj = 30; mines = 99; break;
            }
        }  // настройка уровня сложности

        private void restart()
        {
            initializeParameters();
            initializeCell();
            createMines(mines);
            createOtherCells();
        }                 // перезапуск

        private void initializeParameters()
        {
            time = 0;
            labelTime.Text = "00:00:00";
            pictureBoxBoard.Image = null;

            status = GameStatus.game;
            minesLeft = mines;
            amountClosedCells = maxi * maxj;
            shadedСell = new Point(0, 0);
        }    // инициализация параметров (максимум, количество закрытых клеток)

        private void initializeCell()
        {
            for (int i = 0; i < maxi; ++i)
                for (int j = 0; j < maxj; ++j)
                    Board[i, j] = new Cell();
        }          // инициализация клеток (задание дефолтных значений)

        private void createMines(int mines)
        {
            Random random = new Random();
            int x, y;

            while (mines > 0)
            {
                x = random.Next(0, maxi);
                y = random.Next(0, maxj);
                if (!Board[x, y].Mine)
                {
                    Board[x, y].Mine = true;
                    mines--;
                }
            }
        }    // создание мин

        private void createOtherCells()
        {
            for (int i = 0; i < maxi; ++i)
                for (int j = 0; j < maxj; ++j)
                    if (!Board[i, j].Mine) countMines(i, j);
        }        // создание остальных клеток

        private void countMines(int x, int y)
        {
            for (int dx = -1; dx <= 1; ++dx)
                for (int dy = -1; dy <= 1; ++dy)
                    Board[x, y].SumMines += checkMine(x + dx, y + dy);
        }  // подсчёт количества мин вокруг клетки

        private int checkMine(int x, int y)
        {
            return (x < maxi && y < maxj && x >= 0 && y >= 0) ? (Board[x, y].Mine ? 1 : 0) : 0;
        }    // проверка мины

        private void openingCell(int x, int y)
        {
            if (x >= maxi || y >= maxj || x < 0 || y < 0 || Board[x, y].Status != CellStatus.close) return;
            Board[x, y].Status = CellStatus.open;
            amountClosedCells--;

            if (Board[x, y].SumMines == 0)
            {
                for (int dx = -1; dx <= 1; ++dx)
                    for (int dy = -1; dy <= 1; ++dy)
                        openingCell(x + dx, y + dy);
            }
            //else return;
            //Board[x, y].Status == CellStatus.open if (Board[x, y].Status == CellStatus.flag) minesLeft++;
        } // открытие клетки | пустых ячеек

        private enum GameStatus { game, gameover }

        private enum CellStatus { open, close, flag }

        private class Cell
        {
            public CellStatus Status;
            public bool Mine;
            public int SumMines;

            // CONSTRUCTOR
            public Cell()
            {
                Status = CellStatus.close;
                Mine = false;
                SumMines = 0;
            }
        }

        // выход
        private void MinesweeperForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (signalForExit) Application.Exit();
            menu.Show();
        }
    }
}