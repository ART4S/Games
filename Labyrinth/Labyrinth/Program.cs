﻿using System.Windows.Forms;

namespace Labyrinth
{
    static class Program
    {
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Game_Form());
        }
    }
}