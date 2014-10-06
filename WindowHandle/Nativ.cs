using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AWC.WindowHandle
{
    public class Nativ
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        private static extern IntPtr GetWindowLongPtr32(HandleRef hWnd, Enums.WindowLongFlags nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
        private static extern IntPtr GetWindowLongPtr64(HandleRef hWnd, Enums.WindowLongFlags nIndex);

        public static IntPtr GetWindowLongPtr(HandleRef hWnd, Enums.WindowLongFlags nIndex)
        {
            if (IntPtr.Size == 8)
            {
                return GetWindowLongPtr64(hWnd, nIndex);
            } else
            {
                return GetWindowLongPtr32(hWnd, nIndex);
            }
        }

        [DllImport("user32.dll", CharSet=CharSet.None)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(HandleRef hWnd, out NativStructs.RECT lpRect);

        [DllImport("dwmapi.dll", PreserveSig = true)]
        public static extern int DwmUpdateThumbnailProperties(IntPtr hThumbnail, ref AWC.WindowHandle.NativStructs.ThumbnailProperties props);

        [DllImport("dwmapi.dll", CharSet=CharSet.None)]
        public static extern int DwmUnregisterThumbnail(IntPtr iprThumb);

        [DllImport("dwmapi.dll", CharSet=CharSet.None)]
        public static extern int DwmRegisterThumbnail(IntPtr iprDest, IntPtr iprSrc, ref IntPtr iprThumb);

        [DllImport("dwmapi.dll", CharSet=CharSet.None)]
        public static extern int DwmQueryThumbnailSourceSize(IntPtr iprThumbnail, ref System.Drawing.Size _size);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern Boolean SetWindowPos(IntPtr iprHWND, IntPtr iprInsertAfter, int iPosX, int iPosY, int iSizeX, int iSizeY, int iUFlags);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, [MarshalAs(UnmanagedType.I4)] Enums.WindowLongFlags nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, [MarshalAs(UnmanagedType.I4)] Enums.WindowLongFlags nIndex, IntPtr dwNewLong);

        public static IntPtr SetWindowLongPtr(IntPtr hWnd, Enums.WindowLongFlags nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
            {
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            } else
            {
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
            }
        }


          /*<DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function ShowWindow(ByVal hwnd As IntPtr, ByVal nCmdShow As cWindowHandleFlags.ShowWindowCommands) As Boolean
    End Function*/

        [DllImport("user32.dll", SetLastError=true, CharSet=CharSet.Auto)]
        public static extern Boolean ShowWindow(IntPtr hWnd, Enums.ShowWindowCommands nCmdShow);


    }
}
