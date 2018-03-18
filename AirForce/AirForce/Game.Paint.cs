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
                location: GameField.TopLeftPoint + new Point2D(4, 4));

            Ground.Paint(graphics);

            foreach (FlyingObject obj in FlyingObjects.FindAll(o => o.Strength > 0))
                obj.Paint(graphics);

            if (GameState is WaitingGameState)
                PaintTextInCenterRectangle(
                    graphics: graphics,
                    text: "Press ENTER to start game",
                    rectangle: GameField);
        }

        private void PaintStrengthBar(Graphics graphics, int strength, Point2D location)
        {
            // frame
            Pen framePen = new Pen(Color.White, 3);
            Rectangle frameRectangle = new Rectangle(location: location, size: new Size(303, 33));

            graphics.DrawRectangle(framePen, frameRectangle);

            // redLine
            Brush redLineBrush = Brushes.Red;
            Rectangle redLineRectangle = new Rectangle(location: location + new Point2D(2, 2), size: new Size(3 * strength, 30));

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

        private void PaintTextInCenterRectangle(Graphics graphics, string text, Rectangle rectangle)
        {
            var font = new Font("Segoe UI", 20, FontStyle.Bold);
            var brush = Brushes.White;
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(text, font, brush, rectangle, stringFormat);
        }
    }
}