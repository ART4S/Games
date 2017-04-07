using System.Drawing;
using System.Drawing.Drawing2D;

namespace Paint
{
    public class InterpolatedImage
    {
        private readonly Image image;
        private InterpolationMode interpolationMode;

        public InterpolatedImage(Image image, InterpolationMode interpolationMode)
        {
            this.image = image;
            this.interpolationMode = interpolationMode;
        }

        public void Draw(Graphics graphics, Size drawingFrameSize)
        {
            Rectangle drawingRectangle = new Rectangle(new Point(0, 0), drawingFrameSize);

            graphics.InterpolationMode = interpolationMode;
            graphics.DrawImage(image, drawingRectangle);
        }

        public void SetInterpolationMode(InterpolationMode interpolationMode)
        {
            this.interpolationMode = interpolationMode;
        }
    }
}