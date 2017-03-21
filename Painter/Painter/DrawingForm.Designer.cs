namespace Painter
{
    partial class drawingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(drawingForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsFIleMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pagePropertyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolbarMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutPainterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.newFileButton = new System.Windows.Forms.ToolStripButton();
            this.openFileButton = new System.Windows.Forms.ToolStripButton();
            this.saveFileButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.drawCircleButton = new System.Windows.Forms.ToolStripButton();
            this.drawRectangleButton = new System.Windows.Forms.ToolStripButton();
            this.drawPolygonButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.helpButton = new System.Windows.Forms.ToolStripButton();
            this.patternsList = new System.Windows.Forms.ImageList(this.components);
            this.patternsListView = new System.Windows.Forms.ListView();
            this.drawingPictureBox = new System.Windows.Forms.PictureBox();
            this.drawingPanel = new System.Windows.Forms.Panel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.fToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingPictureBox)).BeginInit();
            this.drawingPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editMenuItem,
            this.viewMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(517, 24);
            this.menuStrip.TabIndex = 0;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileMenuItem,
            this.openFileMenuItem,
            this.saveFileMenuItem,
            this.saveAsFIleMenuItem,
            this.toolStripSeparator1,
            this.pagePropertyMenuItem,
            this.toolStripSeparator2,
            this.exitMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newFileMenuItem
            // 
            this.newFileMenuItem.Name = "newFileMenuItem";
            this.newFileMenuItem.Size = new System.Drawing.Size(157, 22);
            this.newFileMenuItem.Text = "New";
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.Size = new System.Drawing.Size(157, 22);
            this.openFileMenuItem.Text = "Open";
            // 
            // saveFileMenuItem
            // 
            this.saveFileMenuItem.Name = "saveFileMenuItem";
            this.saveFileMenuItem.Size = new System.Drawing.Size(157, 22);
            this.saveFileMenuItem.Text = "Save";
            // 
            // saveAsFIleMenuItem
            // 
            this.saveAsFIleMenuItem.Name = "saveAsFIleMenuItem";
            this.saveAsFIleMenuItem.Size = new System.Drawing.Size(157, 22);
            this.saveAsFIleMenuItem.Text = "Save As...";
            this.saveAsFIleMenuItem.Click += new System.EventHandler(this.saveAsFIleMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(154, 6);
            // 
            // pagePropertyMenuItem
            // 
            this.pagePropertyMenuItem.Name = "pagePropertyMenuItem";
            this.pagePropertyMenuItem.Size = new System.Drawing.Size(157, 22);
            this.pagePropertyMenuItem.Text = "Page Property...";
            this.pagePropertyMenuItem.Click += new System.EventHandler(this.pagePropertyMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(154, 6);
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(157, 22);
            this.exitMenuItem.Text = "Exit";
            this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
            // 
            // editMenuItem
            // 
            this.editMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem,
            this.clearMenuItem});
            this.editMenuItem.Name = "editMenuItem";
            this.editMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editMenuItem.Text = "Edit";
            // 
            // undoMenuItem
            // 
            this.undoMenuItem.Name = "undoMenuItem";
            this.undoMenuItem.Size = new System.Drawing.Size(167, 22);
            this.undoMenuItem.Text = "Undo          Ctrl+Z";
            this.undoMenuItem.Click += new System.EventHandler(this.undoMenuItem_Click);
            // 
            // clearMenuItem
            // 
            this.clearMenuItem.Name = "clearMenuItem";
            this.clearMenuItem.Size = new System.Drawing.Size(167, 22);
            this.clearMenuItem.Text = "Clear           Del";
            this.clearMenuItem.Click += new System.EventHandler(this.clearMenuItem_Click);
            // 
            // viewMenuItem
            // 
            this.viewMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolbarMenuItem});
            this.viewMenuItem.Name = "viewMenuItem";
            this.viewMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewMenuItem.Text = "View";
            // 
            // toolbarMenuItem
            // 
            this.toolbarMenuItem.Checked = true;
            this.toolbarMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolbarMenuItem.Name = "toolbarMenuItem";
            this.toolbarMenuItem.Size = new System.Drawing.Size(114, 22);
            this.toolbarMenuItem.Text = "Toolbar";
            this.toolbarMenuItem.Click += new System.EventHandler(this.toolbarMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutPainterMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutPainterMenuItem
            // 
            this.aboutPainterMenuItem.Name = "aboutPainterMenuItem";
            this.aboutPainterMenuItem.Size = new System.Drawing.Size(165, 22);
            this.aboutPainterMenuItem.Text = "About program...";
            this.aboutPainterMenuItem.Click += new System.EventHandler(this.aboutPainterMenuItem_Click);
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.White;
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFileButton,
            this.openFileButton,
            this.saveFileButton,
            this.toolStripSeparator4,
            this.drawCircleButton,
            this.drawRectangleButton,
            this.drawPolygonButton,
            this.toolStripSeparator5,
            this.toolStripDropDownButton1,
            this.toolStripSeparator3,
            this.helpButton});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(517, 25);
            this.toolStrip.TabIndex = 1;
            // 
            // newFileButton
            // 
            this.newFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newFileButton.Image = ((System.Drawing.Image)(resources.GetObject("newFileButton.Image")));
            this.newFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newFileButton.Name = "newFileButton";
            this.newFileButton.Size = new System.Drawing.Size(23, 22);
            this.newFileButton.Text = "New";
            this.newFileButton.ToolTipText = "New";
            // 
            // openFileButton
            // 
            this.openFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openFileButton.Image = ((System.Drawing.Image)(resources.GetObject("openFileButton.Image")));
            this.openFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(23, 22);
            this.openFileButton.Text = "Open";
            this.openFileButton.ToolTipText = "Open";
            // 
            // saveFileButton
            // 
            this.saveFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveFileButton.Image = ((System.Drawing.Image)(resources.GetObject("saveFileButton.Image")));
            this.saveFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveFileButton.Name = "saveFileButton";
            this.saveFileButton.Size = new System.Drawing.Size(23, 22);
            this.saveFileButton.Text = "Save";
            this.saveFileButton.ToolTipText = "Save";
            this.saveFileButton.Click += new System.EventHandler(this.saveFileButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // drawCircleButton
            // 
            this.drawCircleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawCircleButton.Image = ((System.Drawing.Image)(resources.GetObject("drawCircleButton.Image")));
            this.drawCircleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawCircleButton.Name = "drawCircleButton";
            this.drawCircleButton.Size = new System.Drawing.Size(23, 22);
            this.drawCircleButton.Text = "Circle";
            this.drawCircleButton.Click += new System.EventHandler(this.drawCircleButton_Click);
            // 
            // drawRectangleButton
            // 
            this.drawRectangleButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawRectangleButton.Image = ((System.Drawing.Image)(resources.GetObject("drawRectangleButton.Image")));
            this.drawRectangleButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawRectangleButton.Name = "drawRectangleButton";
            this.drawRectangleButton.Size = new System.Drawing.Size(23, 22);
            this.drawRectangleButton.Text = "Rectangle";
            this.drawRectangleButton.Click += new System.EventHandler(this.drawRectangleButton_Click);
            // 
            // drawPolygonButton
            // 
            this.drawPolygonButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.drawPolygonButton.Image = ((System.Drawing.Image)(resources.GetObject("drawPolygonButton.Image")));
            this.drawPolygonButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.drawPolygonButton.Name = "drawPolygonButton";
            this.drawPolygonButton.Size = new System.Drawing.Size(23, 22);
            this.drawPolygonButton.Text = "Polygon";
            this.drawPolygonButton.Click += new System.EventHandler(this.drawPolygonButton_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // helpButton
            // 
            this.helpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpButton.Image = ((System.Drawing.Image)(resources.GetObject("helpButton.Image")));
            this.helpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(23, 22);
            this.helpButton.Text = "Info";
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // patternsList
            // 
            this.patternsList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("patternsList.ImageStream")));
            this.patternsList.TransparentColor = System.Drawing.Color.Transparent;
            this.patternsList.Images.SetKeyName(0, "Pattern0.bmp");
            this.patternsList.Images.SetKeyName(1, "Pattern1.bmp");
            this.patternsList.Images.SetKeyName(2, "Pattern2.bmp");
            this.patternsList.Images.SetKeyName(3, "pattern3.bmp");
            this.patternsList.Images.SetKeyName(4, "Pattern4.bmp");
            this.patternsList.Images.SetKeyName(5, "Pattern5.bmp");
            this.patternsList.Images.SetKeyName(6, "Pattern6.bmp");
            this.patternsList.Images.SetKeyName(7, "Pattern7.bmp");
            // 
            // patternsListView
            // 
            this.patternsListView.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.patternsListView.BackColor = System.Drawing.Color.White;
            this.patternsListView.BackgroundImageTiled = true;
            this.patternsListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.patternsListView.HideSelection = false;
            this.patternsListView.LargeImageList = this.patternsList;
            this.patternsListView.Location = new System.Drawing.Point(0, 339);
            this.patternsListView.MultiSelect = false;
            this.patternsListView.Name = "patternsListView";
            this.patternsListView.ShowGroups = false;
            this.patternsListView.Size = new System.Drawing.Size(517, 74);
            this.patternsListView.SmallImageList = this.patternsList;
            this.patternsListView.TabIndex = 3;
            this.patternsListView.UseCompatibleStateImageBehavior = false;
            this.patternsListView.View = System.Windows.Forms.View.SmallIcon;
            this.patternsListView.ItemActivate += new System.EventHandler(this.patternsListView_ItemActivate);
            // 
            // drawingPictureBox
            // 
            this.drawingPictureBox.BackColor = System.Drawing.Color.White;
            this.drawingPictureBox.Location = new System.Drawing.Point(0, 0);
            this.drawingPictureBox.Name = "drawingPictureBox";
            this.drawingPictureBox.Size = new System.Drawing.Size(600, 400);
            this.drawingPictureBox.TabIndex = 2;
            this.drawingPictureBox.TabStop = false;
            this.drawingPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawingPictureBox_Paint);
            this.drawingPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawingPictureBox_MouseDown);
            this.drawingPictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawingPictureBox_MouseMove);
            this.drawingPictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawingPictureBox_MouseUp);
            // 
            // drawingPanel
            // 
            this.drawingPanel.AutoScroll = true;
            this.drawingPanel.AutoSize = true;
            this.drawingPanel.BackColor = System.Drawing.Color.DimGray;
            this.drawingPanel.Controls.Add(this.drawingPictureBox);
            this.drawingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingPanel.Location = new System.Drawing.Point(0, 49);
            this.drawingPanel.Name = "drawingPanel";
            this.drawingPanel.Size = new System.Drawing.Size(517, 290);
            this.drawingPanel.TabIndex = 4;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // fToolStripMenuItem
            // 
            this.fToolStripMenuItem.Name = "fToolStripMenuItem";
            this.fToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.fToolStripMenuItem.Text = "f";
            // 
            // drawingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(517, 413);
            this.Controls.Add(this.drawingPanel);
            this.Controls.Add(this.patternsListView);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.HelpButton = true;
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "drawingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple Painter";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drawingForm_KeyDown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawingPictureBox)).EndInit();
            this.drawingPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsFIleMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem pagePropertyMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolbarMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutPainterMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton newFileButton;
        private System.Windows.Forms.ToolStripButton openFileButton;
        private System.Windows.Forms.ToolStripButton saveFileButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton drawCircleButton;
        private System.Windows.Forms.ToolStripButton drawRectangleButton;
        private System.Windows.Forms.ToolStripButton drawPolygonButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton helpButton;
        private System.Windows.Forms.PictureBox drawingPictureBox;
        private System.Windows.Forms.ImageList patternsList;
        private System.Windows.Forms.ListView patternsListView;
        private System.Windows.Forms.Panel drawingPanel;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem fToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

