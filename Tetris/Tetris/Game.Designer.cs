namespace Tetris
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
            this.labelFiguresAmount = new System.Windows.Forms.Label();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelLevel = new System.Windows.Forms.Label();
            this.menuBar = new System.Windows.Forms.MenuStrip();
            this.menu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemNewGame = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTransparency = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.pB_NextFigure = new System.Windows.Forms.PictureBox();
            this.pB_Board = new System.Windows.Forms.PictureBox();
            this.labelBestScore = new System.Windows.Forms.Label();
            this.menuBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_NextFigure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_Board)).BeginInit();
            this.SuspendLayout();
            // 
            // labelFiguresAmount
            // 
            this.labelFiguresAmount.AutoSize = true;
            this.labelFiguresAmount.BackColor = System.Drawing.Color.Transparent;
            this.labelFiguresAmount.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFiguresAmount.Location = new System.Drawing.Point(240, 210);
            this.labelFiguresAmount.Name = "labelFiguresAmount";
            this.labelFiguresAmount.Size = new System.Drawing.Size(57, 16);
            this.labelFiguresAmount.TabIndex = 2;
            this.labelFiguresAmount.Text = "Bricks: 0";
            // 
            // labelScore
            // 
            this.labelScore.AutoSize = true;
            this.labelScore.BackColor = System.Drawing.Color.Transparent;
            this.labelScore.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelScore.Location = new System.Drawing.Point(239, 232);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(56, 16);
            this.labelScore.TabIndex = 3;
            this.labelScore.Text = "Score: 0";
            // 
            // labelLevel
            // 
            this.labelLevel.AutoSize = true;
            this.labelLevel.BackColor = System.Drawing.Color.Transparent;
            this.labelLevel.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLevel.Location = new System.Drawing.Point(239, 256);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(51, 16);
            this.labelLevel.TabIndex = 4;
            this.labelLevel.Text = "Level: 1";
            // 
            // menuBar
            // 
            this.menuBar.BackColor = System.Drawing.Color.Transparent;
            this.menuBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu});
            this.menuBar.Location = new System.Drawing.Point(0, 0);
            this.menuBar.Name = "menuBar";
            this.menuBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.menuBar.Size = new System.Drawing.Size(468, 24);
            this.menuBar.TabIndex = 5;
            this.menuBar.Text = "menuStrip1";
            // 
            // menu
            // 
            this.menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemNewGame,
            this.menuItemTransparency,
            this.menuItemHelp,
            this.menuItemInfo,
            this.menuToolStripSeparator,
            this.menuItemExit});
            this.menu.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(47, 20);
            this.menu.Text = "Menu";
            // 
            // menuItemNewGame
            // 
            this.menuItemNewGame.Name = "menuItemNewGame";
            this.menuItemNewGame.Size = new System.Drawing.Size(170, 22);
            this.menuItemNewGame.Text = "New game          F1";
            this.menuItemNewGame.Click += new System.EventHandler(this.menuItemNewGame_Click);
            // 
            // menuItemTransparency
            // 
            this.menuItemTransparency.Name = "menuItemTransparency";
            this.menuItemTransparency.Size = new System.Drawing.Size(170, 22);
            this.menuItemTransparency.Text = "Grid";
            this.menuItemTransparency.Click += new System.EventHandler(this.menuItemTransparency_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(170, 22);
            this.menuItemHelp.Text = "Rules";
            this.menuItemHelp.Click += new System.EventHandler(this.menuItemHelp_Click);
            // 
            // menuItemInfo
            // 
            this.menuItemInfo.Name = "menuItemInfo";
            this.menuItemInfo.Size = new System.Drawing.Size(170, 22);
            this.menuItemInfo.Text = "About game        F2";
            this.menuItemInfo.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.menuItemInfo.Click += new System.EventHandler(this.menuItemInfo_Click);
            // 
            // menuToolStripSeparator
            // 
            this.menuToolStripSeparator.Name = "menuToolStripSeparator";
            this.menuToolStripSeparator.Size = new System.Drawing.Size(167, 6);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.Size = new System.Drawing.Size(170, 22);
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // timer
            // 
            this.timer.Interval = 800;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // pB_NextFigure
            // 
            this.pB_NextFigure.BackColor = System.Drawing.Color.Transparent;
            this.pB_NextFigure.Location = new System.Drawing.Point(201, 42);
            this.pB_NextFigure.Name = "pB_NextFigure";
            this.pB_NextFigure.Size = new System.Drawing.Size(165, 148);
            this.pB_NextFigure.TabIndex = 1;
            this.pB_NextFigure.TabStop = false;
            this.pB_NextFigure.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxNextFigure_Paint);
            // 
            // pB_Board
            // 
            this.pB_Board.BackColor = System.Drawing.Color.Transparent;
            this.pB_Board.Location = new System.Drawing.Point(0, 27);
            this.pB_Board.Name = "pB_Board";
            this.pB_Board.Size = new System.Drawing.Size(183, 163);
            this.pB_Board.TabIndex = 0;
            this.pB_Board.TabStop = false;
            this.pB_Board.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxBoard_Paint);
            // 
            // labelBestScore
            // 
            this.labelBestScore.AutoSize = true;
            this.labelBestScore.BackColor = System.Drawing.Color.Transparent;
            this.labelBestScore.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBestScore.Location = new System.Drawing.Point(240, 308);
            this.labelBestScore.Name = "labelBestScore";
            this.labelBestScore.Size = new System.Drawing.Size(55, 16);
            this.labelBestScore.TabIndex = 6;
            this.labelBestScore.Text = "BEST: 0";
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(468, 492);
            this.Controls.Add(this.labelBestScore);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.labelScore);
            this.Controls.Add(this.labelFiguresAmount);
            this.Controls.Add(this.pB_NextFigure);
            this.Controls.Add(this.pB_Board);
            this.Controls.Add(this.menuBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuBar;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Game";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tetris";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pressKey_Down);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pressKey_Up);
            this.menuBar.ResumeLayout(false);
            this.menuBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pB_NextFigure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pB_Board)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pB_Board;
        private System.Windows.Forms.PictureBox pB_NextFigure;
        private System.Windows.Forms.Label labelFiguresAmount;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.MenuStrip menuBar;
        private System.Windows.Forms.ToolStripMenuItem menu;
        private System.Windows.Forms.ToolStripMenuItem menuItemNewGame;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemInfo;
        private System.Windows.Forms.ToolStripSeparator menuToolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem menuItemTransparency;
        private System.Windows.Forms.Label labelBestScore;
    }
}

