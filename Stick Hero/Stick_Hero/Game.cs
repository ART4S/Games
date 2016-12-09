using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Stick_Hero
{
    public partial class Game : Form
    {
        private const int shift = 10;
        private const int label_MinIndent = 10;
        private const int label_MaxIndent = 30;
        private const int rect_maxWidth = 70;
        private const int rect_maxHeight = 350;

        private RectangleF rectPrev;
        private RectangleF rectMain;
        private RectangleF rectNext;
        private RectangleF rectHero;

        private Line stickPrev;
        private Line stickMain;
        private Line stickBonus;

        private float rectMain_border;
        private float rectNext_border;
        private float stickMain_border;
        private float stickMain_height;
        private float labelPlay_upperBorder;
        private float labelPlay_lowerBorder;

        private bool isMoveUpLabelPlay;
        private bool isPrintInfoDevelopers;

        private double alpha;
        private short score;

        private GameState status;

        private LinearGradientBrush brush_gradient;

        private Pen pen_stick;
        private Pen pen_bonus;
        private Pen pen_score;

        private readonly Brush brush_rect = Brushes.Black;
        private readonly Font fontString = new Font("Tahoma", 20, FontStyle.Regular);
        private readonly Image im_Hero = Properties.Resources.im_Hero;
        private readonly Random rnd = new Random();

        //=========== Main Part ===========//

        public Game()
        {
            InitializeComponent();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            SetLocations();
            SetParametersBrushes();
            SetLabelPlay();
            Start();
        }

        private void SetLocations()
        {
            labelName.Location = new Point((ClientSize.Width - labelName.Width) / 2, label_MaxIndent);
            labelPlay.Location = new Point((ClientSize.Width - labelPlay.Width) / 2, (ClientSize.Height - labelPlay.Height) / 2);

            labelInfoDevelopers.Location = new Point((ClientSize.Width - label_MinIndent) / 2 - labelInfoDevelopers.Width, 8 * label_MaxIndent);
            labelStartScreen.Location = new Point(labelInfoDevelopers.Location.X - labelStartScreen.Width - label_MinIndent, 8 * label_MaxIndent);

            labelSite.Location = new Point((ClientSize.Width + label_MinIndent) / 2, 8 * label_MaxIndent);
            labelRestart.Location = new Point(labelSite.Location.X + labelSite.Width + label_MinIndent, 8 * label_MaxIndent);
        }

        private void SetParametersBrushes()
        {
            pen_score = new Pen(Color.FromArgb(40, 0, 0, 0), 20) {LineJoin = LineJoin.Round};
            pen_score.MiterLimit = pen_score.Width;

            pen_stick = new Pen(Color.Black, 3);
            pen_bonus = new Pen(Color.Red, 4);

            brush_gradient = new LinearGradientBrush(new Point(ClientSize.Width / 2, 0), new Point(ClientSize.Width / 2, ClientSize.Height), Color.LightSeaGreen, Color.SeaGreen);
        }

        private void SetLabelPlay()
        {
            var GPath = new GraphicsPath();

            GPath.AddEllipse(0, 0, labelPlay.Width, labelPlay.Height);
            labelPlay.Region = new Region(GPath);
            GPath.Dispose();

            labelPlay_lowerBorder = labelPlay.Location.Y + 5;
            labelPlay_upperBorder = labelPlay.Location.Y - 5;
        }

        //========== Drawing Part ==========//

        private void timer_Tick(object sender, EventArgs e)
        {
            switch (status)
            {
                case GameState.startScreen: MoveLabelPlay(); break;

                case GameState.start:
                {
                    ShiftRect(ref rectPrev, 0);
                    ShiftRect(ref rectMain, rectMain_border);
                    ShiftRect(ref rectNext, rectNext_border);

                    ShiftStick(ref stickPrev, 0);
                    ShiftStick(ref stickMain, stickMain_border);

                    ShiftRectHero();
                    ShiftStickBonus();
                    CheckWaitingClick();

                    break;
                }

                case GameState.stickUp: StickUp(); break;

                case GameState.stickDrop: StickDropToHorizon(); break;

                case GameState.nextLevel: HeroMoveToNextRect(); break;

                case GameState.lose:
                {
                    HeroMoveToAbyss();
                    StickDropToAbyss();

                    break;
                }

                case GameState.finalScreen:
                {
                    FinalScreenLabelVisible(true);
                    timer.Stop();

                    break;
                }
            }

            Refresh();
        }

        private void Game_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(brush_gradient, ClientRectangle);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            switch (status)
            {
                case GameState.startScreen:
                {
                    e.Graphics.FillRectangle(brush_rect, rectMain);
                    e.Graphics.DrawImage(im_Hero, rectHero);

                    return;
                }

                case GameState.finalScreen:
                {
                    DrawFinalPanel(e);

                    if (isPrintInfoDevelopers)
                    {
                        PrintInfoItem("ART4S", "DEVELOPER", 3, e);
                        PrintInfoItem("2016", "YEAR", 5, e);
                    }
                    else
                    {
                        PrintInfoItem(score.ToString(), "SCORE", 3, e);
                        PrintInfoItem(Properties.Settings.Default.score_best.ToString(), "BEST", 5, e);

                        if (Properties.Settings.Default.score_best == score)
                            PrintLabelNEWBESTSCORE(e);
                    }

                    break;
                }

                default:
                {
                    DrawScorePanel(e);
                    PrintStartMessage(e);

                    break;
                }
            }

            DrawRectangles(e);
            DrawLines(e);
            e.Graphics.DrawImage(im_Hero, rectHero);
        }


        private void DrawRectangles(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(brush_rect, rectPrev);
            e.Graphics.FillRectangle(brush_rect, rectMain);
            e.Graphics.FillRectangle(brush_rect, rectNext);
        }

        private void DrawLines(PaintEventArgs e)
        {
            e.Graphics.DrawLine(pen_bonus, stickBonus.X0, stickBonus.Y0, stickBonus.X, stickBonus.Y);
            e.Graphics.DrawLine(pen_stick, stickPrev.X0, stickPrev.Y0, stickPrev.X, stickPrev.Y);
            e.Graphics.DrawLine(pen_stick, stickMain.X0, stickMain.Y0, stickMain.X, stickMain.Y);
        }

        private void DrawFinalPanel(PaintEventArgs e)
        {
            const string text_over = "GAME OVER!";

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 0, 0, 0)), ClientRectangle);

            e.Graphics.DrawString(text_over,
                                  fontString,
                                  Brushes.White,
                                  (ClientSize.Width - text_over.Length * fontString.Height / 2) / 2,
                                  label_MaxIndent);

            e.Graphics.FillRectangle(Brushes.White,
                                     (ClientSize.Width - text_over.Length * fontString.Height / 2) / 2,
                                     3 * label_MaxIndent,
                                     text_over.Length * fontString.Height / 2,
                                     4 * label_MaxIndent);

            pen_score.Color = Color.White;

            e.Graphics.DrawRectangle(pen_score,
                                     (ClientSize.Width - text_over.Length * fontString.Height / 2) / 2,
                                     3 * label_MaxIndent,
                                     text_over.Length * fontString.Height / 2,
                                     4 * label_MaxIndent);

            pen_score.Color = Color.FromArgb(40, 0, 0, 0);
        }

        private void DrawScorePanel(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(pen_score,
                                     (ClientSize.Width - score.ToString().Length * fontString.Height / 2) / 2,
                                     2 * label_MaxIndent,
                                     score.ToString().Length * fontString.Height / 2 + pen_score.Width / 4,
                                     pen_score.Width);

            e.Graphics.DrawString(score.ToString(),
                                  fontString,
                                  Brushes.White,
                                  (ClientSize.Width - score.ToString().Length * fontString.Height / 2) / 2,
                                  2 * label_MaxIndent - pen_score.Width / 4);
        }

        private void PrintInfoItem(string item_text, string item_name, int shift_label_indent, PaintEventArgs e)
        {
            e.Graphics.DrawString(item_name,
                                  new Font("Tahoma", fontString.Size / 2, FontStyle.Bold),
                                  Brushes.Black,
                                  (ClientSize.Width - item_name.Length * fontString.Height / 4) / 2,
                                  shift_label_indent * label_MaxIndent);

            e.Graphics.DrawString(item_text,
                                  fontString,
                                  Brushes.Black,
                                  (ClientSize.Width - item_text.Length * fontString.Height / 2) / 2,
                                  shift_label_indent * label_MaxIndent + (float)0.7 * fontString.Height);
        }

        private void PrintLabelNEWBESTSCORE(PaintEventArgs e)
        {
            e.Graphics.DrawString("NEW!",
                                  new Font("Tahoma", fontString.Size / 3, FontStyle.Bold),
                                  Brushes.Red,
                                  (ClientSize.Width - Properties.Settings.Default.score_best.ToString().Length * fontString.Height / 2) / 2 +
                                  Properties.Settings.Default.score_best.ToString().Length * fontString.Height / 2 + 4,
                                  5 * label_MaxIndent + fontString.Height);
        }

        private void PrintStartMessage(PaintEventArgs e)
        {
            if (score != 0 || stickMain_height != 0) return;

            string message = "Hold the space to stretch out the stick";

            e.Graphics.DrawString( message,
                                   new Font("Tahoma", fontString.Size / 2, FontStyle.Bold),
                                   Brushes.Black,
                                   (ClientSize.Width - message.Length * fontString.Height / 5) / 2,
                                   4 * label_MaxIndent);
        }

        //========== Clicks ==========//

        private void labelPlay_Click(object sender, EventArgs e)
        {
            StartScreenLabelVisible(false);

            timer.Interval = 1;

            status = GameState.start;
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {         
            if (e.KeyCode == Keys.Space && status == GameState.waitingClick)
                status = GameState.stickUp;
        }

        private void Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space || status != GameState.stickUp) return;

            stickMain_height = stickMain.Y0 - stickMain.Y;
            status = GameState.stickDrop;

            System.Threading.Thread.Sleep(300);
        }


        private void labelStartScreen_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void labelRestart_Click(object sender, EventArgs e)
        {
            Restart();
        }

        private void labelInfoDevelopers_Click(object sender, EventArgs e)
        {
            isPrintInfoDevelopers = !isPrintInfoDevelopers;

            Refresh();
        }

        private void labelSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/id306040654");
        }

        //========== Logical Part 1 ==========//

        private void MoveLabelPlay()
        {
            if (isMoveUpLabelPlay)
            {
                labelPlay.Location = new Point(labelPlay.Location.X, labelPlay.Location.Y - 1);

                if (labelPlay.Location.Y == labelPlay_upperBorder)
                    isMoveUpLabelPlay = false;
            }
            else
            {
                labelPlay.Location = new Point(labelPlay.Location.X, labelPlay.Location.Y + 1);

                if (labelPlay.Location.Y == labelPlay_lowerBorder)
                    isMoveUpLabelPlay = true;
            }
        }

        private void ShiftRect(ref RectangleF rect, float border)
        {
            if (rect.X - shift >= border)
                rect.X -= shift;
            else
            {
                rect.X = border;

                if (rect == rectPrev)
                {
                    if (rect.Width - shift >= 0)
                        rect.Width -= shift;
                    else
                        rect.Width = 0;
                }
            }

            if (rect.Y - shift > rect_maxHeight)
                rect.Y -= shift;
            else
                rect.Y = rect_maxHeight;
        }

        private void ShiftRectHero()
        {
            rectHero.X = rectMain.X + rectMain.Width - rectHero.Width;
            rectHero.Y = rectMain.Y - rectHero.Height;
        }

        private void ShiftStick(ref Line stick, float border)
        {
            if (!(stick.X > border)) return;

            stick.X -= shift;
            if (stick.X < border) stick.X = border;

            stick.X0 -= shift;
            if (stick.X0 < 0) stick.X0 = 0;
        }

        private void ShiftStickBonus()
        {
            stickBonus.X0 = rectNext.X + (rectNext.Width - pen_bonus.Width)/2;
            stickBonus.X = stickBonus.X0 + pen_bonus.Width;
        }

        private void CheckWaitingClick()
        {
            if (rectMain.X != rectMain_border || rectNext.X != rectNext_border) return;

            stickPrev = stickMain;
            stickMain = new Line(rectMain.X + rectMain.Width, rectMain.Y, rectMain.X + rectMain.Width, rectMain.Y);

            status = GameState.waitingClick;
        }


        private void StickUp()
        {
            if (stickMain.Y - 4 >= 0)
                stickMain.Y -= 4;
            else
                stickMain.Y = 0;
        }
      
        private void StickDropToHorizon()
        {
            StickDrop(stickMain_height, 0);

            if (stickMain.X == stickMain.X0 + stickMain_height && stickMain.Y == stickMain.Y0)
                CheckLose();
        }

        private void StickDropToAbyss()
        {
            if (rectHero.Y != ClientSize.Height) return;

            StickDrop(0, stickMain_height);

            if (stickMain.X != stickMain.X0 || stickMain.Y != stickMain.Y0 + stickMain_height) return;

            CheckBestScore();
            status = GameState.finalScreen;
        }

        private void StickDrop(float finalShift_X0, float finalShift_Y0)
        {
            float newX = stickMain.X0 + (stickMain.X - stickMain.X0) * (float)Math.Cos(alpha) - (stickMain.Y - stickMain.Y0) * (float)Math.Sin(alpha);
            float newY = stickMain.Y0 + (stickMain.Y - stickMain.Y0) * (float)Math.Cos(alpha) + (stickMain.X - stickMain.X0) * (float)Math.Sin(alpha);
            float border1, border2;

            // расстановка параметров
            if (finalShift_X0 == 0)
            {
                border1 = newX;
                border2 = stickMain.X0; 
            }
            else
            {
                border1 = stickMain.Y0;
                border2 = newY;
            }
            //_______________________

            if (border1 >= border2)
            {
                stickMain.X = newX;
                stickMain.Y = newY;
                alpha += 0.004;
            }
            else
            {
                stickMain.X = stickMain.X0 + finalShift_X0;
                stickMain.Y = stickMain.Y0 + finalShift_Y0;
            }
        }


        private void HeroMoveToAbyss()
        {
            rectHero.X = NewCoordinate(5, rectHero.X, stickMain.X);

            if (rectHero.X == stickMain.X)
                rectHero.Y = NewCoordinate(shift, rectHero.Y, ClientSize.Height);
        }

        private void HeroMoveToNextRect()
        {
            float rectHero_borderX = rectNext.X + rectNext.Width - rectHero.Width;

            rectHero.X = NewCoordinate(5, rectHero.X, rectHero_borderX);

            if (rectHero.X != rectHero_borderX) return;

            rectPrev = rectMain;

            rectMain = rectNext;
            rectMain_border = (rect_maxWidth - rectMain.Width)/2;
            stickMain_border = rectMain_border + stickMain.X - rectMain.X;

            GenerateRectNext();

            alpha = 0;
            score++;

            status = GameState.start;
        }

        private float NewCoordinate(int SHIFT, float coordinate, float border)
        {
            if (coordinate + SHIFT <= border)
                return coordinate + SHIFT;

            return border;
        }


        private void GenerateRectNext()
        {
            rectNext = new RectangleF(ClientSize.Width,
                                      rect_maxHeight,
                                      rnd.Next((int)rectHero.Width, rect_maxWidth),
                                      rect_maxHeight);

            rectNext_border = rnd.Next(rect_maxWidth + (int)rectHero.Width,
                                       ClientSize.Width - shift - (int)rectNext.Width);
        }

        private void CheckLose()
        {
            if (stickMain.X >= rectNext.X && stickMain.X <= rectNext.X + rectNext.Width)
            {
                if (stickMain.X >= stickBonus.X0 && stickMain.X <= stickBonus.X)
                    score++;

                status = GameState.nextLevel;
            }
            else
                status = GameState.lose;
        }

        private void CheckBestScore()
        {
            if (Properties.Settings.Default.score_best >= score) return;

            Properties.Settings.Default.score_best = score;
            Properties.Settings.Default.Save();
        }


        private void StartScreenLabelVisible(bool on)
        {
            labelName.Visible = on;
            labelPlay.Visible = on;
        }

        private void FinalScreenLabelVisible(bool on)
        {
            labelRestart.Visible = on;
            labelStartScreen.Visible = on;
            labelInfoDevelopers.Visible = on;
            labelSite.Visible = on;
        }

        //========== Logical Part 2 ==========//

        private void Start()
        {                  
            isPrintInfoDevelopers = false;
            FinalScreenLabelVisible(false);
            StartScreenLabelVisible(true);

            timer.Interval = 90;
            alpha = 0;
            score = 0;
            stickMain_height = 0;
            stickMain_border = 0;

            rectPrev = new RectangleF();
            rectMain = new RectangleF((ClientSize.Width - rect_maxWidth) / 2,
                                       rect_maxHeight + 3 * shift,
                                       rect_maxWidth,
                                       rect_maxHeight);
            rectHero = new RectangleF(rectMain.X + (rectMain.Width - 20) / 2, rectMain.Y - 23, 20, 23);
            GenerateRectNext();

            rectMain_border = (rect_maxWidth - rectMain.Width)/2;

            stickPrev = new Line();
            stickMain = new Line();
            stickBonus = new Line(rectNext.X + (rectNext.Width - pen_bonus.Width) / 2,
                                  rectNext.Y + pen_bonus.Width / 2,
                                  rectNext.X + (rectNext.Width + pen_bonus.Width) / 2,
                                  rectNext.Y + pen_bonus.Width / 2);

            status = GameState.startScreen;
            timer.Start();
        }

        private void Restart()
        {
            isPrintInfoDevelopers = false;
            FinalScreenLabelVisible(false);

            alpha = 0;
            score = 0;

            GenerateRectNext();

            stickMain = stickPrev;
            stickPrev = new Line();

            status = GameState.start;
            timer.Start();
        }
    }
}
