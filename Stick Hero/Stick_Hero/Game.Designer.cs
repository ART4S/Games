namespace Stick_Hero
{
    partial class Game
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.labelName = new System.Windows.Forms.Label();
            this.labelPlay = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.labelStartScreen = new System.Windows.Forms.Label();
            this.labelRestart = new System.Windows.Forms.Label();
            this.labelInfoDevelopers = new System.Windows.Forms.Label();
            this.labelSite = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelName
            // 
            this.labelName.BackColor = System.Drawing.Color.Transparent;
            this.labelName.Font = new System.Drawing.Font("Tahoma", 38.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.ForeColor = System.Drawing.Color.Black;
            this.labelName.Image = global::Stick_Hero.Properties.Resources.im_Name;
            this.labelName.Location = new System.Drawing.Point(52, 9);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(185, 124);
            this.labelName.TabIndex = 0;
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPlay
            // 
            this.labelPlay.BackColor = System.Drawing.Color.Transparent;
            this.labelPlay.Font = new System.Drawing.Font("Tahoma", 17.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlay.ForeColor = System.Drawing.Color.Transparent;
            this.labelPlay.Image = global::Stick_Hero.Properties.Resources.im_Play;
            this.labelPlay.Location = new System.Drawing.Point(84, 145);
            this.labelPlay.Name = "labelPlay";
            this.labelPlay.Size = new System.Drawing.Size(125, 125);
            this.labelPlay.TabIndex = 2;
            this.labelPlay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelPlay.Click += new System.EventHandler(this.labelPlay_Click);
            // 
            // timer
            // 
            this.timer.Interval = 120;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // labelStartScreen
            // 
            this.labelStartScreen.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelStartScreen.BackColor = System.Drawing.Color.Transparent;
            this.labelStartScreen.Image = global::Stick_Hero.Properties.Resources.im_Start;
            this.labelStartScreen.Location = new System.Drawing.Point(60, 286);
            this.labelStartScreen.Name = "labelStartScreen";
            this.labelStartScreen.Size = new System.Drawing.Size(44, 44);
            this.labelStartScreen.TabIndex = 3;
            this.labelStartScreen.Click += new System.EventHandler(this.labelStartScreen_Click);
            // 
            // labelRestart
            // 
            this.labelRestart.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelRestart.BackColor = System.Drawing.Color.Transparent;
            this.labelRestart.Image = global::Stick_Hero.Properties.Resources.im_Restart;
            this.labelRestart.Location = new System.Drawing.Point(215, 286);
            this.labelRestart.Name = "labelRestart";
            this.labelRestart.Size = new System.Drawing.Size(44, 44);
            this.labelRestart.TabIndex = 4;
            this.labelRestart.Click += new System.EventHandler(this.labelRestart_Click);
            // 
            // labelInfoDevelopers
            // 
            this.labelInfoDevelopers.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelInfoDevelopers.BackColor = System.Drawing.Color.Transparent;
            this.labelInfoDevelopers.Image = ((System.Drawing.Image)(resources.GetObject("labelInfoDevelopers.Image")));
            this.labelInfoDevelopers.Location = new System.Drawing.Point(110, 286);
            this.labelInfoDevelopers.Name = "labelInfoDevelopers";
            this.labelInfoDevelopers.Size = new System.Drawing.Size(44, 44);
            this.labelInfoDevelopers.TabIndex = 5;
            this.labelInfoDevelopers.Click += new System.EventHandler(this.labelInfoDevelopers_Click);
            // 
            // labelSite
            // 
            this.labelSite.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelSite.BackColor = System.Drawing.Color.Transparent;
            this.labelSite.Image = global::Stick_Hero.Properties.Resources.im_Site;
            this.labelSite.Location = new System.Drawing.Point(165, 286);
            this.labelSite.Name = "labelSite";
            this.labelSite.Size = new System.Drawing.Size(44, 44);
            this.labelSite.TabIndex = 6;
            this.labelSite.Click += new System.EventHandler(this.labelSite_Click);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(300, 500);
            this.Controls.Add(this.labelSite);
            this.Controls.Add(this.labelInfoDevelopers);
            this.Controls.Add(this.labelRestart);
            this.Controls.Add(this.labelStartScreen);
            this.Controls.Add(this.labelPlay);
            this.Controls.Add(this.labelName);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stick Hero";
            this.Load += new System.EventHandler(this.Game_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Game_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelPlay;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label labelStartScreen;
        private System.Windows.Forms.Label labelRestart;
        private System.Windows.Forms.Label labelInfoDevelopers;
        private System.Windows.Forms.Label labelSite;
    }
}

