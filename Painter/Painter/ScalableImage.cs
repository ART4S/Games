using System.Drawing;
using System.Drawing.Drawing2D;

namespace Paint
{
    public class ScalableImage
    {
        private readonly Image image;
        private InterpolationMode interpolationMode;

        public ScalableImage(Image image, InterpolationMode interpolationMode)
        {
            this.image = image;
            this.interpolationMode = interpolationMode;
        }

        public void Draw(Graphics graphics, Size drawingFrameSize)
        {
            graphics.InterpolationMode = interpolationMode;
            graphics.DrawImage(image, new Rectangle(new Point(0, 0), drawingFrameSize));
        }

        public void SetInterpolationMode(InterpolationMode interpolationMode)
        {
            this.interpolationMode = interpolationMode;
        }
    }
}