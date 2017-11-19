using System;
using System.Drawing;
using System.Windows.Forms;
using AirForce.Enums;

namespace AirForce
{
    public sealed partial class MainForm : Form
    {
        private readonly GameController gameController;
        private readonly Timer drawingTimer = new Timer();

        public MainForm()
        {
            InitializeComponent();

            gameController = new GameController(GameFieldPictureBox.Size);
            GameFieldPictureBox.BackColor = Color.Aqua;

            drawingTimer.Interval = 1;
            drawingTimer.Tick += DrawingTimerTick;
            drawingTimer.Start();
        }

        private void DrawingTimerTick(object sender, EventArgs e)
        {
            GameFieldPictureBox.Refresh();
        }

        private void GameFieldPictureBox_Paint(object sender, PaintEventArgs e)
        {
            gameController.DrawAllElements(e.Graphics);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;

            switch (pressedKey)
            {
                case Keys.W:
                case Keys.Up:
                    gameController.TryPlayerShipMove(Direction.Up);
                    break;

                case Keys.S:
                case Keys.Down:
                    gameController.TryPlayerShipMove(Direction.Down);
                    break;

                case Keys.D:
                case Keys.Left:
                    gameController.TryPlayerShipMove(Direction.Left);
                    break;

                case Keys.A:
                case Keys.Right:
                    gameController.TryPlayerShipMove(Direction.Right);
                    break;

                case Keys.Space:
                    gameController.TryPlayerShipShoot();
                    break;
            }
        }
    }
}
