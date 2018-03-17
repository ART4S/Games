using System.Collections.Generic;
using System.Windows.Forms;

namespace AirForce
{
    public sealed partial class MainForm : Form
    {
        private readonly GameController gameController;
        private readonly Dictionary<Keys, bool> pressedKeys;

        private readonly Timer drawingTimer = new Timer();
        private readonly Timer playerShootingTimer = new Timer();
        private readonly Timer playerMovingTimer = new Timer();

        public MainForm()
        {
            InitializeComponent();

            gameController = new GameController(GameFieldPictureBox.Size);

            pressedKeys = new Dictionary<Keys, bool>
            {
                {Keys.W, false},
                {Keys.S, false},
                {Keys.A, false},
                {Keys.D, false},
                {Keys.Space, false}
            };

            drawingTimer.Interval = 1;
            drawingTimer.Tick += (s, e) => GameFieldPictureBox.Refresh();
            drawingTimer.Start();

            playerShootingTimer.Interval = 300;
            playerShootingTimer.Tick += (s, e) => gameController.PlayerFire();

            playerMovingTimer.Interval = 1;
            playerMovingTimer.Tick += (s, e) => MovePlayer();
            playerMovingTimer.Start();
        }

        private void GameFieldPictureBox_Paint(object sender, PaintEventArgs e)
        {
            gameController.Paint(e.Graphics);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;

            if (pressedKey == Keys.Enter)
                gameController.Restart();

            if (pressedKey == Keys.R)
                gameController.StartRewind();

            if (!pressedKeys.ContainsKey(pressedKey))
                return;

            pressedKeys[pressedKey] = true;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            Keys unpressedKey = e.KeyCode;

            if (unpressedKey == Keys.R)
                gameController.EndRewind();

            if (!pressedKeys.ContainsKey(unpressedKey))
                return;

            pressedKeys[unpressedKey] = false;
        }

        private void MovePlayer()
        {
            Point2D playerMovespeedModifer = new Point2D();

            if (pressedKeys[Keys.W])
                playerMovespeedModifer = new Point2D(0, -1);

            if (pressedKeys[Keys.S])
                playerMovespeedModifer = new Point2D(0, 1);

            if (pressedKeys[Keys.A])
                playerMovespeedModifer = new Point2D(-1, 0);

            if (pressedKeys[Keys.D])
                playerMovespeedModifer = new Point2D(1, 0);

            if (pressedKeys[Keys.W] && pressedKeys[Keys.A])
                playerMovespeedModifer = new Point2D(-1, -1);

            if (pressedKeys[Keys.W] && pressedKeys[Keys.D])
                playerMovespeedModifer = new Point2D(1, -1);

            if (pressedKeys[Keys.S] && pressedKeys[Keys.A])
                playerMovespeedModifer = new Point2D(-1, 1);

            if (pressedKeys[Keys.S] && pressedKeys[Keys.D])
                playerMovespeedModifer = new Point2D(1, 1);

            if (pressedKeys[Keys.Space])
            {
                if (playerShootingTimer.Enabled == false)
                {
                    gameController.PlayerFire();
                    playerShootingTimer.Start();
                }
            }
            else
                playerShootingTimer.Stop();

            gameController.MovePlayer(playerMovespeedModifer);
        }
    }
}
