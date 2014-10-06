using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWC.WindowHandle
{
    public class Enums
    {

        [Flags]
        public enum WindowPosition:short
        {
            NoSize = 0x1,
            NoMove = 0x2,
            TopMost = 0x200
        }


        [Flags]
        public enum WindowStyles:uint
        {
            BORDER = 0x800000,
            CAPTION = 0xc00000,
            CHILD = 0x40000000,
            CLIPCHILDREN = 0x2000000,
            CLIPSIBLINGS = 0x4000000,
            DISABLED = 0x8000000,
            DLGFRAME = 0x400000,
            GROUP = 0x20000,
            HSCROLL = 0x100000,
            MAXIMIZE = 0x1000000,
            MAXIMIZEBOX = 0x10000,
            MINIMIZE = 0x20000000,
            MINIMIZEBOX = 0x20000,
            OVERLAPPED = 0x0,
            OVERLAPPEDWINDOW = OVERLAPPED | CAPTION | SYSMENU | SIZEFRAME | MINIMIZEBOX | MAXIMIZEBOX,
            POPUP = 0x80000000u,
            POPUPWINDOW = POPUP | BORDER | SYSMENU,
            SIZEFRAME = 0x40000,
            SYSMENU = 0x80000,
            TABSTOP = 0x10000,
            VISIBLE = 0x10000000,
            VSCROLL = 0x200000
        }

        public enum WindowExStyle:uint
        {
            RIGHTSCROLLBAR = 0,
            LTRREADING = 0,
            LEFT = 0,
            DLGMODALFRAME = 1,
            NOPARENTNOTIFY = 4,
            TOPMOST = 8,
            ACCEPTFILES = 16,
            TRANSPARENT = 32,
            MDICHILD = 64,
            TOOLWINDOW = 128,
            WINDOWEDGE = 256,
            PALETTEWINDOW = 392,
            CLIENTEDGE = 512,
            OVERLAPPEDWINDOW = 768,
            CONTEXTHELP = 1024,
            RIGHT = 4096,
            RTLREADING = 8192,
            LEFTSCROLLBAR = 16384,
            CONTROLPARENT = 65536,
            STATICEDGE = 131072,
            APPWINDOW = 262144,
            LAYERED = 524288,
            NOINHERITLAYOUT = 1048576,
            LAYOUTRTL = 4194304,
            COMPOSITED = 33554432,
            NOACTIVATE = 134217728
        }

        public enum WindowLongFlags
        {
            GWL_USERDATA = (-21),
            GWL_EXSTYLE = (-20),
            GWL_STYLE = (-16),
            GWL_ID = (-12),
            GWLP_HWNDPARENT = (-8),
            GWLP_HINSTANCE = (-6),
            GWL_WNDPROC = (-4),
            DWLP_MSGRESULT = (0),
            DWLP_DLGPROC = (4),
            DWLP_USER = (8)
        }

        [Flags]
        public enum WindowLogFlags:int
        {
            INFOLOG = 1,
            ERRORLOG = 2,
            WINDOWMESSAGE = 3
        }

        public enum ShowWindowCommands:int
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            Hide = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or  maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window  for the first time.
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            Maximize = 3,
            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>
            ShowMaximized = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value is similar to see cref="Win32.ShowWindowCommand.Normal", except  the window is not actived.
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position. 
            /// </summary>
            Show = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level window in the Z order.
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to see cref="Win32.ShowWindowCommand.ShowMinimized", except the window is not activated.
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is  similar to see cref="Win32.ShowWindowCommand.Show", except the  window is not activated.
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// Activates and displays the window. If the window is minimized or  maximized, the system restores it to its original size and position.  An application should specify this flag when restoring a minimized window.
            /// </summary>
            Restore = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the  STARTUPINFO structure passed to the CreateProcess function by the  program that started the application.
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            /// Windows 2000/XP: Minimizes a window, even if the thread that owns the window is not responding. This flag should only be  used when minimizing windows from a different thread.
            /// </summary>
            ForceMinimize = 11
        }

    }
}
