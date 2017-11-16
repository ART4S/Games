using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    public partial class MainForm : Form
    {
        private readonly GameController gameController;
        private readonly Timer timer = new Timer();

        public MainForm()
        {
            InitializeComponent();

            gameController = new GameController(GameFieldPictureBox.Size);
            GameFieldPictureBox.BackColor = Color.Aqua;

            timer.Interval = 1;
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            GameFieldPictureBox.Refresh();
        }

        private void GameFieldPictureBox_Paint(object sender, PaintEventArgs e)
        {
            gameController.DrawAllElements(e.Graphics);
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            gameController.ChangePlayerShipBehaviour(e.KeyCode);
        }
    }
}
