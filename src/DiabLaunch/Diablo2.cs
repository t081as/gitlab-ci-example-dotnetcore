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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.Win32;

namespace DiabLaunch
{
    /// <summary>
    /// Provides methods to work with the game <c>Diablo 2</c>.
    /// </summary>
    public sealed class Diablo2
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Diablo2"/> class.
        /// </summary>
        public Diablo2()
        {
            string localPath = this.DetectGameFileSystem();

            if (localPath != null)
            {
                this.GamePath = localPath;
            }
            else
            {
                string registryPath = this.DetectGameRegistry();

                if (registryPath != null)
                {
                    this.GamePath = registryPath;
                }
                else
                {
                    throw new FileNotFoundException("Diablo 2 could not be located on this machine", "Diablo II.exe");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Diablo2"/> class with the given <paramref name="fileName"/>.
        /// </summary>
        /// <param name="fileName">The path and filename of the Diablo 2 executable.</param>
        public Diablo2(string fileName)
        {
            this.GamePath = fileName;
        }

        /// <summary>
        /// Gets the path and filename of the game executable.
        /// </summary>
        public string GamePath { get; }

        /// <summary>
        /// Gets the path of the game executable.
        /// </summary>
        public string InstallPath => Path.GetDirectoryName(this.GamePath);

        /// <summary>
        /// Launches the game with the given command line arguments and returns the handle of the game window.
        /// </summary>
        /// <param name="args">The command line arguments that shall be passed to the game.</param>
        /// <returns>The handle of the game window.</returns>
        public IntPtr Launch(string[] args)
        {
            using (var diabloLaunchProcess = new Process())
            {
                diabloLaunchProcess.StartInfo.FileName = this.GamePath;
                diabloLaunchProcess.StartInfo.Arguments = string.Join(' ', args);
                diabloLaunchProcess.StartInfo.UseShellExecute = false;
                diabloLaunchProcess.StartInfo.WorkingDirectory = this.InstallPath;

                diabloLaunchProcess.Start();
            }

            Process diabloGameProcess = null;
            while (diabloGameProcess == null || diabloGameProcess.MainWindowHandle.ToInt32() == 0)
            {
                diabloGameProcess = Process.GetProcessesByName("Game").FirstOrDefault();
            }

            return diabloGameProcess.MainWindowHandle;
        }

        /// <summary>
        /// Tries to detect the path of the game using the Windows Registry.
        /// </summary>
        /// <returns>The path and filename of the game or <c>null</c> if it could not be detected.</returns>
        private string DetectGameRegistry()
        {
            using var registryKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry32);
            var registryPath = registryKey.OpenSubKey(@"Software\Blizzard Entertainment\Diablo II");

            if (registryPath == null)
            {
                return null;
            }

            var registrySetting = registryPath.GetValue("GamePath")?.ToString();

            if (registrySetting == null)
            {
                return null;
            }

            return registrySetting.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Tries to detect the path of the game using the local file system.
        /// </summary>
        /// <returns>The path and filename of the game or <c>null</c> if it could not be detected.</returns>
        private string DetectGameFileSystem()
        {
            string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Diablo II.exe");

            if (File.Exists(fileName))
            {
                return fileName;
            }
            else
            {
                return null;
            }
        }
    }
}
