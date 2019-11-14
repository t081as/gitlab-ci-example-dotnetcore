using System;
using System.Windows.Forms;

namespace DiabLaunch
{
    /// <summary>
    /// Contains the main entry point for the application.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                return;
            }

            if (!Environment.Is64BitProcess)
            {
                return;
            }

            DiabloApplication application = null;

            try
            {
                application = new DiabloApplication();
            }
            catch (InvalidOperationException iex)
            {
                MessageBox.Show($"Unable to locate Diablo 2 installation directory using the system registry ({iex.Message})", "DiabLaunch");
                return;
            }

            application.LaunchNoBorderFullScreen();
        }
    }
}