using System.Collections.Generic;
using System.Windows.Forms;

namespace AirForce
{
    public sealed partial class MainForm : Form
    {
        private readonly GameController gameController;

        private readonly Dictionary<Keys, bool> pressedKeys;

        private readonly Timer drawingTimer = new Timer();
        private readonly Timer playerShootTimer = new Timer();
        private readonly Timer playerMoveTimer = new Timer();

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

            // drawingTimer setting
            drawingTimer.Interval = 1;
            drawingTimer.Tick += (s, e) => GameFieldPictureBox.Refresh();
            drawingTimer.Start();

            // playerShootTimer setting
            playerShootTimer.Interval = 300;
            playerShootTimer.Tick += (s, e) => gameController.TryCreatePlayerBullet();

            // playerMoveTimer setting
            playerMoveTimer.Interval = 1;
            playerMoveTimer.Tick += (s, e) => MovePlayer();
            playerMoveTimer.Start();
        }

        private void GameFieldPictureBox_Paint(object sender, PaintEventArgs e)
        {
            gameController.DrawAllElements(e.Graphics);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;

            if (!pressedKeys.ContainsKey(pressedKey))
                return;

            pressedKeys[pressedKey] = true;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            Keys depressedKey = e.KeyCode;

            if (!pressedKeys.ContainsKey(depressedKey))
                return;

            pressedKeys[depressedKey] = false;
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
                if (playerShootTimer.Enabled == false)
                {
                    gameController.TryCreatePlayerBullet();
                    playerShootTimer.Start();
                }
            }
            else
                playerShootTimer.Stop();

            gameController.TryMovePlayer(playerMovespeedModifer);
        }
    }
}
