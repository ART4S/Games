﻿namespace AirForce
{
    partial class MainForm
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
            this.GameFieldPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.GameFieldPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // GameFieldPictureBox
            // 
            this.GameFieldPictureBox.BackColor = System.Drawing.Color.Black;
            this.GameFieldPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GameFieldPictureBox.Location = new System.Drawing.Point(0, 0);
            this.GameFieldPictureBox.Name = "GameFieldPictureBox";
            this.GameFieldPictureBox.Size = new System.Drawing.Size(1362, 700);
            this.GameFieldPictureBox.TabIndex = 0;
            this.GameFieldPictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 700);
            this.Controls.Add(this.GameFieldPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "Air force";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.GameFieldPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox GameFieldPictureBox;
    }
}

