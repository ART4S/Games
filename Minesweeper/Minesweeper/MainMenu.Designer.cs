namespace Minesweeper
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.pictureMainMenu = new System.Windows.Forms.PictureBox();
            this.labelStart = new System.Windows.Forms.Label();
            this.labelSetting = new System.Windows.Forms.Label();
            this.labelExit = new System.Windows.Forms.Label();
            this.pictureBoxSetting = new System.Windows.Forms.PictureBox();
            this.labelLevel = new System.Windows.Forms.Label();
            this.labelNormal = new System.Windows.Forms.Label();
            this.labelEazy = new System.Windows.Forms.Label();
            this.labelHard = new System.Windows.Forms.Label();
            this.labelMusic = new System.Windows.Forms.Label();
            this.labelMusicOff = new System.Windows.Forms.Label();
            this.labelMusicOn = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureMainMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSetting)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureMainMenu
            // 
            this.pictureMainMenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureMainMenu.Image = global::Minesweeper.Properties.Resources.MainMenu_Animation;
            this.pictureMainMenu.Location = new System.Drawing.Point(0, 0);
            this.pictureMainMenu.Name = "pictureMainMenu";
            this.pictureMainMenu.Size = new System.Drawing.Size(400, 225);
            this.pictureMainMenu.TabIndex = 0;
            this.pictureMainMenu.TabStop = false;
            // 
            // labelStart
            // 
            this.labelStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStart.AutoSize = true;
            this.labelStart.BackColor = System.Drawing.Color.Transparent;
            this.labelStart.Font = new System.Drawing.Font("Segoe Script", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStart.ForeColor = System.Drawing.Color.White;
            this.labelStart.Location = new System.Drawing.Point(21, 41);
            this.labelStart.Name = "labelStart";
            this.labelStart.Size = new System.Drawing.Size(87, 32);
            this.labelStart.TabIndex = 1;
            this.labelStart.Text = "START";
            this.labelStart.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelStart_MouseClick);
            // 
            // labelSetting
            // 
            this.labelSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSetting.AutoSize = true;
            this.labelSetting.BackColor = System.Drawing.Color.Transparent;
            this.labelSetting.Font = new System.Drawing.Font("Segoe Script", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSetting.ForeColor = System.Drawing.Color.White;
            this.labelSetting.Location = new System.Drawing.Point(21, 73);
            this.labelSetting.Name = "labelSetting";
            this.labelSetting.Size = new System.Drawing.Size(123, 32);
            this.labelSetting.TabIndex = 2;
            this.labelSetting.Text = "SETTINGS";
            this.labelSetting.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelSetting_MouseClick);
            // 
            // labelExit
            // 
            this.labelExit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labelExit.AutoSize = true;
            this.labelExit.BackColor = System.Drawing.Color.Transparent;
            this.labelExit.Font = new System.Drawing.Font("Segoe Script", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelExit.ForeColor = System.Drawing.Color.White;
            this.labelExit.Location = new System.Drawing.Point(21, 105);
            this.labelExit.Name = "labelExit";
            this.labelExit.Size = new System.Drawing.Size(64, 32);
            this.labelExit.TabIndex = 3;
            this.labelExit.Text = "EXIT";
            this.labelExit.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelExit_MouseClick);
            // 
            // pictureBoxSetting
            // 
            this.pictureBoxSetting.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxSetting.Location = new System.Drawing.Point(153, 41);
            this.pictureBoxSetting.Name = "pictureBoxSetting";
            this.pictureBoxSetting.Size = new System.Drawing.Size(200, 200);
            this.pictureBoxSetting.TabIndex = 4;
            this.pictureBoxSetting.TabStop = false;
            this.pictureBoxSetting.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxSetting_Paint);
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.BackColor = System.Drawing.Color.Transparent;
            this.labelLevel.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLevel.ForeColor = System.Drawing.Color.White;
            this.labelLevel.Location = new System.Drawing.Point(383, 9);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(33, 17);
            this.labelLevel.TabIndex = 6;
            this.labelLevel.Text = "easy";
            this.labelLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelLevel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelLevel_MouseClick);
            // 
            // labelNormal
            // 
            this.labelNormal.AutoSize = true;
            this.labelNormal.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNormal.ForeColor = System.Drawing.Color.White;
            this.labelNormal.Location = new System.Drawing.Point(383, 83);
            this.labelNormal.Name = "labelNormal";
            this.labelNormal.Size = new System.Drawing.Size(49, 17);
            this.labelNormal.TabIndex = 7;
            this.labelNormal.Text = "normal";
            this.labelNormal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelEazy
            // 
            this.labelEazy.AutoSize = true;
            this.labelEazy.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEazy.ForeColor = System.Drawing.Color.White;
            this.labelEazy.Location = new System.Drawing.Point(383, 51);
            this.labelEazy.Name = "labelEazy";
            this.labelEazy.Size = new System.Drawing.Size(33, 17);
            this.labelEazy.TabIndex = 8;
            this.labelEazy.Text = "easy";
            this.labelEazy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelHard
            // 
            this.labelHard.AutoSize = true;
            this.labelHard.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelHard.ForeColor = System.Drawing.Color.White;
            this.labelHard.Location = new System.Drawing.Point(383, 115);
            this.labelHard.Name = "labelHard";
            this.labelHard.Size = new System.Drawing.Size(38, 17);
            this.labelHard.TabIndex = 9;
            this.labelHard.Text = "hard";
            this.labelHard.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMusic
            // 
            this.labelMusic.AutoSize = true;
            this.labelMusic.BackColor = System.Drawing.Color.Transparent;
            this.labelMusic.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMusic.ForeColor = System.Drawing.Color.White;
            this.labelMusic.Location = new System.Drawing.Point(12, 150);
            this.labelMusic.Name = "labelMusic";
            this.labelMusic.Size = new System.Drawing.Size(23, 17);
            this.labelMusic.TabIndex = 10;
            this.labelMusic.Text = "off";
            this.labelMusic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelMusic.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelMusic_MouseClick);
            // 
            // labelMusicOff
            // 
            this.labelMusicOff.AutoSize = true;
            this.labelMusicOff.BackColor = System.Drawing.Color.Transparent;
            this.labelMusicOff.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMusicOff.ForeColor = System.Drawing.Color.White;
            this.labelMusicOff.Location = new System.Drawing.Point(12, 178);
            this.labelMusicOff.Name = "labelMusicOff";
            this.labelMusicOff.Size = new System.Drawing.Size(23, 17);
            this.labelMusicOff.TabIndex = 11;
            this.labelMusicOff.Text = "off";
            this.labelMusicOff.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelMusicOn
            // 
            this.labelMusicOn.AutoSize = true;
            this.labelMusicOn.BackColor = System.Drawing.Color.Transparent;
            this.labelMusicOn.Font = new System.Drawing.Font("Segoe Script", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMusicOn.ForeColor = System.Drawing.Color.White;
            this.labelMusicOn.Location = new System.Drawing.Point(12, 208);
            this.labelMusicOn.Name = "labelMusicOn";
            this.labelMusicOn.Size = new System.Drawing.Size(22, 17);
            this.labelMusicOn.TabIndex = 12;
            this.labelMusicOn.Text = "on";
            this.labelMusicOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(594, 438);
            this.Controls.Add(this.labelMusicOn);
            this.Controls.Add(this.labelMusicOff);
            this.Controls.Add(this.labelMusic);
            this.Controls.Add(this.labelHard);
            this.Controls.Add(this.labelEazy);
            this.Controls.Add(this.labelNormal);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.pictureBoxSetting);
            this.Controls.Add(this.labelExit);
            this.Controls.Add(this.labelSetting);
            this.Controls.Add(this.labelStart);
            this.Controls.Add(this.pictureMainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureMainMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxSetting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureMainMenu;
        private System.Windows.Forms.Label labelStart;
        private System.Windows.Forms.Label labelSetting;
        private System.Windows.Forms.Label labelExit;
        private System.Windows.Forms.PictureBox pictureBoxSetting;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.Label labelNormal;
        private System.Windows.Forms.Label labelEazy;
        private System.Windows.Forms.Label labelHard;
        private System.Windows.Forms.Label labelMusic;
        private System.Windows.Forms.Label labelMusicOff;
        private System.Windows.Forms.Label labelMusicOn;
    }
}