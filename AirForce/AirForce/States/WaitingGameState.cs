using System.Drawing;

namespace AirForce
{
    public class WaitingGameState : IGameState
    {
        private readonly Game game;

        public WaitingGameState(Game game)
        {
            this.game = game;
        }

        public void Update() { }
        public void MovePlayer(Point2D movespeedModifer) { }
        public void PlayerFire() { }
        public void EndRewind() { }
        public void IncreaseSpeed() { }
        public void DecreaseSpeed() { }

        public void Paint(Graphics graphics)
        {
            PaintTextInCenterRectangle(
                graphics: graphics,
                text: "Press SHIFT",
                fontSize: 30,
                rectangle: game.Field);
        }

        private void PaintTextInCenterRectangle(Graphics graphics, string text, int fontSize, Rectangle2D rectangle)
        {
            var font = new Font("Segoe UI", fontSize, FontStyle.Bold);
            var brush = Brushes.White;
            var stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(text, font, brush, rectangle, stringFormat);
        }

        public void BeginRewind()
        {
            game.State = new RewindGameState(game);
        }
    }
}