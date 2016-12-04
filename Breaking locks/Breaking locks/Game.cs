using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Breaking_locks
{
    public partial class Game : Form
    {
        private Stick lowStick;
        private Stick topStick;

        private Status lowStick_status;

        private const float a_90 = 90F;
        private const float a_180 = 180F;
        private const float a_270 = 270F;
        private const float a_360 = 360F;

        private const float shift_angle = 1F;

        private float sector_length = 45F;
        private float sector_angle_1;
        private float sector_angle_2;
        private float sector_angle_3;
        private float sector_angle_4;

        private bool lowStick_breaking = false;
        private bool cheat = false;

        private double luck_ratio;

        private const int strength_default = 200;
        private const int passkeys_default = 10;

        private int strength;
        private int Strength
        {
            get { return strength; }
            set
            {
                if (value == 0)
                {
                    Passkeys--;
                    strength = strength_default;
                    set_sticks_in_default_position();
                }
                else
                    strength = value;

                lbl_Strength.Text = "Strength: " + (100 * strength / strength_default).ToString() + "%";
            }
        }

        private int passkeys;
        private int Passkeys
        {
            set
            {
                if (value == 0)
                    passkeys = passkeys_default;
                else
                    passkeys = value;

                lbl_Passkeys.Text = "Passkeys: " + passkeys.ToString();
            }

            get { return passkeys; }
        }

        private int score;
        private int Score
        {
            get { return score; }
            set
            {
                score = value;

                set_sticks_in_default_position();
                Create_sector();

                lbl_score.Text = "Score: " + score.ToString();
            }
        }

        public Game()
        {
            InitializeComponent();
            start();
        }

        private void start()
        {
            var middle = new PointF(pB_Game.Width / 2, pB_Game.Height / 2);

            lowStick = new Stick(middle, a_90, pB_Game.Height / 2);
            topStick = new Stick(middle, a_270, pB_Game.Height / 2);

            lowStick_status = Status.Return;

            Strength = strength_default;
            Passkeys = passkeys_default;
            Score = 0;

            Create_sector();

            timer.Start();
        }

        private void Create_sector()
        {
            var rnd = new Random();

            sector_angle_1 = rnd.Next(180, Convert.ToInt16(a_360 - sector_length));
            sector_angle_2 = sector_angle_1 + sector_length / 3;
            sector_angle_3 = sector_angle_2 + sector_length / 3;
            sector_angle_4 = sector_angle_3 + sector_length / 3;

            luck_ratio = get_new_ratio(topStick.Angle);
        }

        private void timer_lowStick_Move(object sender, EventArgs e)
        {
            switch (lowStick_status)
            {
                case Status.Move:
                {
                    if (lowStick_breaking == false)
                    {
                        lowStick.Angle += shift_angle;

                        if (lowStick.Angle >= a_90 + luck_ratio * a_90)
                            lowStick_breaking = true;
                    }
                    else
                    {
                        if (luck_ratio == 1)
                            Score++;
                        else if(!cheat) Strength--;
                    }

                    pB_Game.Refresh();

                    break;
                }

                case Status.Return:
                {
                    lowStick_breaking = false;

                    if (lowStick.Angle > a_90)
                    {
                        lowStick.Angle -= shift_angle;

                        if (lowStick.Angle < a_90)
                        {
                            lowStick.Angle = a_90;
                            lowStick_status = Status.Sleep;
                        }
                    }

                    pB_Game.Refresh();

                    break;
                }

                default: break;
            }
        }

        private void set_sticks_in_default_position()
        {
            lowStick.Angle = a_90;
            topStick.Angle = a_270;

            luck_ratio = get_new_ratio(topStick.Angle);
            lowStick_breaking = false;
        }

        private void pB_Game_MouseMove(object sender, MouseEventArgs e)
        {
            if (lowStick_status == Status.Move) return;

            double NewAngle = Math.Atan2(e.X - lowStick.P0.X, e.Y - lowStick.P0.Y) / Math.PI * a_180;
            if (NewAngle < 0) NewAngle += a_360;
            NewAngle = (450F - NewAngle) % 360;

            if ((a_180 <= NewAngle && NewAngle <= a_360) || NewAngle == 0)
            {
                luck_ratio = get_new_ratio(NewAngle);
                topStick.Angle = NewAngle;
            }

            pB_Game.Refresh();
        }

        private double get_new_ratio(double angle)
        {
            if (sector_angle_1 < angle && angle < sector_angle_2)
            {
                return (angle - sector_angle_1) / (sector_length / 3);
            }

            if (sector_angle_3 < angle && angle < sector_angle_4)
            {
                return (sector_angle_4 - angle) / (sector_length / 3);
            }

            if (sector_angle_2 <= angle && angle <= sector_angle_3)
            {
                return 1;
            }

            return 0;
        }

        //-----------------------------------------------------------------

        private void pB_Game_Paint(object sender, PaintEventArgs e)
        {
            var pen = new Pen(Brushes.Black, 1);
            var pen_lowStick = new Pen(Brushes.Red, 5);
            var pen_topStrick = new Pen(Brushes.Blue, 5);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.DrawRectangle(pen, pen.Width / 2, pen.Width / 2, pB_Game.Width - pen.Width - 1, pB_Game.Height - pen.Width - 1);
            e.Graphics.DrawLine(pen, 0, pB_Game.Height / 2, pB_Game.Width, pB_Game.Height / 2);
            e.Graphics.DrawEllipse(pen, 0, 0, pB_Game.Height, pB_Game.Height);

            if (cheat)
            {
                e.Graphics.FillPie(Brushes.LightGreen, 0, 0, pB_Game.Height, pB_Game.Height, sector_angle_1, 45);
                e.Graphics.FillPie(Brushes.Green, 0, 0, pB_Game.Height, pB_Game.Height, sector_angle_2, 15);
            }

            if (lowStick_breaking)
                pen_lowStick.Brush = Brushes.Red;
            else
                pen_lowStick.Brush = Brushes.Green;

            e.Graphics.DrawLine(pen_topStrick, topStick.P0, topStick.P1);
            e.Graphics.DrawLine(pen_lowStick, lowStick.P0, lowStick.P1);

        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                lowStick_status = Status.Move;
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                lowStick_status = Status.Return;
        }

        private void lbl_cheat_Click(object sender, EventArgs e)
        {
            cheat = !cheat;
        }

        private void lbl_cheat_MouseHover(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Font = new Font(label.Font.Name, 15, label.Font.Style);
        }

        private void lbl_cheat_MouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Font = new Font(label.Font.Name, 14, label.Font.Style);
        }
    }
}
