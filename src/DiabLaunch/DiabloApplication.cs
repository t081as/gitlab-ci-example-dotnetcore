using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DiabLaunch
{
    class DiabloApplication
    {
        /// <summary>
        /// The path and filename of the Diablo 2 executable.
        /// </summary>
        private string executable;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiabloApplication"/> class.
        /// </summary>
        public DiabloApplication()
        {
            var registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            var registryPath = registryKey.OpenSubKey(@"Software\Blizzard Entertainment\Diablo II");

            if (registryPath == null)
            {
                throw new InvalidOperationException("Unable to open Diablo 2 registry path");
            }

            var registrySetting = registryPath.GetValue("GamePath")?.ToString();

            if (registrySetting == null)
            {
                throw new InvalidOperationException("Unable to open Diablo 2 registry value");
            }

            this.executable = registrySetting.ToString();
        }

        /// <summary>
        /// Launches Diablo 2, removes the window border and sets the window to full screen mode.
        /// </summary>
        public void LaunchNoBorderFullScreen()
        {
            var diabloLaunchProcess = new Process();
            diabloLaunchProcess.StartInfo.FileName = this.executable;
            diabloLaunchProcess.StartInfo.Arguments = "-nofixaspect -w";
            diabloLaunchProcess.StartInfo.UseShellExecute = false;
            diabloLaunchProcess.StartInfo.WorkingDirectory = Path.GetDirectoryName(this.executable);

            diabloLaunchProcess.Start();

            Process diabloGameProcess = null;

            while (diabloGameProcess == null || diabloGameProcess.MainWindowHandle.ToInt32() == 0)
            {
                diabloGameProcess = Process.GetProcessesByName("Game").FirstOrDefault();
            }

            DiabloWindow diabloWindow = new DiabloWindow(diabloGameProcess.MainWindowHandle);
            diabloWindow.RemoveBorder();
            diabloWindow.SetPosition(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }
    }
}
