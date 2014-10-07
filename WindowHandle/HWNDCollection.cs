using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace AWC.WindowHandle
{
    public class HWNDCollection
    {
        private List<Window> myLHWND;

        public List<Window> LHWND
        {
            get
            {
                return myLHWND;
            }
            set
            {
                myLHWND = value;
            }
        }

        public HWNDCollection()
        {
            myLHWND = new List<Window>();
        }

        public void Load()//replace GetList from TA_Window_API
        {
            try
            {
                if (myLHWND.Count > 0)
                    myLHWND.Clear();

                foreach (Process iPrc in Process.GetProcesses())
                {
                    if (iPrc.MainWindowHandle.ToInt32() > 0)
                    {
                        //Window wHWND = new Window(iPrc);
                        myLHWND.Add(new Window(iPrc));
                    }
                }

                GC.Collect();
            } catch (Exception)
            { }
        }

        public Window FindByString(string strWindowTitle) //replace FindWindowInList from TA_Window_API
        {
            try
            {
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

        public Window FindByIntPtr(IntPtr mHandle) //replace FindWindowInListByItrPtr from TA_Window_API
        {
            try
            {
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
    }
}
