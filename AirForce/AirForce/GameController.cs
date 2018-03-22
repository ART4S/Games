using System.Collections.Generic;
using System.Windows.Forms;

namespace AirForce
{
    public class GameController
    {
        private readonly Game game;
        private readonly Coooldown playerShootingCoooldown = new Coooldown(currentValue: 10, maxValue: 10);
        private readonly Timer updatingTimer = new Timer();

        private const int MaxSpeed = 9;
        private const int MinSpeed = 1;
        private int gameSpeed = MinSpeed;

        private readonly Dictionary<Keys, bool> pressedKeys = new Dictionary<Keys, bool>
        {
            {Keys.W, false},
            {Keys.S, false},
            {Keys.A, false},
            {Keys.D, false},
            {Keys.Q, false},
            {Keys.E, false},
            {Keys.Space, false},
            {Keys.Enter, false},
            {Keys.ShiftKey, false},
        };

        public GameController(Control display)
        {
            game = new Game(display.Size);

            display.Paint += (s, e) => game.Paint(e.Graphics);

            updatingTimer.Interval = 15;
            updatingTimer.Tick += (s, e) =>
            {
                for (int i = 0; i < gameSpeed; i++)
                {
                    Update();
                    display.Refresh();
                }
            };
        }

        public void StartGame()
        {
            updatingTimer.Start();
        }

        public void StopGame()
        {
            updatingTimer.Stop();
        }

        public void KeyDown(Keys pressedKey)
        {
            if (pressedKeys.ContainsKey(pressedKey))
                pressedKeys[pressedKey] = true;

            if (pressedKeys[Keys.Q] && gameSpeed < MaxSpeed)
                gameSpeed++;

            if (pressedKeys[Keys.E] && gameSpeed > MinSpeed)
                gameSpeed--;

            if (pressedKeys[Keys.Enter])
                game.Restart();

            if (pressedKeys[Keys.ShiftKey])
                game.BeginRewind();
        }

        public void KeyUp(Keys unpressedKey)
        {
            if (pressedKeys.ContainsKey(unpressedKey))
                pressedKeys[unpressedKey] = false;

            if (!pressedKeys[Keys.ShiftKey])
                game.EndRewind();
        }

        private void Update()
        {
            MovePlayer();
            PlayerFire();

            game.Update();

            if (game.GameState is WaitingGameState)
                gameSpeed = MinSpeed;
        }

        private void MovePlayer()
        {
            Point2D playerMovespeedModifer = new Point2D();

            if (pressedKeys[Keys.W])
                playerMovespeedModifer += new Point2D(0, -1);

            if (pressedKeys[Keys.S])
                playerMovespeedModifer += new Point2D(0, 1);

            if (pressedKeys[Keys.A])
                playerMovespeedModifer += new Point2D(-1, 0);

            if (pressedKeys[Keys.D])
                playerMovespeedModifer += new Point2D(1, 0);

            game.MovePlayer(playerMovespeedModifer);
        }

        private void PlayerFire()
        {
            if (!pressedKeys[Keys.Space])
            {
                playerShootingCoooldown.SetOnTick();
                return;
            }

            if (playerShootingCoooldown.Tick())
                game.PlayerFire();
        }
    }
}