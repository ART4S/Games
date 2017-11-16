using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    public partial class MainForm : Form
    {
        private readonly GameController gameController;

        private delegate void ResizeMethod(Size newSize);
        private event ResizeMethod OnChangeGameFieldSize;


        public MainForm()
        {
            InitializeComponent();

            gameController = new GameController(GameFieldPictureBox.Size);

            OnChangeGameFieldSize += gameController.ResizeGameFieldBorders;

            GameFieldPictureBox.BackColor = Color.Aqua;
        }

        private void GameFieldPictureBox_Paint(object sender, PaintEventArgs e)
        {
            gameController.DrawAllElements(e.Graphics);
        }

        private void MainForm_Resize(object sender, System.EventArgs e)
        {
            OnChangeGameFieldSize?.Invoke(GameFieldPictureBox.Size);
            GameFieldPictureBox.Refresh();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            gameController.ChangePlayerShipBehaviour(e.KeyCode);
        }
    }
}
