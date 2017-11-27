using System.Windows.Forms;

namespace AirForce
{
    public sealed partial class MainForm : Form
    {
        private readonly GameController gameController;

        private readonly Timer drawingTimer = new Timer();
        private readonly Timer playerShootTimer = new Timer();
        private readonly Timer playerMoveTimer = new Timer();

        private Point2D playerMovespeedModifer;

        public MainForm()
        {
            InitializeComponent();

            gameController = new GameController(GameFieldPictureBox.Size);

            // drawingTimer setting
            drawingTimer.Interval = 1;
            drawingTimer.Tick += (s, e) => GameFieldPictureBox.Refresh();
            drawingTimer.Start();

            // playerShootTimer setting
            playerShootTimer.Interval = 300;
            playerShootTimer.Tick += (s, e) => gameController.TryCreatePlayerBullet();

            // playerMoveTimer setting
            playerMoveTimer.Interval = 1;
            playerMoveTimer.Tick += (s, e) => gameController.TryMovePlayer(playerMovespeedModifer);
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
                case Keys.Up:
                    playerMovespeedModifer = new Point2D(0, -1);
                    break;

                case Keys.Down:
                    playerMovespeedModifer = new Point2D(0, 1);
                    break;

                case Keys.Left:
                    playerMovespeedModifer = new Point2D(-1, 0);
                    break;

                case Keys.Right:
                    playerMovespeedModifer = new Point2D(1, 0);
                    break;

                case Keys.Space:
                    if (playerShootTimer.Enabled == false)
                    {
                        gameController.TryCreatePlayerBullet();
                        playerShootTimer.Start();
                    }
                    break;
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                playerShootTimer.Stop();

            playerMovespeedModifer = new Point2D();
        }
    }
}
