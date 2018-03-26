using System.Drawing;
using System.Linq;

namespace AirForce
{
    public class RewindGameState : IGameState
    {
        private readonly Game game;
        private int speed = Game.MinSpeed;

        public RewindGameState(Game game)
        {
            this.game = game;
        }

        public void MovePlayer(Point2D movespeedModifer) { }
        public void PlayerFire() { }
        public void BeginRewind() { }

        public void Update()
        {
            for (int i = 0; i < speed; i++)
            {
                if (game.RewindMacroCommands.Any())
                    UndoLastMacroCommand();
            }
        }

        private void UndoLastMacroCommand()
        {
            RewindMacroCommand rewindMacroCommand = game.RewindMacroCommands.Last();
            rewindMacroCommand.Undo();
            game.RewindMacroCommands.Remove(rewindMacroCommand);
        }

        public void EndRewind()
        {
            game.State = new PlayingGameState(game);
        }

        public void Paint(Graphics graphics)
        {
            PaintGameSpeed(graphics);
        }

        private void PaintGameSpeed(Graphics graphics)
        {
            string text = $"X{speed}";
            Font textPen = new Font("Segoe UI", 20, FontStyle.Bold);
            Brush textBrush = Brushes.White;
            Rectangle locationRectangle = new Rectangle(location: new Point(0, 50), size: new Size(50, 50));
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(text, textPen, textBrush, locationRectangle, stringFormat);
        }

        public void IncreaseSpeed()
        {
            if (speed < Game.MaxSpeed)
                speed++;
        }

        public void DecreaseSpeed()
        {
            if (speed > Game.MinSpeed)
                speed--;
        }
    }
}