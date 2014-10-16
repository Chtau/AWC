using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AWC.Log
{
    public class cLogger
    {
        private static string tmpLastText;
        private static string myLogText;
        private static RichTextBox myDebugTextControl;
        private static Dictionary<LogType, List<string>> myDicLogText;

        public enum LogType
        {
            Default = 0,
            Warning = 1,
            Error = 2,
            Process = 3,
            Message = 4
        }

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

        public static bool CreateDebugLogFile(string strPath, bool bResetLogText)
        {
            try
            {
                CreateDebugLogFile(strPath);
                if (bResetLogText)
                    myLogText = string.Empty;

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

        private delegate void StringCallback(string _str, LogType _lt);

        private static void MyLog(string str, LogType lt)
        {
            if (tmpLastText != str)
            {
                if (myDebugTextControl != null && !(myDebugTextControl.Disposing) && !(myDebugTextControl.IsDisposed))
                {
                    if (myDebugTextControl.InvokeRequired && myDebugTextControl.IsHandleCreated)
                    {
                        myDebugTextControl.BeginInvoke(new StringCallback(MyLog), str, lt);
                    }
                    else
                    {
                        if (myDicLogText == null)
                        {
                            myDicLogText = new Dictionary<LogType, List<string>>();
                        }

                        if (!myDicLogText.ContainsKey(lt))
                        {
                            myDicLogText.Add(lt, new List<string>());
                        }
                        myDicLogText[lt].Add(str);


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
                Log(ex, LogType.Default);
            }
        }

        public static void Log(Exception ex, LogType lt)
        {
            if (ex != null)
            {
                MyLog(ex.Message + "\r" + ex.StackTrace, lt);
            }
        }

        public static void Log(Exception ex, string txt)
        {
            if (ex != null)
            {
                Log(ex, txt, LogType.Default);
            }
        }

        public static void Log(Exception ex, string txt, LogType lt)
        {
            if (ex != null)
            {
                MyLog(txt + "\r" + ex.Message + "\r" + ex.StackTrace, lt);
            }
        }

        public static void Log(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                MyLog(str, LogType.Default);
            }
        }

        public static void Log(string str, LogType lt)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Log(str, lt);
            }
        }

    }
}
