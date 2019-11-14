using System;

namespace DiabLaunch
{
    /// <summary>
    /// Proviees methods to work with the native Diablo 2 application window.
    /// </summary>
    class DiabloWindow
    {
        const int GWL_STYLE = -16;

        const uint WS_BORDER = 0x00800000;
        const uint WS_DLGFRAME = 0x00400000;
        const uint WS_THICKFRAME = 0x00040000;
        const uint WS_CAPTION = WS_BORDER | WS_DLGFRAME;
        const uint WS_MINIMIZE = 0x20000000;
        const uint WS_MAXIMIZE = 0x01000000;
        const uint WS_SYSMENU = 0x00080000;

        const uint SWP_FRAMECHANGED = 0x0020;
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOMOVE = 0x0002;

        /// <summary>
        /// The handle of the diablo window represented by this instance.
        /// </summary>
        private IntPtr handle;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiabloWindow"/> class with the given <paramref name="handle"/>.
        /// </summary>
        /// <param name="handle">The handle of the diablo window.</param>
        public DiabloWindow(IntPtr handle)
        {
            this.handle = handle;
        }

        /// <summary>
        /// Removes the border of the window.
        /// </summary>
        public void RemoveBorder()
        {
            uint currentStyle = (uint)NativeMethods.GetWindowLong(this.handle, GWL_STYLE).ToInt64();
            uint[] stylesToRemove = { WS_CAPTION, WS_THICKFRAME, WS_MINIMIZE, WS_MAXIMIZE, WS_SYSMENU };

            foreach (var styleToRemove in stylesToRemove)
            {
                if ((currentStyle & styleToRemove) != 0)
                {
                    currentStyle &= ~styleToRemove;
                }
            }

            NativeMethods.SetWindowLongPtr(this.handle, GWL_STYLE, (IntPtr)currentStyle);
            NativeMethods.SetWindowPos(this.handle, (IntPtr)0, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_FRAMECHANGED);
        }

        /// <summary>
        /// Sets the position and size of the window.
        /// </summary>
        /// <param name="x">The desired x coordinate.</param>
        /// <param name="y">The desired y coordinate.</param>
        /// <param name="width">The desired with</param>
        /// <param name="height">The desired height.</param>
        public void SetPosition(int x, int y, int width, int height)
        {
            NativeMethods.SetWindowPos(this.handle, (IntPtr)0, x, y, width, height, 0);
        }
    }
}