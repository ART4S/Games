namespace SnakeForm
{
    partial class Snake
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

        #region
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Snake));
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.labelScore = new System.Windows.Forms.Label();
            this.labelLength = new System.Windows.Forms.Label();
            this.pictureBoxBoard = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            // 
            // labelScore
            // 
            this.labelScore.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelScore.AutoSize = true;
            this.labelScore.BackColor = System.Drawing.Color.Transparent;
            this.labelScore.Font = new System.Drawing.Font("Segoe Script", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelScore.ForeColor = System.Drawing.Color.Black;
            this.labelScore.Location = new System.Drawing.Point(12, 9);
            this.labelScore.Name = "labelScore";
            this.labelScore.Size = new System.Drawing.Size(94, 32);
            this.labelScore.TabIndex = 0;
            this.labelScore.Text = "score: 0";
            // 
            // labelLength
            // 
            this.labelLength.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLength.AutoSize = true;
            this.labelLength.Font = new System.Drawing.Font("Segoe Script", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelLength.Location = new System.Drawing.Point(12, 51);
            this.labelLength.Name = "labelLength";
            this.labelLength.Size = new System.Drawing.Size(111, 32);
            this.labelLength.TabIndex = 3;
            this.labelLength.Text = "length: 2";
            // 
            // pictureBoxBoard
            // 
            this.pictureBoxBoard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBoxBoard.BackgroundImage = global::SnakeForm.Properties.Resources.grass;
            this.pictureBoxBoard.Location = new System.Drawing.Point(12, 104);
            this.pictureBoxBoard.Name = "pictureBoxBoard";
            this.pictureBoxBoard.Size = new System.Drawing.Size(400, 400);
            this.pictureBoxBoard.TabIndex = 4;
            this.pictureBoxBoard.TabStop = false;
            // 
            // Snake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Yellow;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(422, 516);
            this.Controls.Add(this.pictureBoxBoard);
            this.Controls.Add(this.labelLength);
            this.Controls.Add(this.labelScore);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Snake";
            this.Text = "Snake";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PressKey);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.Label labelScore;
        private System.Windows.Forms.Label labelLength;
        private System.Windows.Forms.PictureBox pictureBoxBoard;
    }
}

