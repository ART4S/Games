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
        private readonly Timer shootTimer = new Timer();

        public MainForm()
        {
            InitializeComponent();

            gameController = new GameController(GameFieldPictureBox.Size);
            GameFieldPictureBox.BackColor = Color.DarkBlue;

            drawingTimer.Interval = 1;
            drawingTimer.Tick += DrawingTimerTick;
            drawingTimer.Start();

            shootTimer.Interval = 400;
            shootTimer.Tick += MakePlayerShot;
        }

        private void DrawingTimerTick(object sender, EventArgs e)
        {
            GameFieldPictureBox.Refresh();
        }

        private void GameFieldPictureBox_Paint(object sender, PaintEventArgs e)
        {
            gameController.DrawAllElements(e.Graphics);
            //label1.Text = gameController.PlayerShipKillAmount.ToString();
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
                    if (shootTimer.Enabled == false)
                    {
                        gameController.TryCreatePlayerBullet();
                        shootTimer.Start();
                    }
                    break;

                case Keys.R:
                    gameController.Restart();
                    break;
            }
        }

        private void MakePlayerShot(object sender, EventArgs e)
        {
            gameController.TryCreatePlayerBullet();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                shootTimer.Stop();
        }
    }
}
