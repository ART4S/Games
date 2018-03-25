using System.Drawing;
using System.Linq;

namespace AirForce
{
    public class RewindGameState : IGameState
    {
        private readonly Game game;

        public RewindGameState(Game game)
        {
            this.game = game;
        }

        public void MovePlayer(Point2D movespeedModifer) { }
        public void PlayerFire() { }
        public void BeginRewind() { }

        public void Update()
        {
            if (game.RewindMacroCommands.Count == 0)
                return;

            for (int i = 0; i < game.Speed; i++)
                UndoLastMacroCommand();
        }

        private void UndoLastMacroCommand()
        {
            RewindMacroCommand rewindMacroCommand = game.RewindMacroCommands.Last();
            rewindMacroCommand.Undo();
            game.RewindMacroCommands.Remove(rewindMacroCommand);
        }

        public void EndRewind()
        {
            game.Speed = Game.MinSpeed;
            game.State = new PlayingGameState(game);
        }

        public void Paint(Graphics graphics)
        {
            PaintGameSpeed(graphics, new Point(0, 50));
        }

        private void PaintGameSpeed(Graphics graphics, Point location)
        {
            string text = $"X{game.Speed}";
            Font textPen = new Font("Segoe UI", 20, FontStyle.Bold);
            Brush textBrush = Brushes.White;
            Rectangle locationRectangle = new Rectangle(location: location, size: new Size(50, 50));
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(text, textPen, textBrush, locationRectangle, stringFormat);
        }

        public void IncreaseSpeed()
        {
            if (game.Speed < Game.MaxSpeed)
                game.Speed++;
        }

        public void DecreaseSpeed()
        {
            if (game.Speed > Game.MinSpeed)
                game.Speed--;
        }
    }
}