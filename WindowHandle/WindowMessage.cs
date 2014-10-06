using AWC.WindowHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace WindowHandle
{
    //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    class WindowMessage : NativeWindow
    {
        public delegate void WindowLogText(object sender, LogEventArgs e);
        public event WindowLogText WindowDataTextChanged;

        public WindowMessage(IntPtr handle, IntPtr myFromHandle)
        {           
            Nativ.SetParent(myFromHandle, handle);

            AssignHandle(handle);
            //Application.AddMessageFilter(this);
        }

        internal void OnHandleCreated(object sender, EventArgs e)
        {
            // Window is now created, assign handle to NativeWindow.
            AssignHandle(this.Handle);
        }
        internal void OnHandleDestroyed(object sender, EventArgs e)
        {
            // Window was destroyed, release hook.
            ReleaseHandle();
        }

       /* public bool PreFilterMessage(ref Message m)
        {
            if (m != null)
            {
                 if (m.Msg >= 513 && m.Msg <= 515)
                 {
                     //WriteOutput("Left mouse button is pressed", Enums.WindowLogFlags.WINDOWMESSAGE);
                 }
            } 
            else
            {
             
            }
            return false;
        }*/

        [SecurityPermissionAttribute(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        [SecurityPermissionAttribute(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg >= 513 && m.Msg <= 515)
            {
                WriteOutput("Left mouse button is pressed get from WndProc", Enums.WindowLogFlags.WINDOWMESSAGE);
            }
        }

        private void WriteOutput(string strText, Enums.WindowLogFlags e)
        {
            try
            {
                string strInfoText = strText + Environment.NewLine;
                WindowDataTextChanged(this, new LogEventArgs(strText, e));
                /*switch (e)
                {
                    case Enums.WindowLogFlags.ERRORLOG:
                        myWindowErrorText += strInfoText;
                        break;
                    default:
                        //Infolog
                        myWindowInfoText += strInfoText;
                        break;
                }*/
            }
            catch (Exception)
            {
            }
        }
    }

    public class externThreadCaller: MarshalByRefObject
    {
        public void Test()
        {

        }
    }
}
