using System;
using System.Windows.Forms;

namespace Tetris
{
    public partial class SetOpacityLinesForm : Form
    {
        private Game mainForm;

        public SetOpacityLinesForm(Game mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;
            trackBar.Value = mainForm.color_opacity;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.color_opacity = (short)trackBar.Value;
            Properties.Settings.Default.Save();

            Close();
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            mainForm.color_opacity = trackBar.Value;
            mainForm.Refresh();
        }
    }
}
