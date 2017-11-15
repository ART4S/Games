using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    public partial class MainForm : Form
    {
        private readonly GameController gameController = new GameController();

        public MainForm()
        {
            InitializeComponent();

            GameFieldPictureBox.BackColor = Color.Aqua;
        }

        private void GameFieldPictureBox_Paint(object sender, PaintEventArgs e)
        {
            gameController.DrawAllElements(e.Graphics, GameFieldPictureBox.Size);
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            GameFieldPictureBox.Refresh();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            gameController.MakingPlayerShipAct(e.KeyCode);
        }
    }
}
