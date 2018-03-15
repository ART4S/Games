using System.Drawing;

namespace AirForce
{
    public class FlyingObjectPainter: IPainer
    {
        private readonly FlyingObject flyingObject;

        public FlyingObjectPainter(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public void Paint(Graphics graphics)
        {
            Rectangle imageRectangle = new Rectangle
            {
                Location = flyingObject.Position - new Point2D(flyingObject.Radius, flyingObject.Radius),
                Size = new Size(2 * flyingObject.Radius, 2 * flyingObject.Radius)
            };

            graphics.DrawImage(flyingObject.Image, imageRectangle);
        }
    }
}