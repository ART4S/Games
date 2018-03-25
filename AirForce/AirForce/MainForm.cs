﻿using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    public sealed partial class MainForm : Form
    {
        private readonly Game game;
        private readonly Coooldown playerShootingCoooldown = new Coooldown(maxValue: 20, isCollapsed: true);
        private readonly Timer updatingTimer = new Timer();

        private readonly Dictionary<Keys, bool> pressedKeys = new Dictionary<Keys, bool>
        {
            {Keys.W, false},
            {Keys.S, false},
            {Keys.A, false},
            {Keys.D, false},
            {Keys.Q, false},
            {Keys.E, false},
            {Keys.Space, false},
            {Keys.ShiftKey, false},
        };

        public MainForm()
        {
            InitializeComponent();

            game = new Game(GameFieldPictureBox.Size);

            GameFieldPictureBox.Paint += (s, e) => game.Paint(e.Graphics);

            updatingTimer.Interval = 15;
            updatingTimer.Tick += (s, e) => UpdateGame();
            updatingTimer.Start();
        }

        private void UpdateGame()
        {
            PlayerFire();
            game.Update();
            game.MovePlayer(GetPlayerMoveSpeedModifer());
            GameFieldPictureBox.Refresh();
        }

        private Point2D GetPlayerMoveSpeedModifer()
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

            return playerMovespeedModifer;
        }

        private void PlayerFire()
        {
            if (!pressedKeys[Keys.Space])
            {
                playerShootingCoooldown.SetOneTickToCollapse(new RewindMacroCommand());
                return;
            }

            playerShootingCoooldown.Tick(new RewindMacroCommand());

            if (playerShootingCoooldown.IsCollapsed)
                game.PlayerFire();
        }

        //private void PaintGameSpeed(Graphics graphics, Point location)
        //{
        //    string text = $"X{gameSpeed}";
        //    Font textPen = new Font("Segoe UI", 20, FontStyle.Bold);
        //    Brush textBrush = Brushes.White;
        //    Rectangle locationRectangle = new Rectangle(location: location, size: new Size(50, 50));
        //    StringFormat stringFormat = new StringFormat
        //    {
        //        Alignment = StringAlignment.Center,
        //        LineAlignment = StringAlignment.Center
        //    };

        //    graphics.DrawString(text, textPen, textBrush, locationRectangle, stringFormat);
        //}

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;

            if (pressedKeys.ContainsKey(pressedKey))
                pressedKeys[pressedKey] = true;

            if (pressedKeys[Keys.ShiftKey])
                game.BeginRewind();

            if (pressedKeys[Keys.Q])
                game.IncreaseSpeed();

            if (pressedKeys[Keys.E])
                game.DecreaseSpeed();
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            Keys unpressedKey = e.KeyCode;

            if (pressedKeys.ContainsKey(unpressedKey))
                pressedKeys[unpressedKey] = false;

            if (!pressedKeys[Keys.ShiftKey])
                game.EndRewind();
        }
    }
}
