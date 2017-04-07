using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Paint.Properties;

namespace Paint
{
    public partial class ImageViewerForm : Form
    {
        private InterpolatedImage firstInterpolatedImage;
        private InterpolatedImage secondInterpolatedImage;
        private InterpolatedImage selectedInterpolatedImage;

        public ImageViewerForm()
        {
            InitializeComponent();

            newMenuItem.Click             += (sender, e) => SetInterpolatedImagesViaOpenDialogBox();
            openMenuItem.Click            += (sender, e) => SetInterpolatedImagesViaOpenDialogBox();
            exitMenuItem.Click            += (sender, e) => Close();
            helpMenuItem.Click            += (sender, e) => MessageBox.Show(Resources.ImageViewerFormHelpText, "Help");

            firstPictureBox.Click         += (sender, e) => selectedInterpolatedImage = firstInterpolatedImage;
            secondPictureBox.Click        += (sender, e) => selectedInterpolatedImage = secondInterpolatedImage;

            firstPictureBox.Paint         += (sender, e) => firstInterpolatedImage?.Draw(e.Graphics, firstPictureBox.Size);
            secondPictureBox.Paint        += (sender, e) => secondInterpolatedImage?.Draw(e.Graphics, secondPictureBox.Size);

            nearestNeighborMenuItem.Click += (sender, e) => SetInterpolationModeToSelectedInterpolatedImage(InterpolationMode.NearestNeighbor);
            bilinearMenuItem.Click        += (sender, e) => SetInterpolationModeToSelectedInterpolatedImage(InterpolationMode.HighQualityBilinear);
            bicubicMenuItem.Click         += (sender, e) => SetInterpolationModeToSelectedInterpolatedImage(InterpolationMode.HighQualityBicubic);

            splitContainer.SplitterDistance = 0;
        }

        private void SetInterpolatedImagesViaOpenDialogBox()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Resources.ImageFilterPattern
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image selectedImage = Image.FromFile(openFileDialog.FileName);

                firstInterpolatedImage = new InterpolatedImage(selectedImage, InterpolationMode.Default);
                secondInterpolatedImage = new InterpolatedImage(selectedImage, InterpolationMode.Default);
                selectedInterpolatedImage = secondInterpolatedImage;

                firstPictureBox.Size = selectedImage.Size;
                secondPictureBox.Size = selectedImage.Size;

                splitContainer.Refresh();
            }
        }

        private void SetInterpolationModeToSelectedInterpolatedImage(InterpolationMode interpolationMode)
        {
            selectedInterpolatedImage?.SetInterpolationMode(interpolationMode);

            splitContainer.Refresh();
        }

        private void ImageViewerForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;

            const int pixelsCount = 20;

            if (pressedKey == Keys.E && e.Control)
                ChangePictureBoxSizeToPixelsCount(
                    selectedInterpolatedImage == firstInterpolatedImage ? firstPictureBox : secondPictureBox, pixelsCount);

            if (pressedKey == Keys.Q && e.Control)
                ChangePictureBoxSizeToPixelsCount(
                    selectedInterpolatedImage == firstInterpolatedImage ? firstPictureBox : secondPictureBox, -pixelsCount);

            splitContainer.Refresh();
        }

        private void ChangePictureBoxSizeToPixelsCount(PictureBox pictureBox, int pixelsCount)
        {
            pictureBox.Size = new Size(pictureBox.Width + pixelsCount, pictureBox.Height + pixelsCount);
        }
    }
}
