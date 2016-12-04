using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Game : Form
    {
        static private float plW = 1, plW_div2 = plW / 2;
        public int color_opacity;
        private Pen penLines;

        private const short maxi = 20,
                            maxj = 10,
                            cellSize = 30,
                            step = 3,
                            speed_default = 800,
                            indent = 8;

        private Cell[,] Board;
        private Figure mainFigure, nextFigure;

        private int figuresAmount;
        private int score;
        private int level;
        private int speed;

        private bool flag_to_create_NewFigure;

        private Point middleCell_default, middleCell_nextF, middleCell;

        static private Random random;

        public Game()
        {
            InitializeComponent();

            setDefaultValues();
            setSizes();
            setLocations();

            start();
        }

        //===== Interface structure =====//

        //menu

        private void menuItemNewGame_Click(object sender, EventArgs e)
        {
            start();
        }

        private void menuItemTransparency_Click(object sender, EventArgs e)
        {
            SetOpacityLinesForm OpacityLinesForm = new SetOpacityLinesForm(this);
            OpacityLinesForm.ShowDialog();
        }

        private void menuItemHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Use the bricks to fill the strings. If the bricks would reach the red line, the game will be over.\n\nPress WASD to control the bricks.\n\nIf you would fill the several strings at the same time, it will bring more points:\n1 string = 100 points\n2 strings = 300 points\n3 strings = 700 points\n4 strings = 1500 points\n\nAfter ever 1000 points the speed of falling bricks will increase.\nThere are only 10 levels.");
        }

        private void menuItemInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developer: ART4S\nYear: 2016");
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //board

        private void setDefaultValues()
        {
            color_opacity = Properties.Settings.Default.color_opacity;

            penLines = new Pen(Color.Black, plW);
            Board = new Cell[maxi, maxj];

            middleCell_default = new Point(1, 4);
            middleCell_nextF = new Point(1, 1);

            random = new Random();
        }

        private void setSizes()
        {
            pB_Board.Size = new Size(maxj * cellSize + (int)plW, maxi * cellSize + (int)plW);
            pB_NextFigure.Size = new Size(4 * cellSize + (int)plW, 4 * cellSize + (int)plW);
            ClientSize = new Size(3 * indent + pB_Board.Width + pB_NextFigure.Width, 2 * indent + pB_Board.Height + menuBar.Height);
        }

        private void setLocations()
        {
            pB_Board.Location = new Point(indent, indent + menuBar.Height);
            pB_NextFigure.Location = new Point(pB_Board.Location.X + pB_Board.Width + indent, pB_Board.Location.Y);

            labelFiguresAmount.Location = new Point(pB_NextFigure.Location.X, pB_NextFigure.Location.Y + pB_NextFigure.Height + indent);
            labelScore.Location = new Point(pB_NextFigure.Location.X, labelFiguresAmount.Location.Y + labelFiguresAmount.Height + indent);
            labelLevel.Location = new Point(pB_NextFigure.Location.X, labelScore.Location.Y + labelScore.Height + indent);
            labelBestScore.Location = new Point(pB_NextFigure.Location.X, labelLevel.Location.Y + labelLevel.Height + indent);
        }

        private void pictureBoxBoard_Paint(object sender, PaintEventArgs e)
        {
            drawLines(e);
            drawFigure(e, middleCell, mainFigure);
            drawBoard(e);
        }

        private void pictureBoxNextFigure_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(penLines, 0, 0, 4 * cellSize, 4 * cellSize);
            drawFigure(e, middleCell_nextF, nextFigure);
        }

        private void drawLines(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(penLines, 0, 0, maxj * cellSize, maxi * cellSize);

            penLines.Color = Color.FromArgb(color_opacity, Color.Black);
            for (var i = 1; i < maxi; ++i)
            {
                if (i < maxj) e.Graphics.DrawLine(penLines, i * cellSize, 0, i * cellSize, maxi * cellSize);
                e.Graphics.DrawLine(penLines, 0, i * cellSize, maxj * cellSize, i * cellSize);
            }

            penLines.Color = Color.Red;
            e.Graphics.DrawLine(penLines, 0, 2 * cellSize, maxi * cellSize, 2 * cellSize);

            penLines.Color = Color.Black;
        }

        private void drawFigure(PaintEventArgs e, Point mainCell, Figure figure)
        {
            for (int i = figure.pointer; i < (figure.pointer + step); ++i)
                e.Graphics.FillRectangle( figure.color,
                                          (mainCell.Y + figure.cells[i].Y) * cellSize + plW_div2,
                                          (mainCell.X + figure.cells[i].X) * cellSize + plW_div2,
                                          cellSize - plW,
                                          cellSize - plW);

            e.Graphics.FillRectangle( figure.color,
                                      mainCell.Y * cellSize + plW_div2,
                                      mainCell.X * cellSize + plW_div2,
                                      cellSize - plW,
                                      cellSize - plW);
        }

        private void drawBoard(PaintEventArgs e)
        {
            for (int i = 0; i < maxi; ++i)
                for (int j = 0; j < maxj; ++j)
                    if (Board[i, j].closed)
                        e.Graphics.FillRectangle(Board[i, j].color, j * cellSize + plW_div2, i * cellSize + plW_div2, cellSize - plW, cellSize - plW);
        }

        //control

        private void pressKey_Down(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W: turnFigure(); return;

                case Keys.A: moveFigure(0, -1); return;

                case Keys.D: moveFigure(0, 1); return;

                case Keys.S: timer.Interval = 45; return;

                case Keys.Space: timer.Interval = 1; return;

                case Keys.F1: start(); return;

                case Keys.F2: menuItemInfo_Click(sender, e); return;
            }
        }

        private void pressKey_Up(object sender, KeyEventArgs e)
        {
            timer.Interval = speed;
        }

        //====== Logical structure ======//

        private void start()
        {
            createBoard();

            mainFigure = new Figure((FigureType)random.Next(Enum.GetValues(typeof(FigureType)).Length));
            nextFigure = new Figure((FigureType)random.Next(Enum.GetValues(typeof(FigureType)).Length));

            middleCell = middleCell_default;
            flag_to_create_NewFigure = false;
            speed = speed_default;
            timer.Interval = speed;

            figuresAmount = 0;
            score = 0;
            level = 1;
            changeTextOnLabels();
            labelBestScore.Text = "BEST: " + Properties.Settings.Default.score_best;
                              
            Refresh();
            timer.Start();
        }

        private void createBoard()
        {
            for (int i = 0; i < maxi; ++i)
                for (int j = 0; j < maxj; ++j)
                    Board[i, j] = new Cell(false, Brushes.Transparent);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            moveFigure(1, 0);

            if (flag_to_create_NewFigure)
            {
                addNewColorToBoard();
                checkFilledRows();

                if (checkLose())
                {
                    checkBestScore();
                    timer.Stop();

                    return;
                }

                mainFigure = nextFigure;
                nextFigure = new Figure((FigureType)random.Next(Enum.GetValues(typeof(FigureType)).Length));
                middleCell = middleCell_default;
                flag_to_create_NewFigure = false;
                timer.Interval = speed;

                Refresh();
            }
        }

        private void moveFigure(int dx, int dy)
        {
            if (timer.Enabled && checkMove(dx, dy))
            {
                middleCell.X += dx;
                middleCell.Y += dy;

                pB_Board.Refresh();

                return;
            }

            if (dx != 0)
                flag_to_create_NewFigure = true;
        }

        private void turnFigure()
        {
            if (!timer.Enabled) return;

            int pointer = mainFigure.pointer;

            if (mainFigure.pointer + step == mainFigure.cells.Length)
                mainFigure.pointer = 0;
            else
                mainFigure.pointer += step;

            if (!checkMove(0, 0))
                mainFigure.pointer = pointer;

            pB_Board.Refresh();
        }

        private void addNewColorToBoard()
        {
            Board[middleCell.X, middleCell.Y] = new Cell(true, mainFigure.color);

            for (int i = mainFigure.pointer; i < (mainFigure.pointer + step); ++i)
                Board[middleCell.X + mainFigure.cells[i].X, middleCell.Y + mainFigure.cells[i].Y] = new Cell(true, mainFigure.color);
        }

        private void checkFilledRows()
        {
            int bonus = 0, closedCells = 0;

            for (int i = 0; i < maxi; ++i)
            {
                for (int j = 0; j < maxj; ++j)
                    if (Board[i, j].closed) closedCells++;

                if (closedCells == maxj)
                {
                    deleteRow(i);
                    bonus = 2 * bonus + 100;
                }

                closedCells = 0;
            }

            changeOptions(bonus);
        }

        private void deleteRow(int row)
        {
            for (int j = 0; j < maxj; ++j)
                for (int i = row; i > 0; --i)
                    Board[i, j] = Board[i - 1, j];
        }

        private void changeOptions(int bonus)
        {
            figuresAmount++;
            score += bonus;

            if (score < 10000)
            {
                level = (score / 1000) + 1;
                speed = speed_default - (speed_default / 20) * level;
            }

            changeTextOnLabels();
        }

        private void changeTextOnLabels()
        {
            labelFiguresAmount.Text = "Bricks: " + figuresAmount.ToString();
            labelScore.Text = "Score: " + score.ToString();
            labelLevel.Text = "Level: " + level.ToString();
        }

        private bool checkMove(int dx, int dy)
        {
            Point newC, newCm = new Point(middleCell.X + dx, middleCell.Y + dy); // C - cell, m - middle 

            if (newCm.Y < 0 || newCm.Y >= maxj || newCm.X >= maxi) return false;
            else if (Board[newCm.X, newCm.Y].closed) return false;

            for (int i = mainFigure.pointer; i < (mainFigure.pointer + step); ++i)
            {
                newC = new Point(newCm.X + mainFigure.cells[i].X, newCm.Y + mainFigure.cells[i].Y);

                if (newC.Y < 0 || newC.Y >= maxj || newC.X >= maxi) return false;
                else if (Board[newC.X, newC.Y].closed) return false;
            }

            return true;
        }

        private bool checkLose()
        {
            for (int j = 0; j < maxj; ++j)
                if (Board[2, j].closed) return true;

            return false;
        }

        private void checkBestScore()
        {
            if (Properties.Settings.Default.score_best < score)
            {
                Properties.Settings.Default.score_best = score;
                Properties.Settings.Default.Save();
            }
        }

        private struct Figure
        {
            public FigureType figureType { get; private set; }
            public Brush color { get; private set; }
            public Point[] cells { get; private set; }
            public int pointer;

            public Figure(FigureType figure)
            {
                this = new Figure();

                figureType = figure;

                switch (figureType)
                {
                    case FigureType.T:
                        color = Brushes.Violet;
                        cells = new Point[] { new Point(0,-1), new Point(0, 1), new Point(1, 0),
                                              new Point(-1,0), new Point(1, 0), new Point(0,-1),
                                              new Point(0,-1), new Point(0, 1), new Point(-1,0),
                                              new Point(-1,0), new Point(1, 0), new Point(0, 1) };
                        break;
                    case FigureType.J:
                        color = Brushes.Blue;
                        cells = new Point[] { new Point(-1,0), new Point(1, 0), new Point(1,-1),
                                              new Point(0,-1), new Point(0, 1), new Point(-1,-1),
                                              new Point(-1,0), new Point(1, 0), new Point(-1,1),
                                              new Point(0,-1), new Point(0, 1), new Point(1, 1) };
                        break;
                    case FigureType.L:
                        color = Brushes.Orange;
                        cells = new Point[] { new Point(-1,0), new Point(1, 0), new Point(1, 1),
                                              new Point(0,-1), new Point(0, 1), new Point(1,-1),
                                              new Point(-1,0), new Point(1, 0), new Point(-1,-1),
                                              new Point(0,-1), new Point(0, 1), new Point(-1,1) };
                        break;
                    case FigureType.I:
                        color = Brushes.Aqua;
                        cells = new Point[] { new Point(0,-1), new Point(0, 1), new Point(0, 2),
                                              new Point(-1,0), new Point(1, 0), new Point(2, 0) };
                        break;
                    case FigureType.Z:
                        color = Brushes.Red;
                        cells = new Point[] { new Point(0,-1), new Point(1, 0), new Point(1, 1),
                                              new Point(0, 1), new Point(1, 0), new Point(-1,1) };
                        break;
                    case FigureType.S:
                        color = Brushes.Green;
                        cells = new Point[] { new Point(0, 1), new Point(1, 0), new Point(1,-1),
                                              new Point(0,-1), new Point(1, 0), new Point(-1,-1) };
                        break;
                    case FigureType.Q:
                        color = Brushes.Yellow;
                        cells = new Point[] { new Point(0, 1), new Point(1, 0), new Point(1, 1) };
                        break;
                    case FigureType.o:
                        color = Brushes.Yellow;
                        cells = new Point[] { new Point(0, 0), new Point(0, 0), new Point(0, 0) };
                        break;
                }

                if (figureType == FigureType.I) pointer = 0;
                else pointer = random.Next(cells.Length / step) * step;
            }
        }
    }
}
