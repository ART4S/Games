namespace Labyrinth
{
    partial class GameForm
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
            this.pictureBoxGameField = new System.Windows.Forms.PictureBox();
            this.labelVisitedСells = new System.Windows.Forms.Label();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labelNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.labelSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.labelRules = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.labelAboutGame = new System.Windows.Forms.ToolStripMenuItem();
            this.labelBestScore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameField)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxGameField
            // 
            this.pictureBoxGameField.Location = new System.Drawing.Point(31, 29);
            this.pictureBoxGameField.Name = "pictureBoxGameField";
            this.pictureBoxGameField.Size = new System.Drawing.Size(333, 313);
            this.pictureBoxGameField.TabIndex = 0;
            this.pictureBoxGameField.TabStop = false;
            this.pictureBoxGameField.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGameField_Paint);
            // 
            // labelVisitedСells
            // 
            this.labelVisitedСells.AutoSize = true;
            this.labelVisitedСells.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelVisitedСells.Location = new System.Drawing.Point(370, 29);
            this.labelVisitedСells.Name = "labelVisitedСells";
            this.labelVisitedСells.Size = new System.Drawing.Size(129, 16);
            this.labelVisitedСells.TabIndex = 1;
            this.labelVisitedСells.Text = "Посещённые клетки: ";
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.Gainsboro;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(548, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelNewGame,
            this.labelSettings,
            this.labelRules,
            this.toolStripSeparator1,
            this.labelAboutGame});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.menuToolStripMenuItem.Text = "Меню";
            // 
            // labelNewGame
            // 
            this.labelNewGame.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.labelNewGame.Name = "labelNewGame";
            this.labelNewGame.Size = new System.Drawing.Size(160, 22);
            this.labelNewGame.Text = "Новая игра     F1";
            // 
            // labelSettings
            // 
            this.labelSettings.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.labelSettings.Name = "labelSettings";
            this.labelSettings.Size = new System.Drawing.Size(160, 22);
            this.labelSettings.Text = "Настройки";
            // 
            // labelRules
            // 
            this.labelRules.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.labelRules.Name = "labelRules";
            this.labelRules.Size = new System.Drawing.Size(160, 22);
            this.labelRules.Text = "Правила";
            this.labelRules.Click += new System.EventHandler(this.labelRules_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // labelAboutGame
            // 
            this.labelAboutGame.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.labelAboutGame.Name = "labelAboutGame";
            this.labelAboutGame.Size = new System.Drawing.Size(160, 22);
            this.labelAboutGame.Text = "Об игре";
            this.labelAboutGame.Click += new System.EventHandler(this.labelAboutGame_Click);
            // 
            // labelBestScore
            // 
            this.labelBestScore.AutoSize = true;
            this.labelBestScore.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelBestScore.Location = new System.Drawing.Point(370, 55);
            this.labelBestScore.Name = "labelBestScore";
            this.labelBestScore.Size = new System.Drawing.Size(116, 16);
            this.labelBestScore.TabIndex = 3;
            this.labelBestScore.Text = "Лучший результат: ";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(548, 397);
            this.Controls.Add(this.labelBestScore);
            this.Controls.Add(this.labelVisitedСells);
            this.Controls.Add(this.pictureBoxGameField);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "GameForm";
            this.Text = "Лабиринт минотавра";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameField)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGameField;
        private System.Windows.Forms.Label labelVisitedСells;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labelNewGame;
        private System.Windows.Forms.ToolStripMenuItem labelSettings;
        private System.Windows.Forms.ToolStripMenuItem labelRules;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem labelAboutGame;
        private System.Windows.Forms.Label labelBestScore;
    }
}

