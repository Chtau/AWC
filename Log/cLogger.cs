using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace AWC.Log
{
    class cLogger
    {
        private static string tmpLastText;
        private static string myLogText;
        private static RichTextBox myDebugTextControl;

        public static string GetLogText()
        {
            return myLogText;
        }

        public static bool CreateDebugLogFile(string strPath)
        {
            try
            {
                System.IO.File.WriteAllText(strPath, myLogText);

                return true;
            } catch (Exception ex)
            {
                Log(ex);
                return false;
            }
        }

        public static void SetDebugTextControl(Control ctrl)
        {
            if (myDebugTextControl == null)
            {
                RichTextBox mrtb = ctrl as RichTextBox;
                if (mrtb != null)
                {
                    myDebugTextControl = mrtb;
                }
            } else
            {
                throw new ArgumentException("RichtTextBox for Logger already set");
            }
        }

        private delegate void StringCallback(string str);

        private static void MyLog(string str)
        {
            if (tmpLastText != str)
            {
                if (myDebugTextControl != null && !(myDebugTextControl.Disposing) && !(myDebugTextControl.IsDisposed))
                {
                    if (myDebugTextControl.InvokeRequired && myDebugTextControl.IsHandleCreated)
                    {
                        myDebugTextControl.BeginInvoke(new StringCallback(MyLog), str);
                    }
                    else
                    {
                        Debug.Print(str);
                        myLogText += "\r" + str;
                        if (!string.IsNullOrEmpty(myDebugTextControl.Text))
                        {
                            myDebugTextControl.Text += "\r" + str;
                        }
                        else
                        {
                            myDebugTextControl.Text += str;
                        }
                        myDebugTextControl.SelectionStart = myDebugTextControl.Text.Length;
                        myDebugTextControl.ScrollToCaret();

                        tmpLastText = str;
                    }
                }
            } else
            {
                Debug.Print("Same Log text as last log Text:'" + str + "'");
            }
        }

        public static void Log(Exception ex)
        {
            if (ex != null)
            {
                MyLog(ex.Message + "\r" + ex.StackTrace);
            }
        }

        public static void Log(Exception ex, string txt)
        {
            if (ex != null)
            {
                MyLog(txt + "\r" + ex.Message + "\r" + ex.StackTrace);
            }
        }

        public static void Log(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                MyLog(str);
            }
        }

    }
}
