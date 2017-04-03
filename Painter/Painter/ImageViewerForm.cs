using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Paint.Properties;

namespace Paint
{
    public partial class ImageViewerForm : Form
    {
        private ScalableImage firstScalableImage;
        private ScalableImage secondScalableImage;
        private ScalableImage selectedScalableImage;

        public ImageViewerForm()
        {
            InitializeComponent();

            newImageMenuItem.Click        += (sender, e) => InitScalableImagesViaOpenDialogBox();
            openNewImageMenuItem.Click    += (sender, e) => InitScalableImagesViaOpenDialogBox();
            exitMenuItem.Click            += (sender, e) => Close();
            helpMenuItem.Click            += (sender, e) => MessageBox.Show(Resources.ImageViewerFormHelpText, "Help");

            firstPictureBox.Click         += (sender, e) => selectedScalableImage = firstScalableImage;
            secondPictureBox.Click        += (sender, e) => selectedScalableImage = secondScalableImage;

            firstPictureBox.Paint         += (sender, e) => firstScalableImage?.Draw(e.Graphics, firstPictureBox.Size);
            secondPictureBox.Paint        += (sender, e) => secondScalableImage?.Draw(e.Graphics, secondPictureBox.Size);

            nearestNeighborMenuItem.Click += (sender, e) => SetInterpolationModeToSelectedScalableImage(InterpolationMode.NearestNeighbor);
            bilinearMenuItem.Click        += (sender, e) => SetInterpolationModeToSelectedScalableImage(InterpolationMode.Bilinear);
            bicubicMenuItem.Click         += (sender, e) => SetInterpolationModeToSelectedScalableImage(InterpolationMode.Bicubic);

            splitContainer.SplitterDistance = 0;
        }

        private void InitScalableImagesViaOpenDialogBox()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Resources.ImageFilterPattern
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image selectedImage = Image.FromFile(openFileDialog.FileName);

                firstScalableImage = new ScalableImage(selectedImage, InterpolationMode.Default);
                secondScalableImage = new ScalableImage(selectedImage, InterpolationMode.Default);
                selectedScalableImage = secondScalableImage;

                firstPictureBox.Size = selectedImage.Size;
                secondPictureBox.Size = selectedImage.Size;

                splitContainer.Refresh();
            }
        }

        private void ImageViewerForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;

            const int pixelsCount = 10;

            if (pressedKey == Keys.E && e.Control)
                ChangePictureBoxSizeToPixelsCount(
                    selectedScalableImage == firstScalableImage ? firstPictureBox : secondPictureBox, pixelsCount);

            if (pressedKey == Keys.Q && e.Control)
                ChangePictureBoxSizeToPixelsCount(
                    selectedScalableImage == firstScalableImage ? firstPictureBox : secondPictureBox, -pixelsCount);

            splitContainer.Refresh();
        }

        private void ChangePictureBoxSizeToPixelsCount(PictureBox pictureBox, int pixelsCount)
        {
            pictureBox.Size = new Size(pictureBox.Width + pixelsCount, pictureBox.Height + pixelsCount);
        }

        private void SetInterpolationModeToSelectedScalableImage(InterpolationMode interpolationMode)
        {
            selectedScalableImage?.SetInterpolationMode(interpolationMode);

            splitContainer.Refresh();
        }
    }
}
