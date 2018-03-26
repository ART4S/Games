using System.Drawing;

namespace AirForce
{
    public partial class Game
    {
        public void Paint(Graphics graphics)
        {
            PaintStrengthBar(
                graphics: graphics,
                strength: Player.Strength,
                location: Field.Location + new Point2D(4, 4));

            graphics.FillRectangle(Brushes.Green, Ground);

            foreach (FlyingObject obj in ObjectsOnField)
                obj.Paint(graphics);

            State.Paint(graphics);
        }

        private void PaintStrengthBar(Graphics graphics, int strength, Point2D location)
        {
            // frame
            Pen framePen = new Pen(Color.White, 3);
            Rectangle2D frameRectangle = new Rectangle2D(
                location: location,
                size: new Size2D(303, 33));

            graphics.DrawRectangle(framePen, frameRectangle);

            // redLine
            Brush redLineBrush = Brushes.Red;
            Rectangle2D redLineRectangle = new Rectangle2D(
                location: location + new Point2D(2, 2),
                size: new Size2D(3 * strength, 30));

            graphics.FillRectangle(redLineBrush, redLineRectangle);

            // text
            string text = strength.ToString();
            Font textPen = new Font("Segoe UI", 13, FontStyle.Bold);
            Brush textBrush = Brushes.White;
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(text, textPen, textBrush, frameRectangle, stringFormat);
        }
    }
}