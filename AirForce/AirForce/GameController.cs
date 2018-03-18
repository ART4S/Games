using System.Collections.Generic;
using System.Windows.Forms;

namespace AirForce
{
    public class GameController
    {
        private readonly Game game;
        private readonly Coooldown playerShootingCoooldown = new Coooldown(currentValue: 30, maxValue: 30);
        private readonly Coooldown enemiesCreatingCooldown = new Coooldown(currentValue: 80, maxValue: 80);
        private readonly Timer updatingTimer = new Timer();

        private readonly Dictionary<Keys, bool> pressedKeys = new Dictionary<Keys, bool>
        {
            {Keys.W, false},
            {Keys.S, false},
            {Keys.A, false},
            {Keys.D, false},
            {Keys.Space, false},
            {Keys.Enter, false},
            {Keys.ShiftKey, false}
        };

        public GameController(Control display)
        {
            game = new Game(display.Size);

            display.Paint += (s, e) => game.Paint(e.Graphics);

            updatingTimer.Interval = 1;
            updatingTimer.Tick += (s, e) =>
            {
                Update();
                display.Refresh();
            };
            updatingTimer.Start();
        }

        public void KeyDown(Keys pressedKey)
        {
            if (pressedKeys.ContainsKey(pressedKey))
                pressedKeys[pressedKey] = true;
        }

        public void KeyUp(Keys unpressedKey)
        {
            if (pressedKeys.ContainsKey(unpressedKey))
                pressedKeys[unpressedKey] = false;
        }

        private void Update()
        {
            if (pressedKeys[Keys.Enter])
                game.Restart();

            if (pressedKeys[Keys.ShiftKey])
                game.BeginRewind();

            if (!pressedKeys[Keys.ShiftKey])
                game.EndRewind();

            MovePlayer();
            PlayerFire();
            AddNewRandomEnemy();

            game.Update();
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

        private void AddNewRandomEnemy()
        {
            if (enemiesCreatingCooldown.Tick())
                game.AddNewRandomEnemy();
        }
    }
}