using System.Drawing;
using System.Linq;

namespace AirForce
{
    public class GamePainter : IPainer
    {
        private readonly GameController game;

        public GamePainter(GameController game)
        {
            this.game = game;
        }

        public void Paint(Graphics graphics)
        {
            PaintStrengthBar(
                graphics,
                game.Player.Strength,
                game.GameField.TopLeftPoint + new Point2D(4, 4));

            game.Ground.Paint(graphics);

            foreach (FlyingObject obj in game.FlyingObjects)
                obj.Paint(graphics);

            if (game.GameState is WaitingGameState)
                PaintTextInCenterRectangle(graphics, "Press ENTER to start game", game.GameField);
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

        private void PaintStrengthBar(Graphics graphics, int strength, Point2D location)
        {
            // frame
            Pen framePen = new Pen(Color.White, 3);
            Rectangle frameRectangle = new Rectangle
            {
                Location = location,
                Size = new Size(303, 33)
            };

            graphics.DrawRectangle(framePen, frameRectangle);

            // redLine
            Brush redLineBrush = Brushes.Red;
            Rectangle redLineRectangle = new Rectangle
            {
                Location = location + new Point2D(2, 2),
                Size = new Size(3 * strength, 30)
            };

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