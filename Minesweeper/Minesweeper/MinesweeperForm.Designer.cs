namespace Minesweeper
{
    partial class MinesweeperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MinesweeperForm));
            this.labelMinesLeft = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.pictureBoxBoard = new System.Windows.Forms.PictureBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.labelStartGame = new System.Windows.Forms.Label();
            this.labelMainMenu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // labelMinesLeft
            // 
            this.labelMinesLeft.AutoSize = true;
            this.labelMinesLeft.BackColor = System.Drawing.Color.Transparent;
            this.labelMinesLeft.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMinesLeft.Location = new System.Drawing.Point(15, 9);
            this.labelMinesLeft.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelMinesLeft.Name = "labelMinesLeft";
            this.labelMinesLeft.Size = new System.Drawing.Size(21, 23);
            this.labelMinesLeft.TabIndex = 0;
            this.labelMinesLeft.Text = "0";
            // 
            // labelTime
            // 
            this.labelTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelTime.AutoSize = true;
            this.labelTime.BackColor = System.Drawing.Color.Transparent;
            this.labelTime.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTime.Location = new System.Drawing.Point(309, 9);
            this.labelTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(98, 23);
            this.labelTime.TabIndex = 1;
            this.labelTime.Text = "00:00:00";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBoxBoard
            // 
            this.pictureBoxBoard.BackColor = System.Drawing.Color.White;
            this.pictureBoxBoard.Location = new System.Drawing.Point(126, 72);
            this.pictureBoxBoard.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBoxBoard.Name = "pictureBoxBoard";
            this.pictureBoxBoard.Size = new System.Drawing.Size(156, 83);
            this.pictureBoxBoard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxBoard.TabIndex = 3;
            this.pictureBoxBoard.TabStop = false;
            this.pictureBoxBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxBoard_Paint);
            this.pictureBoxBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBoard_MouseClick);
            this.pictureBoxBoard.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBoard_MouseMove);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // labelStartGame
            // 
            this.labelStartGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStartGame.AutoSize = true;
            this.labelStartGame.BackColor = System.Drawing.Color.Transparent;
            this.labelStartGame.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelStartGame.Location = new System.Drawing.Point(15, 217);
            this.labelStartGame.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelStartGame.Name = "labelStartGame";
            this.labelStartGame.Size = new System.Drawing.Size(87, 23);
            this.labelStartGame.TabIndex = 4;
            this.labelStartGame.Text = "Restart";
            this.labelStartGame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelStartGame_MouseClick);
            this.labelStartGame.MouseLeave += new System.EventHandler(this.labelsLower_MouseLeave);
            this.labelStartGame.MouseHover += new System.EventHandler(this.labelsLower_MouseHover);
            // 
            // labelMainMenu
            // 
            this.labelMainMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMainMenu.AutoSize = true;
            this.labelMainMenu.BackColor = System.Drawing.Color.Transparent;
            this.labelMainMenu.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMainMenu.Location = new System.Drawing.Point(298, 217);
            this.labelMainMenu.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.labelMainMenu.Name = "labelMainMenu";
            this.labelMainMenu.Size = new System.Drawing.Size(109, 23);
            this.labelMainMenu.TabIndex = 5;
            this.labelMainMenu.Text = "Main menu";
            this.labelMainMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.labelMainMenu_MouseClick);
            this.labelMainMenu.MouseLeave += new System.EventHandler(this.labelsLower_MouseLeave);
            this.labelMainMenu.MouseHover += new System.EventHandler(this.labelsLower_MouseHover);
            // 
            // MinesweeperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(422, 249);
            this.Controls.Add(this.labelMainMenu);
            this.Controls.Add(this.labelStartGame);
            this.Controls.Add(this.pictureBoxBoard);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelMinesLeft);
            this.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MinesweeperForm";
            this.Text = "Minesweeper";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MinesweeperForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelMinesLeft;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.PictureBox pictureBoxBoard;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label labelStartGame;
        private System.Windows.Forms.Label labelMainMenu;
    }
}

