namespace Paint
{
    partial class ImageViewerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewerForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nearestNeighborMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bilinearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bicubicMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.firstPictureBox = new System.Windows.Forms.PictureBox();
            this.secondPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.firstPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.White;
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.helpMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(576, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newMenuItem,
            this.openMenuItem,
            this.toolStripSeparator1,
            this.exitMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newMenuItem
            // 
            this.newMenuItem.Name = "newMenuItem";
            this.newMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newMenuItem.Text = "New";
            // 
            // openMenuItem
            // 
            this.openMenuItem.Name = "openMenuItem";
            this.openMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openMenuItem.Text = "Open";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitMenuItem.Text = "Exit";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nearestNeighborMenuItem,
            this.bilinearMenuItem,
            this.bicubicMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // nearestNeighborMenuItem
            // 
            this.nearestNeighborMenuItem.Name = "nearestNeighborMenuItem";
            this.nearestNeighborMenuItem.Size = new System.Drawing.Size(165, 22);
            this.nearestNeighborMenuItem.Text = "Nearest neighbor";
            // 
            // bilinearMenuItem
            // 
            this.bilinearMenuItem.Name = "bilinearMenuItem";
            this.bilinearMenuItem.Size = new System.Drawing.Size(165, 22);
            this.bilinearMenuItem.Text = "Bilinear";
            // 
            // bicubicMenuItem
            // 
            this.bicubicMenuItem.Name = "bicubicMenuItem";
            this.bicubicMenuItem.Size = new System.Drawing.Size(165, 22);
            this.bicubicMenuItem.Text = "Bicubic";
            // 
            // helpMenuItem
            // 
            this.helpMenuItem.Name = "helpMenuItem";
            this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpMenuItem.Text = "Help";
            // 
            // splitContainer
            // 
            this.splitContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 24);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.AutoScroll = true;
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.DimGray;
            this.splitContainer.Panel1.Controls.Add(this.firstPictureBox);
            this.splitContainer.Panel1MinSize = 0;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.AutoScroll = true;
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.DimGray;
            this.splitContainer.Panel2.Controls.Add(this.secondPictureBox);
            this.splitContainer.Panel2MinSize = 0;
            this.splitContainer.Size = new System.Drawing.Size(576, 361);
            this.splitContainer.SplitterDistance = 25;
            this.splitContainer.SplitterWidth = 5;
            this.splitContainer.TabIndex = 2;
            // 
            // firstPictureBox
            // 
            this.firstPictureBox.Location = new System.Drawing.Point(3, 3);
            this.firstPictureBox.Name = "firstPictureBox";
            this.firstPictureBox.Size = new System.Drawing.Size(0, 0);
            this.firstPictureBox.TabIndex = 0;
            this.firstPictureBox.TabStop = false;
            // 
            // secondPictureBox
            // 
            this.secondPictureBox.Location = new System.Drawing.Point(3, 3);
            this.secondPictureBox.Name = "secondPictureBox";
            this.secondPictureBox.Size = new System.Drawing.Size(0, 0);
            this.secondPictureBox.TabIndex = 0;
            this.secondPictureBox.TabStop = false;
            // 
            // ImageViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 385);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ImageViewerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Image Viewer";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImageViewerForm_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.firstPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ToolStripMenuItem nearestNeighborMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bilinearMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bicubicMenuItem;
        private System.Windows.Forms.PictureBox firstPictureBox;
        private System.Windows.Forms.PictureBox secondPictureBox;
        private System.Windows.Forms.ToolStripMenuItem helpMenuItem;
    }
}