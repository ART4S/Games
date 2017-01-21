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
            this.labelMode = new System.Windows.Forms.ToolStripMenuItem();
            this.labelEazyCrazy = new System.Windows.Forms.ToolStripMenuItem();
            this.labelEazy = new System.Windows.Forms.ToolStripMenuItem();
            this.labelNormal = new System.Windows.Forms.ToolStripMenuItem();
            this.labelHard = new System.Windows.Forms.ToolStripMenuItem();
            this.labelRules = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.labelAboutGame = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCurrentMode = new System.Windows.Forms.Label();
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
            this.labelVisitedСells.Size = new System.Drawing.Size(114, 16);
            this.labelVisitedСells.TabIndex = 1;
            this.labelVisitedСells.Text = "Клеток посещено: ";
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
            this.labelMode,
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
            this.labelNewGame.Size = new System.Drawing.Size(152, 22);
            this.labelNewGame.Text = "Новая игра";
            this.labelNewGame.Click += new System.EventHandler(this.labelNewGame_Click);
            // 
            // labelMode
            // 
            this.labelMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelEazyCrazy,
            this.labelEazy,
            this.labelNormal,
            this.labelHard});
            this.labelMode.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.labelMode.Name = "labelMode";
            this.labelMode.Size = new System.Drawing.Size(152, 22);
            this.labelMode.Text = "Режим";
            // 
            // labelEazyCrazy
            // 
            this.labelEazyCrazy.Name = "labelEazyCrazy";
            this.labelEazyCrazy.Size = new System.Drawing.Size(152, 22);
            this.labelEazyCrazy.Text = "Очень легко";
            this.labelEazyCrazy.Click += new System.EventHandler(this.labelEazyCrazy_Click);
            // 
            // labelEazy
            // 
            this.labelEazy.Name = "labelEazy";
            this.labelEazy.Size = new System.Drawing.Size(152, 22);
            this.labelEazy.Text = "Легко";
            this.labelEazy.Click += new System.EventHandler(this.labelEazy_Click);
            // 
            // labelNormal
            // 
            this.labelNormal.Name = "labelNormal";
            this.labelNormal.Size = new System.Drawing.Size(152, 22);
            this.labelNormal.Text = "Нормально";
            this.labelNormal.Click += new System.EventHandler(this.labelNormal_Click);
            // 
            // labelHard
            // 
            this.labelHard.Name = "labelHard";
            this.labelHard.Size = new System.Drawing.Size(152, 22);
            this.labelHard.Text = "Сложно";
            this.labelHard.Click += new System.EventHandler(this.labelHard_Click);
            // 
            // labelRules
            // 
            this.labelRules.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.labelRules.Name = "labelRules";
            this.labelRules.Size = new System.Drawing.Size(152, 22);
            this.labelRules.Text = "Правила";
            this.labelRules.Click += new System.EventHandler(this.labelRules_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // labelAboutGame
            // 
            this.labelAboutGame.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.labelAboutGame.Name = "labelAboutGame";
            this.labelAboutGame.Size = new System.Drawing.Size(152, 22);
            this.labelAboutGame.Text = "Об игре";
            this.labelAboutGame.Click += new System.EventHandler(this.labelAboutGame_Click);
            // 
            // labelCurrentMode
            // 
            this.labelCurrentMode.AutoSize = true;
            this.labelCurrentMode.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelCurrentMode.Location = new System.Drawing.Point(370, 55);
            this.labelCurrentMode.Name = "labelCurrentMode";
            this.labelCurrentMode.Size = new System.Drawing.Size(106, 16);
            this.labelCurrentMode.TabIndex = 4;
            this.labelCurrentMode.Text = "Текущий режим: ";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(548, 397);
            this.Controls.Add(this.labelCurrentMode);
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
        private System.Windows.Forms.ToolStripMenuItem labelMode;
        private System.Windows.Forms.ToolStripMenuItem labelRules;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem labelAboutGame;
        private System.Windows.Forms.ToolStripMenuItem labelEazyCrazy;
        private System.Windows.Forms.ToolStripMenuItem labelEazy;
        private System.Windows.Forms.ToolStripMenuItem labelNormal;
        private System.Windows.Forms.ToolStripMenuItem labelHard;
        private System.Windows.Forms.Label labelCurrentMode;
    }
}

