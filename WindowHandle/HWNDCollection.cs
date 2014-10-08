using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AWC.WindowHandle
{
    public class HWNDCollection :IDisposable    
    {
        private List<Window> myLHWND;
        private bool _Dispose = false;

        public List<Window> LHWND
        {
            get
            {
                if (myLHWND == null)
                    myLHWND = new List<Window>();
                return myLHWND;
            }
            set
            {
                myLHWND = value;
            }
        }

        public HWNDCollection()
        {

        }

        public void Load()
        {
            if (myLHWND == null)
                myLHWND = new List<Window>();

            if (myLHWND.Count > 0)
                myLHWND.Clear();

            foreach (Process iPrc in Process.GetProcesses())
            {
                try
                {
                    if (iPrc.MainWindowHandle.ToInt32() > 0)
                    {
                        myLHWND.Add(new Window(iPrc));
                    }
                }
                catch (Exception)
                {
                    if (iPrc != null)
                        iPrc.Dispose();
                }
            }
        }

        public Window FindByString(string strWindowTitle) 
        {
            try
            {
                if (myLHWND == null)
                    myLHWND = new List<Window>();

                foreach (Window iHwnd in myLHWND)
                {
                    if (iHwnd.Processname == strWindowTitle || iHwnd.Title == strWindowTitle)
                    {
                        return iHwnd;
                    }
                }
                return null;
            } catch(Exception)
            {
                return null;
            }
        }

        public Window FindByIntPtr(IntPtr mHandle) 
        {
            try
            {
                if (myLHWND == null)
                    myLHWND = new List<Window>();

                foreach (Window iHwnd in myLHWND)
                {
                    if (iHwnd.Handle == mHandle)
                        return iHwnd;
                }
                return null;
            } catch (Exception)
            {
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(!this._Dispose);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            if (bDisposing)
            {
                if (myLHWND != null)
                {
                    myLHWND.Clear();
                    myLHWND = null;
                }

                this._Dispose = true;
            }
        }
    }
}
