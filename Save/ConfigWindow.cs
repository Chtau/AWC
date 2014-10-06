using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWC.Save
{
    public class ConfigWindow
    {
        private System.Drawing.Rectangle myRec;
        private string myProcessName;

        public string ProcessName
        {
            get
            {
                return myProcessName;
            }
        }

        public System.Drawing.Rectangle WindowRectangle
        {
            get
            {
                return myRec;
            }
            set
            {
                myRec = value;
            }
        }

        public ConfigWindow(string _ProcessName)
        {
            if (string.IsNullOrEmpty(_ProcessName))
            {
                throw new ArgumentNullException("_ProcessName");
            }
            else
            {
                myProcessName = _ProcessName;
            }
        }

        public void SaveToFile()
        {
            throw new NotImplementedException("SaveToFile");
        }
    }
}
