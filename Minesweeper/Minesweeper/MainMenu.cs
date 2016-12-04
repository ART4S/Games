using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class MainMenu : Form
    {
        private int labelFontMax = 18,
                    labelFontMin = 15,
                    interval = 20,
                    level = 1;

        string[] namesLabelsInLeftColumn = { "level :", "music :" };
        private Color color = Color.Black;
        private Font font = new Font("Segoe Script", 10, FontStyle.Regular);

        public MainMenu()
        {
            InitializeComponent();
            settingClientWindow();
            settingMainLabels();
            settingPanelSettings();
        }

        // настройка формы

        private void settingClientWindow()
        {
            Image backImage = Properties.Resources.MainMenu_Animation;

            ClientSize = new Size(backImage.Width - 100, backImage.Height - 10);
            pictureMainMenu.Size = new Size(ClientSize.Width, ClientSize.Height);
            StartPosition = FormStartPosition.CenterScreen;

            backImage.Dispose();
        }

        private void settingMainLabels()
        {
            // добавление на pictureMainMenu
            pictureMainMenu.Controls.Add(labelStart);
            pictureMainMenu.Controls.Add(labelSetting);
            pictureMainMenu.Controls.Add(labelExit);

            // наведение курсора
            labelStart.MouseHover += new EventHandler(labelMainMenu_MouseHover);
            labelStart.MouseLeave += new EventHandler(labelMainMenu_MouseLeave);

            labelSetting.MouseHover += new EventHandler(labelMainMenu_MouseHover);
            labelSetting.MouseLeave += new EventHandler(labelMainMenu_MouseLeave);

            labelExit.MouseHover += new EventHandler(labelMainMenu_MouseHover);
            labelExit.MouseLeave += new EventHandler(labelMainMenu_MouseLeave);
        }

        private void settingPanelSettings()
        {
            int startPosX = 50, startPosY = 20;

            // pictureBoxSetting
            pictureBoxSetting.Location = new Point(labelSetting.Location.X + labelSetting.Width + 25, labelStart.Location.Y);
            pictureMainMenu.Controls.Add(pictureBoxSetting);
            pictureBoxSetting.Visible = false;

            // "level"
            setParamLabel(labelLevel, new Point(startPosX, startPosY), Color.Transparent);
            setParamLabel(labelEazy, new Point(startPosX, labelLevel.Location.Y + interval), Color.Black);
            setParamLabel(labelNormal, new Point(startPosX, labelEazy.Location.Y + interval), Color.Black);
            setParamLabel(labelHard, new Point(startPosX, labelNormal.Location.Y + interval), Color.Black);

            setEventsHandlersLabel(labelEazy, labelSettingsMenuItem_MouseHover, labelSettingsMenuItem_MouseLeave, labelLevelSettingsMenuItem_MouseClick);
            setEventsHandlersLabel(labelNormal, labelSettingsMenuItem_MouseHover, labelSettingsMenuItem_MouseLeave, labelLevelSettingsMenuItem_MouseClick);
            setEventsHandlersLabel(labelHard, labelSettingsMenuItem_MouseHover, labelSettingsMenuItem_MouseLeave, labelLevelSettingsMenuItem_MouseClick);

            // "music"
            setParamLabel(labelMusic, new Point(startPosX, labelLevel.Location.Y + interval), Color.Transparent);
            setParamLabel(labelMusicOn, new Point(startPosX, labelMusic.Location.Y + interval), Color.Black);
            setParamLabel(labelMusicOff, new Point(startPosX, labelMusicOn.Location.Y + interval), Color.Black);

            setEventsHandlersLabel(labelMusicOn, labelSettingsMenuItem_MouseHover, labelSettingsMenuItem_MouseLeave, labelMusicSettingsMenuItem_MouseClick);
            setEventsHandlersLabel(labelMusicOff, labelSettingsMenuItem_MouseHover, labelSettingsMenuItem_MouseLeave, labelMusicSettingsMenuItem_MouseClick);
        }

        private void setParamLabel(Label label, Point location, Color color)
        {
            label.Size = new Size(75, 20);
            label.Location = location;
            label.Font = font;
            label.BackColor = color;
            pictureBoxSetting.Controls.Add(label);
            label.AutoSize = false;

            string[] namesLabelsInRightColumn = { labelLevel.Name, labelMusic.Name };

            foreach (string name in namesLabelsInRightColumn)
                if (label.Name == name) return;

            label.Visible = false;
        }

        private void setEventsHandlersLabel(Label label, EventHandler hoverFunc, EventHandler leaveFunc, MouseEventHandler mouseClickFunc)
        {
            label.MouseHover += new EventHandler(hoverFunc);
            label.MouseLeave += new EventHandler(leaveFunc);
            label.MouseClick += new MouseEventHandler(mouseClickFunc);
        }

        // события

            //рисование окна настроек
        private void pictureBoxSetting_Paint(object sender, PaintEventArgs e)
        {
            int interval = this.interval;

            // рисование верхней надписи
            e.Graphics.DrawString("SETTINGS", font, Brushes.White, 25, 0);

            // рисование пунктов в левой колонке
            foreach (string name in namesLabelsInLeftColumn)
            {
                e.Graphics.DrawString(name, font, Brushes.White, 5, interval);
                interval += this.interval;
            }
        }

            //labelMainMenu
        private void labelMainMenu_MouseHover(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Font = new Font(label.Font.Name, labelFontMax, FontStyle.Regular);
            pictureBoxSetting.Visible = false;
        }

        private void labelMainMenu_MouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Font = new Font(label.Font.Name, labelFontMin, FontStyle.Regular);
        }

        private void labelStart_MouseClick(object sender, MouseEventArgs e)
        {
            MinesweeperForm game = new MinesweeperForm(level, this);
            game.Show();
            Hide();
        }

        private void labelSetting_MouseClick(object sender, MouseEventArgs e)
        {
            if (!pictureBoxSetting.Visible)
                pictureBoxSetting.Visible = true;
            else
                pictureBoxSetting.Visible = false;
        }

        private void labelExit_MouseClick(object sender, MouseEventArgs e)
        {
            Application.Exit();
        }

            //labelSettingsMenu
        private void labelsLevelVisible(bool param)
        {
            labelEazy.Visible = param;
            labelHard.Visible = param;
            labelNormal.Visible = param;
        }

        private void labelsMusicVisible(bool param)
        {
            labelMusicOn.Visible = param;
            labelMusicOff.Visible = param;
        }

        private void labelLevel_MouseClick(object sender, MouseEventArgs e)
        {
            // скрытие остальных меток
            labelsMusicVisible(false);

            // открытие|скрытие текущей
            if (!labelEazy.Visible)
                labelsLevelVisible(true);
            else
                labelsLevelVisible(false);
        }

        private void labelMusic_MouseClick(object sender, MouseEventArgs e)
        {   
            // скрытие остальных меток
            labelsLevelVisible(false);

            // открытие|скрытие текущей
            if (!labelMusicOff.Visible)
                labelsMusicVisible(true);
            else
                labelsMusicVisible(false);
        }

            //labelSettingsMenuItem
        private void labelSettingsMenuItem_MouseHover(object sender, EventArgs e)
        {
            ((Label)sender).Font = new Font(font.Name, 12, FontStyle.Regular);
        }

        private void labelSettingsMenuItem_MouseLeave(object sender, EventArgs e)
        {
            ((Label)sender).Font = font;
        }

        private void labelLevelSettingsMenuItem_MouseClick(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;

            labelsLevelVisible(false);
            labelLevel.Text = label.Text;

            switch (label.Text)
            {
                case "easy":   level = 1; break;
                case "normal": level = 2; break;
                case "hard":   level = 3; break;
            }
        }

        private void labelMusicSettingsMenuItem_MouseClick(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;

            labelsMusicVisible(false);
            labelMusic.Text = label.Text;

            switch (label.Text)
            {
                case "off": break;
                case "on":  break;
            }
        }
    }
}
