namespace Labyrinth
{
    partial class Form_Game
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
            this.pB_Labyrinth = new System.Windows.Forms.PictureBox();
            this.lbl_status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pB_Labyrinth)).BeginInit();
            this.SuspendLayout();
            // 
            // pB_Labyrinth
            // 
            this.pB_Labyrinth.Location = new System.Drawing.Point(31, 29);
            this.pB_Labyrinth.Name = "pB_Labyrinth";
            this.pB_Labyrinth.Size = new System.Drawing.Size(333, 313);
            this.pB_Labyrinth.TabIndex = 0;
            this.pB_Labyrinth.TabStop = false;
            this.pB_Labyrinth.Paint += new System.Windows.Forms.PaintEventHandler(this.pB_Labyrinth_Paint);
            // 
            // lbl_status
            // 
            this.lbl_status.AutoSize = true;
            this.lbl_status.Location = new System.Drawing.Point(408, 46);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(35, 13);
            this.lbl_status.TabIndex = 1;
            this.lbl_status.Text = "label1";
            // 
            // Form_Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(464, 354);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.pB_Labyrinth);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form_Game";
            this.Text = "Labyrinth";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pB_Labyrinth)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pB_Labyrinth;
        private System.Windows.Forms.Label lbl_status;
    }
}

