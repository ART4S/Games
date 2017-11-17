using System;
using System.Windows.Forms;

namespace AirForce
{
    public static class Program
    {
        public static readonly Random Random = new Random();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
