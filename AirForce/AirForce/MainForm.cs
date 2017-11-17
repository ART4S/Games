using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
            gameController.ChangePlayerShipBehaviour(e.KeyCode);
        }
    }
}
