namespace Ball_Breaker
{
    partial class Form_settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_ok = new System.Windows.Forms.Button();
            this.tB_Delay = new System.Windows.Forms.TrackBar();
            this.lbl_delay = new System.Windows.Forms.Label();
            this.lbl_mode = new System.Windows.Forms.Label();
            this.cB_mode = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.tB_Delay)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.Transparent;
            this.btn_ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_ok.ForeColor = System.Drawing.Color.Black;
            this.btn_ok.Location = new System.Drawing.Point(286, 205);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(87, 27);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // tB_Delay
            // 
            this.tB_Delay.LargeChange = 100;
            this.tB_Delay.Location = new System.Drawing.Point(17, 128);
            this.tB_Delay.Maximum = 1000;
            this.tB_Delay.Name = "tB_Delay";
            this.tB_Delay.Size = new System.Drawing.Size(353, 45);
            this.tB_Delay.SmallChange = 100;
            this.tB_Delay.TabIndex = 100;
            this.tB_Delay.TickFrequency = 100;
            // 
            // lbl_delay
            // 
            this.lbl_delay.AutoSize = true;
            this.lbl_delay.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.lbl_delay.Location = new System.Drawing.Point(13, 95);
            this.lbl_delay.Name = "lbl_delay";
            this.lbl_delay.Size = new System.Drawing.Size(39, 15);
            this.lbl_delay.TabIndex = 3;
            this.lbl_delay.Text = "Delay:";
            // 
            // lbl_mode
            // 
            this.lbl_mode.AutoSize = true;
            this.lbl_mode.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.lbl_mode.Location = new System.Drawing.Point(14, 10);
            this.lbl_mode.Name = "lbl_mode";
            this.lbl_mode.Size = new System.Drawing.Size(38, 15);
            this.lbl_mode.TabIndex = 4;
            this.lbl_mode.Text = "Mode:";
            // 
            // cB_mode
            // 
            this.cB_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_mode.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cB_mode.FormattingEnabled = true;
            this.cB_mode.Location = new System.Drawing.Point(17, 43);
            this.cB_mode.Name = "cB_mode";
            this.cB_mode.Size = new System.Drawing.Size(140, 23);
            this.cB_mode.TabIndex = 101;
            // 
            // Form_settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.ClientSize = new System.Drawing.Size(385, 244);
            this.ControlBox = false;
            this.Controls.Add(this.cB_mode);
            this.Controls.Add(this.lbl_mode);
            this.Controls.Add(this.lbl_delay);
            this.Controls.Add(this.tB_Delay);
            this.Controls.Add(this.btn_ok);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_settings";
            this.Text = "Settings";
            ((System.ComponentModel.ISupportInitialize)(this.tB_Delay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.TrackBar tB_Delay;
        private System.Windows.Forms.Label lbl_delay;
        private System.Windows.Forms.Label lbl_mode;
        private System.Windows.Forms.ComboBox cB_mode;
    }
}