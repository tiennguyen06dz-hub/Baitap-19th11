using System;
using System.Windows.Forms;
using KaraokeManagement.GUI;

namespace KaraokeManagement
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
            ApplicationConfiguration.Initialize();
            Application.Run(new FormKaraokeMain());
        }
    }
}
