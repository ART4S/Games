using System;
using System.Windows.Forms;

namespace Ball_Breaker
{
    public partial class Form_settings : Form
    {
        private readonly Game mainForm;

        public Form_settings(Game mainForm)
        {
            InitializeComponent();

            this.mainForm = mainForm;

            SetGameMode();
            SetDelay();
        }

        private void SetGameMode()
        {
            var i = 0;

            foreach (var elem in Enum.GetNames(typeof(GameMode)))
            {
                cB_mode.Items.Add(elem);
            }

            while (i < cB_mode.Items.Count && cB_mode.Items[i].ToString() != mainForm.Mode.ToString()) ++i;

            cB_mode.SelectedIndex = i;                 
            cB_mode.Refresh();
        }

        private void SetDelay()
        {
            tB_Delay.Value = mainForm.Delay;
            tB_Delay.Refresh();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            mainForm.Mode = (GameMode)cB_mode.SelectedIndex;
            mainForm.Delay = tB_Delay.Value;

            Close();
        }
    }
}
