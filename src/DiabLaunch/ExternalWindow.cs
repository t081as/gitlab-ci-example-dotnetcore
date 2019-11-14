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
using System.Windows.Forms;

namespace DiabLaunch
{
    /// <summary>
    /// Proviees methods to work with an external native window.
    /// </summary>
    public class ExternalWindow : IWin32Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExternalWindow"/> class with the given <paramref name="handle"/>.
        /// </summary>
        /// <param name="handle">The handle of the external window.</param>
        public ExternalWindow(IntPtr handle)
        {
            this.Handle = handle;
        }

        /// <inheritdoc/>
        public IntPtr Handle { get; }

        /// <summary>
        /// Removes the border of the external window.
        /// </summary>
        public void RemoveBorder()
        {
            uint currentStyle = (uint)NativeMethods.GetWindowLong(this.Handle, NativeConstants.GWL_STYLE).ToInt64();
            uint[] stylesToRemove = { NativeConstants.WS_CAPTION, NativeConstants.WS_THICKFRAME, NativeConstants.WS_MINIMIZE, NativeConstants.WS_MAXIMIZE, NativeConstants.WS_SYSMENU };

            foreach (var styleToRemove in stylesToRemove)
            {
                if ((currentStyle & styleToRemove) != 0)
                {
                    currentStyle &= ~styleToRemove;
                }
            }

            NativeMethods.SetWindowLongPtr(this.Handle, NativeConstants.GWL_STYLE, (IntPtr)currentStyle);
            NativeMethods.SetWindowPos(this.Handle, (IntPtr)0, 0, 0, 0, 0, NativeConstants.SWP_NOMOVE | NativeConstants.SWP_NOSIZE | NativeConstants.SWP_FRAMECHANGED);
        }

        /// <summary>
        /// Sets the position and size of the window.
        /// </summary>
        /// <param name="x">The desired x coordinate.</param>
        /// <param name="y">The desired y coordinate.</param>
        /// <param name="width">The desired with.</param>
        /// <param name="height">The desired height.</param>
        public void SetPosition(int x, int y, int width, int height)
        {
            NativeMethods.SetWindowPos(this.Handle, (IntPtr)0, x, y, width, height, 0);
        }
    }
}