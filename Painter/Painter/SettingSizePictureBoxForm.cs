using System;
using System.Windows.Forms;

namespace Painter
{
    public partial class SettingSizePictureBoxForm : Form
    {
        private readonly PictureBox pictureBox;

        public SettingSizePictureBoxForm(PictureBox pictureBox)
        {
            InitializeComponent();

            this.pictureBox = pictureBox;

            widthNumericUpDown.Value = pictureBox.Width;
            heightNumericUpDown.Value = pictureBox.Height;
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            pictureBox.Width = (int) widthNumericUpDown.Value;
            pictureBox.Height = (int) heightNumericUpDown.Value;

            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
