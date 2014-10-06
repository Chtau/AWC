using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWC.Public
{
    public class pEventArgs
    {

    }

    public class ProcessEventArgs : EventArgs
    {
        private WindowHandle.Window myWindow;

        public WindowHandle.Window Window
        {
            get { return myWindow; }
        }

        public ProcessEventArgs(WindowHandle.Window myWin)
        {
            myWindow = myWin;
        }
    }
}
