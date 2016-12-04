namespace Ball_Breaker
{
    partial class Game
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.pB_Game = new System.Windows.Forms.PictureBox();
            this.lbl_counterScore = new System.Windows.Forms.Label();
            this.lbl_score = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.lbl_menu = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_newGame = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_rules = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lbl_aboutGame = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_return = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_score_best = new System.Windows.Forms.Label();
            this.lbl_mode = new System.Windows.Forms.Label();
            this.lbl_delay = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pB_Game)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pB_Game
            // 
            this.pB_Game.Location = new System.Drawing.Point(14, 31);
            this.pB_Game.Name = "pB_Game";
            this.pB_Game.Size = new System.Drawing.Size(266, 265);
            this.pB_Game.TabIndex = 0;
            this.pB_Game.TabStop = false;
            this.pB_Game.Click += new System.EventHandler(this.pB_Game_Click);
            this.pB_Game.Paint += new System.Windows.Forms.PaintEventHandler(this.pB_Game_Paint);
            this.pB_Game.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pB_Game_MouseMove);
            // 
            // lbl_counterScore
            // 
            this.lbl_counterScore.BackColor = System.Drawing.Color.Aqua;
            this.lbl_counterScore.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.lbl_counterScore.Location = new System.Drawing.Point(328, 166);
            this.lbl_counterScore.Name = "lbl_counterScore";
            this.lbl_counterScore.Size = new System.Drawing.Size(38, 30);
            this.lbl_counterScore.TabIndex = 2;
            this.lbl_counterScore.Text = "lbl_counterScore";
            this.lbl_counterScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_counterScore.Click += new System.EventHandler(this.lbl_counterScore_Click);
            // 
            // lbl_score
            // 
            this.lbl_score.AutoSize = true;
            this.lbl_score.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_score.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbl_score.Location = new System.Drawing.Point(285, 31);
            this.lbl_score.Name = "lbl_score";
            this.lbl_score.Size = new System.Drawing.Size(56, 16);
            this.lbl_score.TabIndex = 4;
            this.lbl_score.Text = "lbl_score";
            this.lbl_score.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_menu,
            this.lbl_return});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(443, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // lbl_menu
            // 
            this.lbl_menu.BackColor = System.Drawing.Color.Transparent;
            this.lbl_menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_newGame,
            this.lbl_settings,
            this.lbl_rules,
            this.toolStripSeparator1,
            this.lbl_aboutGame});
            this.lbl_menu.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.lbl_menu.Name = "lbl_menu";
            this.lbl_menu.Size = new System.Drawing.Size(47, 20);
            this.lbl_menu.Text = "Menu";
            // 
            // lbl_newGame
            // 
            this.lbl_newGame.Name = "lbl_newGame";
            this.lbl_newGame.Size = new System.Drawing.Size(164, 22);
            this.lbl_newGame.Text = "New Game        F1";
            this.lbl_newGame.Click += new System.EventHandler(this.lbl_newGame_Click);
            // 
            // lbl_settings
            // 
            this.lbl_settings.Name = "lbl_settings";
            this.lbl_settings.Size = new System.Drawing.Size(164, 22);
            this.lbl_settings.Text = "Settings";
            this.lbl_settings.Click += new System.EventHandler(this.lbl_settings_Click);
            // 
            // lbl_rules
            // 
            this.lbl_rules.Name = "lbl_rules";
            this.lbl_rules.Size = new System.Drawing.Size(164, 22);
            this.lbl_rules.Text = "Rules";
            this.lbl_rules.Click += new System.EventHandler(this.lbl_rules_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(161, 6);
            // 
            // lbl_aboutGame
            // 
            this.lbl_aboutGame.Name = "lbl_aboutGame";
            this.lbl_aboutGame.Size = new System.Drawing.Size(163, 22);
            this.lbl_aboutGame.Text = "About game";
            this.lbl_aboutGame.Click += new System.EventHandler(this.lbl_aboutGame_Click);
            // 
            // lbl_return
            // 
            this.lbl_return.BackColor = System.Drawing.Color.Transparent;
            this.lbl_return.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.lbl_return.Name = "lbl_return";
            this.lbl_return.Size = new System.Drawing.Size(53, 20);
            this.lbl_return.Text = "Cancel";
            this.lbl_return.Click += new System.EventHandler(this.lbl_return_Click);
            // 
            // lbl_score_best
            // 
            this.lbl_score_best.AutoSize = true;
            this.lbl_score_best.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_score_best.Location = new System.Drawing.Point(286, 57);
            this.lbl_score_best.Name = "lbl_score_best";
            this.lbl_score_best.Size = new System.Drawing.Size(86, 16);
            this.lbl_score_best.TabIndex = 6;
            this.lbl_score_best.Text = "lbl_score_best";
            // 
            // lbl_mode
            // 
            this.lbl_mode.AutoSize = true;
            this.lbl_mode.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_mode.Location = new System.Drawing.Point(285, 92);
            this.lbl_mode.Name = "lbl_mode";
            this.lbl_mode.Size = new System.Drawing.Size(55, 16);
            this.lbl_mode.TabIndex = 7;
            this.lbl_mode.Text = "lbl_mode";
            // 
            // lbl_delay
            // 
            this.lbl_delay.AutoSize = true;
            this.lbl_delay.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_delay.Location = new System.Drawing.Point(286, 108);
            this.lbl_delay.Name = "lbl_delay";
            this.lbl_delay.Size = new System.Drawing.Size(55, 16);
            this.lbl_delay.TabIndex = 8;
            this.lbl_delay.Text = "lbl_delay";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(443, 306);
            this.Controls.Add(this.lbl_delay);
            this.Controls.Add(this.lbl_mode);
            this.Controls.Add(this.lbl_score_best);
            this.Controls.Add(this.lbl_score);
            this.Controls.Add(this.lbl_counterScore);
            this.Controls.Add(this.pB_Game);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Game";
            this.Text = "Ball Breaker";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pB_Game)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pB_Game;
        private System.Windows.Forms.Label lbl_counterScore;
        private System.Windows.Forms.Label lbl_score;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem lbl_menu;
        private System.Windows.Forms.ToolStripMenuItem lbl_newGame;
        private System.Windows.Forms.ToolStripMenuItem lbl_rules;
        private System.Windows.Forms.ToolStripMenuItem lbl_aboutGame;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem lbl_return;
        private System.Windows.Forms.Label lbl_score_best;
        private System.Windows.Forms.Label lbl_mode;
        private System.Windows.Forms.ToolStripMenuItem lbl_settings;
        private System.Windows.Forms.Label lbl_delay;
    }
}

