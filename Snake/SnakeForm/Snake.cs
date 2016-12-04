using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeForm
{
    enum CellStatus { empty, body, food }

    enum GameStatus { game, win, lose }

    enum Direction { Up, Down, Left, Right }

    public partial class Snake : Form
    {

        private Random random = new Random();
        private Timer timer = new Timer();

        private const int max = 10, sizeCell = 30;
        private Cell[,] Board = new Cell[max, max];
        private PictureBox[,] arrayPictureBox = new PictureBox[max, max];

        private int amountOpenCells, score, bonusTime, points = 150, speed;
        private GameStatus status;
        private Point head, tail, ShiftSnake;
        private Keys command, WrongCommand, OldCommand;

        private Image
        imageApple = Properties.Resources.apple,

        imageHeadRight = Properties.Resources.headRight,
        imageHeadLeft = Properties.Resources.headLeft,
        imageHeadDown = Properties.Resources.headDown,
        imageHeadUp = Properties.Resources.headUp,
        imageHead,

        imageTailRight = Properties.Resources.tailRight,
        imageTailLeft = Properties.Resources.tailLeft,
        imageTailDown = Properties.Resources.tailDown,
        imageTailUp = Properties.Resources.tailUp,
        imageTail,

        imageTopRightCorner = Properties.Resources.TopRightCorner,
        imageTopLeftCorner = Properties.Resources.TopLeftCorner,
        imageLowerRightCorner = Properties.Resources.LowerRightCorner,
        imageLowerLeftCorner = Properties.Resources.LowerLeftCorner,
        imageLeftToRight = Properties.Resources.LeftToRight,
        imageUpToDown = Properties.Resources.UpToDown;

        public Snake()
        {
            InitializeComponent();
            restart();
        }

        // методы

        private void boardParameters()
        {
            int Width = 125, Height = 42;
            Point Location = new Point(10, 10);

            labelScore.Size =      new Size(Width, Height);
            labelLength.Size =     new Size(Width, Height);
            pictureBoxBoard.Size = new Size(max * sizeCell, max * sizeCell);
            ClientSize = new Size(pictureBoxBoard.Size.Width + Location.X*2, pictureBoxBoard.Size.Height + 2 * Height + Location.Y*2);

            labelScore.Location =      new Point(Location.X, Location.Y);
            labelLength.Location =     new Point(Location.X, Location.Y + Height);
            pictureBoxBoard.Location = new Point(Location.X, Height*2+Location.Y);
        }

        private void createArrayPictureBox()
        {
            for (int i = 0; i < max; ++i)
                for (int j = 0; j < max; ++j)
                {
                    arrayPictureBox[i, j] = new PictureBox();
                    arrayPictureBox[i, j].Size = new Size(sizeCell, sizeCell);
                    arrayPictureBox[i, j].Location = new Point(i * sizeCell, j * sizeCell);
                    arrayPictureBox[i, j].BackColor = Color.Transparent;
                    arrayPictureBox[i, j].BackgroundImageLayout = ImageLayout.Zoom;
                    pictureBoxBoard.Controls.Add(arrayPictureBox[i, j]);
                }
        }

        private void createCell()
        {
            for (int i = 0; i < max; ++i)
                for (int j = 0; j < max; ++j)
                    Board[i, j] = new Cell();
        }

        private void createSnake()
        {
            head = new Point(max / 2, max / 2);
            Board[head.X, head.Y].status = CellStatus.body;
            Board[head.X, head.Y].DirectionCell = Direction.Left;
            arrayPictureBox[head.X, head.Y].BackgroundImage = imageHeadLeft;

            tail = new Point(head.X + 1, head.Y);
            Board[tail.X, tail.Y].status = CellStatus.body;
            Board[tail.X, tail.Y].setLinkCell(head.X, head.Y);
            arrayPictureBox[tail.X, tail.Y].BackgroundImage = imageTailLeft;
        }

        private void initializeParameters()
        {
            status = GameStatus.game;
            amountOpenCells = max * max - 2;
            score = 0;
            bonusTime = 1;
            speed = 700;
            command = Keys.Left;
        }

        private void initializeTimer()
        {
            timer.Interval = speed;
            timer.Tick += timer_Tick;
        }

        private void generateFood()
        {
            int x, y;

            do
            {
                x = random.Next(0, max);
                y = random.Next(0, max);
            }
            while (Board[x, y].status == CellStatus.body);

            Board[x, y].status = CellStatus.food;
            arrayPictureBox[x, y].BackgroundImage = imageApple;
        }

            private void restart()
            {
                boardParameters();
                createCell();
                initializeParameters();
                createArrayPictureBox();
                createSnake();
                initializeTimer();
                generateFood();
                timer.Start();
            }

        private void PressKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                command = e.KeyCode;
        }

        private void settingShiftSnake()
        {
            if (command != WrongCommand)
            {
                switch (command)
                {
                    case Keys.Right:
                        ShiftSnake = new Point(1, 0);
                        WrongCommand = Keys.Left;
                        break;
                    case Keys.Left:
                        ShiftSnake = new Point(-1, 0);
                        WrongCommand = Keys.Right;
                        break;
                    case Keys.Down:
                        ShiftSnake = new Point(0, 1);
                        WrongCommand = Keys.Up;
                        break;
                    case Keys.Up:
                        ShiftSnake = new Point(0, -1);
                        WrongCommand = Keys.Down;
                        break;
                }
                OldCommand = command;
            }
            else command = OldCommand;
        }

        private void drawSnakeBody()
        {
            arrayPictureBox[head.X, head.Y].BackgroundImage = null;

            if ((command == Keys.Up && Board[head.X, head.Y].DirectionCell == Direction.Left) ||
                (command == Keys.Right && Board[head.X, head.Y].DirectionCell == Direction.Down))
                arrayPictureBox[head.X, head.Y].BackgroundImage = imageLowerLeftCorner;

            if ((command == Keys.Up   && Board[head.X, head.Y].DirectionCell == Direction.Right) ||
                (command == Keys.Left && Board[head.X, head.Y].DirectionCell == Direction.Down))
                arrayPictureBox[head.X, head.Y].BackgroundImage = imageLowerRightCorner;

            if ((command == Keys.Down && Board[head.X, head.Y].DirectionCell == Direction.Left) ||
                (command == Keys.Right && Board[head.X, head.Y].DirectionCell == Direction.Up))
                arrayPictureBox[head.X, head.Y].BackgroundImage = imageTopLeftCorner;

            if ((command == Keys.Down && Board[head.X, head.Y].DirectionCell == Direction.Right) ||
                (command == Keys.Left && Board[head.X, head.Y].DirectionCell == Direction.Up))
                arrayPictureBox[head.X, head.Y].BackgroundImage = imageTopRightCorner;

            if (command == Keys.Left && Board[head.X, head.Y].DirectionCell == Direction.Left)
                arrayPictureBox[head.X, head.Y].BackgroundImage = imageLeftToRight;

            if (command == Keys.Right && Board[head.X, head.Y].DirectionCell == Direction.Right)
                arrayPictureBox[head.X, head.Y].BackgroundImage = imageLeftToRight;

            if (command == Keys.Up && Board[head.X, head.Y].DirectionCell == Direction.Up)
                arrayPictureBox[head.X, head.Y].BackgroundImage = imageUpToDown;

            if (command == Keys.Down && Board[head.X, head.Y].DirectionCell == Direction.Down)
                arrayPictureBox[head.X, head.Y].BackgroundImage = imageUpToDown;
        }

        private void scoreUp()
        {
            if (bonusTime > points)
                score++;
            else
                score += points / bonusTime;

            bonusTime = 1;
            labelScore.Text = "Score: "+score.ToString();
            labelLength.Text = "length: " + (max*max - amountOpenCells).ToString();
        }

        private void move()
        {
            Point NewHead = new Point(head.X + ShiftSnake.X, head.Y + ShiftSnake.Y);
            bool SignalToGenerateFood = false;

            if (command == Keys.Down && NewHead.Y == max) NewHead.Y = 0;
            if (command == Keys.Up && NewHead.Y < 0) NewHead.Y += max;
            if (command == Keys.Right && NewHead.X == max) NewHead.X = 0;
            if (command == Keys.Left && NewHead.X < 0) NewHead.X += max;

            // если встретил еду, то пропускаю смещение хвоста
            if (Board[NewHead.X, NewHead.Y].status == CellStatus.food)
            {
                arrayPictureBox[NewHead.X, NewHead.Y].BackgroundImage = null;
                SignalToGenerateFood = true;
                amountOpenCells--;
                timer.Interval --;
                scoreUp();
                goto ShiftHead;
            }

            // врезалась ли голова в тело
            if (Board[NewHead.X, NewHead.Y].status == CellStatus.body && NewHead != tail)
            {
                status = GameStatus.lose;
                return;
            }

            // смещение хвоста
            Board[tail.X, tail.Y].status = CellStatus.empty;

            arrayPictureBox[tail.X, tail.Y].BackgroundImage = null;

            tail = new Point(Board[tail.X, tail.Y].LinkCell.X, Board[tail.X, tail.Y].LinkCell.Y);

        ShiftHead:
            //отрисовка тела
            drawSnakeBody();

            //смещение головы
            Board[head.X, head.Y].setLinkCell(NewHead.X, NewHead.Y);

            head = NewHead;

            Board[head.X, head.Y].status = CellStatus.body;

            //генерация еды и проверка на победу
            if (SignalToGenerateFood && amountOpenCells != 0) generateFood();
            if (amountOpenCells == 0) status = GameStatus.win;
        }

        private void drawSnakeHead()
        {
            switch (command)
            {
                case Keys.Right:
                    Board[head.X, head.Y].DirectionCell = Direction.Right;
                    imageHead = imageHeadRight;
                    break;
                case Keys.Left:
                    Board[head.X, head.Y].DirectionCell = Direction.Left;
                    imageHead = imageHeadLeft;
                    break;
                case Keys.Down:
                    Board[head.X, head.Y].DirectionCell = Direction.Down;
                    imageHead = imageHeadDown;
                    break;
                case Keys.Up:
                    Board[head.X, head.Y].DirectionCell = Direction.Up;
                    imageHead = imageHeadUp;
                    break;
            }
            if(status != GameStatus.lose)
            arrayPictureBox[head.X, head.Y].BackgroundImage = imageHead;
        }

        private void drawSnakeTail()
        {
            switch (Board[Board[tail.X, tail.Y].LinkCell.X, Board[tail.X, tail.Y].LinkCell.Y].DirectionCell)
            {
                case Direction.Right: imageTail = imageTailRight; break;
                case Direction.Left: imageTail = imageTailLeft; break;
                case Direction.Down: imageTail = imageTailDown; break;
                case Direction.Up: imageTail = imageTailUp; break;
            }
            arrayPictureBox[tail.X, tail.Y].BackgroundImage = imageTail;
        }

        private void messageAboutGameStatus()
        {
            if (status == GameStatus.win)
                MessageBox.Show("WIN. You score: " + score);
            else
                MessageBox.Show("LOSE. You score: " + score);

            Close();
        }

            private void timer_Tick(object sender, EventArgs e)
            {
                bonusTime++;
                settingShiftSnake();
                move();
                drawSnakeHead();
                drawSnakeTail();
                if (status != GameStatus.game)
                {
                    timer.Stop();
                    messageAboutGameStatus();
                }
            }
    }
}
