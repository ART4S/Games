namespace Breaking_locks
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
            this.lbl_Passkeys = new System.Windows.Forms.Label();
            this.lbl_Strength = new System.Windows.Forms.Label();
            this.lbl_rules = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pB_Game = new System.Windows.Forms.PictureBox();
            this.lbl_score = new System.Windows.Forms.Label();
            this.lbl_cheat = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pB_Game)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Passkeys
            // 
            this.lbl_Passkeys.AutoSize = true;
            this.lbl_Passkeys.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_Passkeys.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbl_Passkeys.Location = new System.Drawing.Point(346, 107);
            this.lbl_Passkeys.Name = "lbl_Passkeys";
            this.lbl_Passkeys.Size = new System.Drawing.Size(67, 13);
            this.lbl_Passkeys.TabIndex = 0;
            this.lbl_Passkeys.Text = "Passkey: 0";
            // 
            // lbl_Strength
            // 
            this.lbl_Strength.AutoSize = true;
            this.lbl_Strength.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_Strength.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbl_Strength.Location = new System.Drawing.Point(346, 130);
            this.lbl_Strength.Name = "lbl_Strength";
            this.lbl_Strength.Size = new System.Drawing.Size(70, 13);
            this.lbl_Strength.TabIndex = 1;
            this.lbl_Strength.Text = "Strength: 0";
            // 
            // lbl_rules
            // 
            this.lbl_rules.AutoSize = true;
            this.lbl_rules.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_rules.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbl_rules.Location = new System.Drawing.Point(346, 12);
            this.lbl_rules.Name = "lbl_rules";
            this.lbl_rules.Size = new System.Drawing.Size(244, 39);
            this.lbl_rules.TabIndex = 2;
            this.lbl_rules.Text = "Rules:\r\nУправляйте отмычкой с помощью мыши\r\nSpace - взломать замок";
            // 
            // timer
            // 
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer_lowStick_Move);
            // 
            // pB_Game
            // 
            this.pB_Game.BackColor = System.Drawing.Color.White;
            this.pB_Game.Location = new System.Drawing.Point(12, 12);
            this.pB_Game.Name = "pB_Game";
            this.pB_Game.Size = new System.Drawing.Size(280, 280);
            this.pB_Game.TabIndex = 3;
            this.pB_Game.TabStop = false;
            this.pB_Game.Paint += new System.Windows.Forms.PaintEventHandler(this.pB_Game_Paint);
            this.pB_Game.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pB_Game_MouseMove);
            // 
            // lbl_score
            // 
            this.lbl_score.AutoSize = true;
            this.lbl_score.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lbl_score.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lbl_score.Location = new System.Drawing.Point(346, 85);
            this.lbl_score.Name = "lbl_score";
            this.lbl_score.Size = new System.Drawing.Size(52, 13);
            this.lbl_score.TabIndex = 4;
            this.lbl_score.Text = "Score: 0";
            // 
            // lbl_cheat
            // 
            this.lbl_cheat.AutoSize = true;
            this.lbl_cheat.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cheat.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbl_cheat.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbl_cheat.Location = new System.Drawing.Point(114, 308);
            this.lbl_cheat.Name = "lbl_cheat";
            this.lbl_cheat.Size = new System.Drawing.Size(64, 24);
            this.lbl_cheat.TabIndex = 5;
            this.lbl_cheat.Text = "Cheat";
            this.lbl_cheat.Click += new System.EventHandler(this.lbl_cheat_Click);
            this.lbl_cheat.MouseLeave += new System.EventHandler(this.lbl_cheat_MouseLeave);
            this.lbl_cheat.MouseHover += new System.EventHandler(this.lbl_cheat_MouseHover);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(630, 350);
            this.Controls.Add(this.lbl_cheat);
            this.Controls.Add(this.lbl_score);
            this.Controls.Add(this.pB_Game);
            this.Controls.Add(this.lbl_rules);
            this.Controls.Add(this.lbl_Strength);
            this.Controls.Add(this.lbl_Passkeys);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Breaking locks";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pB_Game)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Passkeys;
        private System.Windows.Forms.Label lbl_Strength;
        private System.Windows.Forms.Label lbl_rules;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.PictureBox pB_Game;
        private System.Windows.Forms.Label lbl_score;
        private System.Windows.Forms.Label lbl_cheat;
    }
}

