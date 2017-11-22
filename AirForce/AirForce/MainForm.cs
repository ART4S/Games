using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    public sealed partial class MainForm : Form
    {
        private readonly GameController gameController;

        private readonly Timer drawingTimer = new Timer();
        private readonly Timer playerShootTimer = new Timer();
        private readonly Timer playerMoveTimer = new Timer();

        private int playerMovespeedModiferX;
        private int playerMovespeedModiferY;

        public MainForm()
        {
            InitializeComponent();

            gameController = new GameController(GameFieldPictureBox.Size);
            GameFieldPictureBox.BackColor = Color.DarkBlue;

            drawingTimer.Interval = 1;
            drawingTimer.Tick += (s, e) => GameFieldPictureBox.Refresh();
            drawingTimer.Start();

            playerShootTimer.Interval = 400;
            playerShootTimer.Tick += (s, e) => gameController.TryCreatePlayerBullet();

            playerMoveTimer.Interval = 1;
            playerMoveTimer.Tick += (s, e) => gameController.TryPlayerShipMove(playerMovespeedModiferX, playerMovespeedModiferY);

            playerMoveTimer.Start();
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
                    playerMovespeedModiferX = 0;
                    playerMovespeedModiferY = -1;
                    break;

                case Keys.S:
                case Keys.Down:
                    playerMovespeedModiferX = 0;
                    playerMovespeedModiferY = 1;
                    break;

                case Keys.A:
                case Keys.Left:
                    playerMovespeedModiferX = -1;
                    playerMovespeedModiferY = 0;
                    break;

                case Keys.D:
                case Keys.Right:
                    playerMovespeedModiferX = 1;
                    playerMovespeedModiferY = 0;
                    break;

                case Keys.Space:
                    if (playerShootTimer.Enabled == false)
                    {
                        gameController.TryCreatePlayerBullet();
                        playerShootTimer.Start();
                    }
                    break;

                case Keys.R:
                    gameController.Restart();
                    break;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                playerShootTimer.Stop();

            playerMovespeedModiferX = 0;
            playerMovespeedModiferY = 0;
        }
    }

    //public sealed class PlayerBehaviourController
    //{
    //    private GameController gameController;

    //    private readonly Timer playerShootTimer = new Timer();
    //    private readonly Timer playerMoveTimer = new Timer();

    //    public PlayerBehaviourController(GameController gameController)
    //    {
    //        this.gameController = gameController;

    //        playerShootTimer.Interval = 400;
    //        playerShootTimer.Tick += (s, e) => gameController.TryCreatePlayerBullet();

    //        playerMoveTimer.Interval = 1;
    //        playerMoveTimer.Tick += (s, e) => gameController.TryPlayerShipMove(1, 1);
    //    }

    //    public void Start()
    //    {
    //        playerMoveTimer.Start();
    //        playerShootTimer.Start();
    //    }
    //}
}
