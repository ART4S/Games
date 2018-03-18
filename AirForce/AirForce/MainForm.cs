using System.Windows.Forms;

namespace AirForce
{
    public sealed partial class MainForm : Form
    {
        private readonly GameController gameController;

        public MainForm()
        {
            InitializeComponent();

            gameController = new GameController(display: GameFieldPictureBox);
        }


        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            gameController.KeyDown(e.KeyCode);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            gameController.KeyUp(e.KeyCode);
        }


    }
}
