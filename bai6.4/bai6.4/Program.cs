using System;
using System.Windows.Forms;
using GarageManagement.GUI;

namespace GarageManagement
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }
    }
}
