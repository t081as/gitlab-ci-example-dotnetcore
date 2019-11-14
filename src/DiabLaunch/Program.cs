// DiabLaunch - Diablo 2 full screen launcher
// Copyright (C) 2018-2019  Tobias Koch
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program. If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Windows.Forms;

namespace DiabLaunch
{
    /// <summary>
    /// Contains the main entry point for the application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            {
                MessageBox.Show("DiabLaunch requires Microsoft Windows", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!Environment.Is64BitProcess)
            {
                MessageBox.Show("DiabLaunch requires a x64 processor", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Diablo2 diabloGame;

            try
            {
                diabloGame = new Diablo2();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("DiabLaunch is unable to detect the Diablo 2 directory.\nMake sure that Diablo 2 is installed correctly or copy this application into your Diablo 2 directory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IntPtr gameWindowHandle = diabloGame.Launch(new string[] { "-w", "-nofixaspect" });

            ExternalWindow gameWindow = new ExternalWindow(gameWindowHandle);
            gameWindow.RemoveBorder();
            gameWindow.SetPosition(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }
    }
}