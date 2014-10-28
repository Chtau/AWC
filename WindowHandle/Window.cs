using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using WindowHandle;

namespace AWC.WindowHandle
{
    public class Window: IDisposable
    {
        private bool _Dispose = false;
        private bool _ProcessEndSend = false;

        public delegate void WindowLogText(object sender, LogEventArgs e);
        public event WindowLogText WindowDataTextChanged;
        public event EventHandler<Public.ProcessEventArgs> WindowPositionSizeChanged;
        public event EventHandler<Public.ProcessEventArgs> WindowStyleChanged;
        public event EventHandler<Public.ProcessEventArgs> WindowExStyleChanged;
        public event EventHandler<Public.ProcessEventArgs> WindowProcessExit;
        public event EventHandler<Public.ProcessEventArgs> WindowBasicChanged;
        public event EventHandler<Public.ProcessEventArgs> WindowTitleChanged;

        private Save.ConfigWindow myConfigWindow;
        private string myWindowTitle;
        private IntPtr myHandle;
        private Process myWindowProcess;
        private string myWindowProcessName;
        private bool myHaveWindow;
        private List<Enums.WindowStyles> myWindowStyleList;
        private List<Enums.WindowExStyle> myWindowExStyleList;
        private Thread myWindowDataRefreshThread;
        private string myWindowErrorText;
        private string myWindowInfoText;
        private int myWindowDataSleepTimebeforRefresh = 50;
        private System.Drawing.Rectangle myWindowRectangle;
        private string myProcessPath = "";
        private DateTime myProcessStartTime = DateTime.MinValue;
        private TimeSpan myProcessRuntime = TimeSpan.Zero;
        private int myWindowProcessID;


        public DateTime StartTime
        {
            get
            {
                if (myProcessStartTime == DateTime.MinValue)
                    GetTimes();
                return myProcessStartTime;
            }
        }

        public TimeSpan Runtime
        {
            get
            {
                if (myProcessRuntime == TimeSpan.Zero)
                    GetTimes();
                return myProcessRuntime;
            }
        }

        public string ProcessPath
        {
            get
            {
                if (string.IsNullOrEmpty(myProcessPath))
                    myProcessPath = GetProcessPath();
                return myProcessPath;
            }
        }

        public string Title
        {
            get 
            {
                if (string.IsNullOrEmpty(myWindowTitle))
                    myWindowTitle = "No Title Name Found (Process: " + myWindowProcessName +" )" ;
                return myWindowTitle; 
            }
        }

        public IntPtr Handle
        {
            get { return myHandle; }
        }

        public Process Process
        {
            get { return myWindowProcess; }
        }

        public string Processname
        {
            get { return myWindowProcessName; }
        }

        public bool HaveWindow
        {
            get { return myHaveWindow; }
        }

        public string Errortext
        {
            get { return myWindowErrorText; }
        }

        public string Infotext
        {
            get { return myWindowInfoText; }
        }

        public System.Drawing.Rectangle Rectangle
        {
            get 
            {
                if (myWindowRectangle == null || myWindowRectangle.IsEmpty)
                {
                    GetPositionSize();
                }
                return myWindowRectangle; 
            }
        }

        public List<Enums.WindowStyles> Stylelist
        {
            get
            {
                if (myWindowStyleList == null)
                {
                    GetStyles();
                }
                return myWindowStyleList;
            }
        }

        public List<Enums.WindowExStyle> ExStylelist
        {
            get
            {
                if (myWindowExStyleList == null)
                {
                    GetExStyle();
                }
                return myWindowExStyleList;
            }
        }

        public bool IsTopMost
        {
            get
            {
                if (ExStylelist != null)
                {
                    if (ExStylelist.Contains(Enums.WindowExStyle.TOPMOST))
                        return true;
                    else
                        return false;
                } else
                {
                    return false;
                }
            }
        }

        public Window(Process prc)
        {
            if (prc != null)
            {
                if (prc.MainWindowHandle != IntPtr.Zero)
                {
                    myHaveWindow = true;
                    myHandle = prc.MainWindowHandle;
                    myWindowTitle = prc.MainWindowTitle;
                    myWindowProcessName = prc.ProcessName;
                    myWindowProcessID = prc.Id;
                    myConfigWindow = new Save.ConfigWindow(myWindowProcessName);
                    myWindowProcess =  prc;
                    try
                    {
                        myWindowProcess.EnableRaisingEvents = true;
                    }
                    catch (System.ComponentModel.Win32Exception scex) 
                    {
                        if (scex.ErrorCode != -2147467259)
                        {
                            System.Diagnostics.Debug.Print(string.Format("Process can't raise events Processname:'{0}' Ex:'{1}'", myWindowProcessName, scex.Message));
                        } 
                    }
                    myWindowProcess.Disposed += myWindowProcess_Disposed;
                    myWindowProcess.ErrorDataReceived += myWindowProcess_ErrorDataReceived;
                    myWindowProcess.Exited += myWindowProcess_Exited;
                    myWindowProcess.OutputDataReceived += myWindowProcess_OutputDataReceived;
                } else
                {
                    myHaveWindow = false;
                }
            }
        }

#region Process Events
        void myWindowProcess_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            WriteOutput(string.Format("Window Process outputdata received Data:'{0}';", e.Data), Enums.WindowLogFlags.INFOLOG);
        }

        void myWindowProcess_Exited(object sender, EventArgs e)
        {
            ProcessEnds();
        }

        void myWindowProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            WriteOutput(string.Format("Window Process errordata received Data:'{0}';", e.Data), Enums.WindowLogFlags.ERRORLOG);
        }

        void myWindowProcess_Disposed(object sender, EventArgs e)
        {
            ProcessEnds();
        }
#endregion

        private void ProcessEnds()
        {
            if (!_ProcessEndSend)
            {
                if (WindowProcessExit != null)
                {
                    OnWindowProcessExit(new Public.ProcessEventArgs(this));
                    _ProcessEndSend = true;
                }

                WriteOutput("Window Process has end;", Enums.WindowLogFlags.INFOLOG);
            }
        }

        protected virtual void OnWindowProcessExit(Public.ProcessEventArgs e)
        {
            if (WindowProcessExit != null)
                WindowProcessExit(this, e);
        }

        private void GetStyles()
        {
            try
            {
                if (myHandle != IntPtr.Zero)
                {
                    List<Enums.WindowStyles> myTempList = new List<Enums.WindowStyles>();

                    System.Runtime.InteropServices.HandleRef handleRef = new System.Runtime.InteropServices.HandleRef(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(new object()), myHandle);
                    double windowLongPtr = (double)(Nativ.GetWindowLongPtr(handleRef, Enums.WindowLongFlags.GWL_STYLE));
                    if (windowLongPtr < 0)
                    {
                        windowLongPtr = -(windowLongPtr * 2);
                    }

                    for (int i = Enum.GetValues(typeof(Enums.WindowStyles)).Length - 1; i > 0; i--)
                    {
                        double EnumValue = (double)(Enum.GetValues(typeof(Enums.WindowStyles)) as Enums.WindowStyles[])[i];

                        if ((windowLongPtr - EnumValue) >= 0)
                        {
                            Enums.WindowStyles val = (Enum.GetValues(typeof(Enums.WindowStyles)) as Enums.WindowStyles[])[i];
                            myTempList.Add(val);
                            windowLongPtr = windowLongPtr - EnumValue;
                        }
                    }

                    if (myWindowStyleList == null)
                    {
                        WriteOutput("Window Style create new List;", Enums.WindowLogFlags.INFOLOG);

                        myWindowStyleList = new List<Enums.WindowStyles>();

                        if (myTempList != null && myTempList.Count > 0)
                        {
                            string strtmp = "( ";
                            foreach (Enums.WindowStyles data in myTempList)
                            {
                                strtmp += data + "  ";
                            }
                            strtmp += ");";
                            WriteOutput("Window Styles in List = " + strtmp, Enums.WindowLogFlags.INFOLOG);
                        }
                    }
                    else
                    {
                        if (myWindowStyleList.Count != myTempList.Count)
                        {
                            WriteOutput("Window Style changed;", Enums.WindowLogFlags.INFOLOG);

                            if (myTempList != null && myTempList.Count > 0)
                            {
                                string strtmp = "( ";
                                foreach (Enums.WindowStyles data in myTempList)
                                {
                                    strtmp += data + "  ";
                                }
                                strtmp += ");";
                                WriteOutput("Window Styles in List = " + strtmp, Enums.WindowLogFlags.INFOLOG);
                            }
                        }
                        myWindowStyleList.Clear();
                    }
                    myWindowStyleList.AddRange(myTempList.ToArray());

                    myTempList.Clear();

                    OnWindowStyleChanged(new Public.ProcessEventArgs(this));
                } else
                {
                    WriteOutput("GetStyles can´t execute, Window Handle is zero;", Enums.WindowLogFlags.ERRORLOG);
                }
            } catch (Exception)
            { }
        }

        protected virtual void OnWindowStyleChanged(Public.ProcessEventArgs e)
        {
            if (WindowStyleChanged != null && !myWindowProcess.HasExited)
                WindowStyleChanged(this, e);
        }

        private void GetExStyle()
        {
            try
            {
                if (myHandle != IntPtr.Zero)
                {
                    List<Enums.WindowExStyle> myTempList = new List<Enums.WindowExStyle>();

                    System.Runtime.InteropServices.HandleRef handleRef = new System.Runtime.InteropServices.HandleRef(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(new object()), myHandle);
                    double windowLongPtr = (double)(Nativ.GetWindowLongPtr(handleRef, Enums.WindowLongFlags.GWL_EXSTYLE));
                    if (windowLongPtr < 0)
                    {
                        windowLongPtr = -(windowLongPtr * 2);
                    }

                    for (int i = Enum.GetValues(typeof(Enums.WindowExStyle)).Length - 1; i > 0; i--)
                    {
                        double EnumValue = (double)(Enum.GetValues(typeof(Enums.WindowExStyle)) as Enums.WindowExStyle[])[i];

                        if ((windowLongPtr - EnumValue) >= 0)
                        {
                            Enums.WindowExStyle val = (Enum.GetValues(typeof(Enums.WindowExStyle)) as Enums.WindowExStyle[])[i];
                            myTempList.Add(val);
                            windowLongPtr = windowLongPtr - EnumValue;
                        }
                    }

                    if (myWindowExStyleList == null)
                    {
                        WriteOutput("Window ExStyle create new List;", Enums.WindowLogFlags.INFOLOG);

                        myWindowExStyleList = new List<Enums.WindowExStyle>();

                        if (myTempList != null && myTempList.Count > 0)
                        {
                            string strtmp = "( ";
                            foreach (Enums.WindowExStyle data in myTempList)
                            {
                                strtmp += data + "  ";
                            }
                            strtmp += ");";
                            WriteOutput("Window ExStyles in List = " + strtmp, Enums.WindowLogFlags.INFOLOG);
                        }
                    }
                    else
                    {
                        if (myWindowExStyleList.Count != myTempList.Count)
                        {
                            WriteOutput("Window ExStyle changed;", Enums.WindowLogFlags.INFOLOG);

                            if (myTempList != null && myTempList.Count > 0)
                            {
                                string strtmp = "( ";
                                foreach (Enums.WindowExStyle data in myTempList)
                                {
                                    strtmp += data + "  ";
                                }
                                strtmp += ");";
                                WriteOutput("Window ExStyles in List = " + strtmp, Enums.WindowLogFlags.INFOLOG);
                            }
                        }
                        myWindowExStyleList.Clear();
                    }
                    myWindowExStyleList.AddRange(myTempList.ToArray());

                    myTempList.Clear();

                    OnWindowExStyleChanged(new Public.ProcessEventArgs(this));
                } else
                {
                    WriteOutput("GetExStyle can´t execute, Window Handle is zero;", Enums.WindowLogFlags.ERRORLOG);
                }
            } catch (Exception)
            { }
        }

        protected virtual void OnWindowExStyleChanged(Public.ProcessEventArgs e)
        {
            if (WindowExStyleChanged != null && !myWindowProcess.HasExited)
                WindowExStyleChanged(this, e);
        }

        private void GetPositionSize()
        {
            if (myHandle != IntPtr.Zero)
            {
                System.Runtime.InteropServices.HandleRef handleRef = new System.Runtime.InteropServices.HandleRef(System.Runtime.CompilerServices.RuntimeHelpers.GetObjectValue(new object()), myHandle);
                NativStructs.RECT mWindRECT;
                Nativ.GetWindowRect(handleRef, out mWindRECT);
                if (mWindRECT != null)
                {
                    mWindRECT.X = mWindRECT.X + 8;
                    mWindRECT.Y = mWindRECT.Y + 8;
                    mWindRECT.Height = mWindRECT.Height;
                    mWindRECT.Width = mWindRECT.Width;

                    if (myWindowRectangle == null)
                    {
                        myWindowRectangle = new System.Drawing.Rectangle();
                    }
                    if (mWindRECT.X != myWindowRectangle.X || mWindRECT.Y != myWindowRectangle.Y || mWindRECT.Size != myWindowRectangle.Size || mWindRECT.Location != myWindowRectangle.Location)
                    {
                        WriteOutput(string.Format("Set new Position or Size for Window (new X:'{0}' Y:'{1}' Height:'{2}' Width:'{3}');", mWindRECT.X, mWindRECT.Y, mWindRECT.Height, mWindRECT.Width), Enums.WindowLogFlags.INFOLOG);
                        myWindowRectangle.Height = mWindRECT.Height;
                        myWindowRectangle.Location = mWindRECT.Location;
                        myWindowRectangle.Size = mWindRECT.Size;
                        myWindowRectangle.Width = mWindRECT.Width;
                        myWindowRectangle.X = mWindRECT.X;
                        myWindowRectangle.Y = mWindRECT.Y;

                        OnWindowPositionSizeChanged(new Public.ProcessEventArgs(this));
                    }
                }
            } else
            {
                WriteOutput("GetPositionSize can´t execute, Window Handle is zero;", Enums.WindowLogFlags.ERRORLOG);
            }
        }

        protected virtual void OnWindowPositionSizeChanged(Public.ProcessEventArgs e)
        {
            if (WindowPositionSizeChanged != null)
                WindowPositionSizeChanged(this, e);
        }

        private string GetProcessPath()
        {
            if (myWindowProcess != null)
            {
                try
                {
                    if (myWindowProcess.MainModule != null && myWindowProcess.MainModule.FileName != null)
                    {
                        //Get Path from Process
                        return myWindowProcess.MainModule.FileName;
                    }
                } catch (Exception)
                {
                    //try get path from WMI
                    List<List<string>> lPrcWMI = WMI.WMI.ProcessList(myWindowProcess.Id);
                    if (lPrcWMI != null)
                    {
                        for (int i = 0; i < lPrcWMI[0].Count; i++)
                        {
                            if (lPrcWMI[0][i].StartsWith("ExecutablePath"))
                            {
                                //lProcInfo(0).Item(i).Split(";;")(1)
                                return lPrcWMI[0][i].Split(new string[] { ";;" }, StringSplitOptions.None)[1];
                            }
                        }
                        return "";
                    } else
                    {
                        return "";
                    }
                }
            }
            return "";
        }

        private void GetTimes()
        {
            if (myWindowProcess != null && !myWindowProcess.HasExited)
            {
                if (myProcessStartTime == DateTime.MinValue)
                    myProcessStartTime = myWindowProcess.StartTime;
                if (myProcessStartTime != DateTime.MinValue)
                    myProcessRuntime = new TimeSpan((DateTime.Now.Subtract(myProcessStartTime)).Ticks);

                OnWindowBasicChanged(new Public.ProcessEventArgs(this));
            }
        }

        protected virtual void OnWindowBasicChanged(Public.ProcessEventArgs e)
        {
            if (WindowBasicChanged != null && !myWindowProcess.HasExited)
                WindowBasicChanged(this, e);
        }

        private void GetTitle()
        {
            if (myWindowProcess != null && !myWindowProcess.HasExited)
            {
                try
                {
                    if (myWindowProcess.MainWindowTitle != myWindowTitle)
                    {
                        myWindowTitle = myWindowProcess.MainWindowTitle;

                        OnWindowTitleChanged(new Public.ProcessEventArgs(this));
                    }
                } catch (Exception ex)
                {
                    Log.cLogger.Log(ex);
                }
            }
        }

        protected virtual void OnWindowTitleChanged(Public.ProcessEventArgs e)
        {
            if (WindowTitleChanged != null && !myWindowProcess.HasExited)
                WindowTitleChanged(this, e);
        }

        private void GetProcessCheck()
        {
            try
            {
                if (myWindowProcess == null)
                {
                    if (myWindowProcessID > 0)
                        myWindowProcess = System.Diagnostics.Process.GetProcessById(myWindowProcessID);
                    else
                    {
                        if (!string.IsNullOrEmpty(myWindowProcessName))
                        {
                            System.Diagnostics.Process[] prcArray = System.Diagnostics.Process.GetProcessesByName(myWindowProcessName);
                            if (prcArray != null && prcArray.Length > 0)
                                myWindowProcess = prcArray[0];
                        }
                    }
                }

                if (myWindowProcess != null && !myWindowProcess.HasExited)
                {
                    myWindowProcess.Refresh();
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public void WindowRefreshThread(bool bStart)
        {
            if (bStart)
            {
                if (myWindowDataRefreshThread != null)
                {
                    WriteOutput("Command for WindowRefreshThread to start, but the Thread is not null;", Enums.WindowLogFlags.ERRORLOG);
                } else
                {
                    //start new refresh Thread
                    myWindowDataRefreshThread = new Thread(new ThreadStart(asynRefreshWindowData));
                    myWindowDataRefreshThread.Name = "WindowDataRefresh";
                    myWindowDataRefreshThread.Start();

                    WriteOutput(string.Format("Show Data for Window (Windowhandle='{0}', Processname='{1}', Windowtitle='{2}');", myHandle,myWindowProcess, myWindowTitle), Enums.WindowLogFlags.INFOLOG);
                    WriteOutput("Command for WindowRefreshThread to start State: starting Thread;", Enums.WindowLogFlags.INFOLOG);
                }
            } else
            {
                if (myWindowDataRefreshThread != null)
                {
                    if (myWindowDataRefreshThread.IsAlive)
                    {
                        try
                        {
                            myWindowDataRefreshThread.Abort();
                        }
                        catch (ThreadAbortException)
                        {
                            WriteOutput("Command for WindowRefreshThread to stop State:successful;", Enums.WindowLogFlags.INFOLOG);
                        }
                    } else
                    {
                        WriteOutput("Command for WindowRefreshThread to stop State:already stopped;", Enums.WindowLogFlags.INFOLOG);
                    }
                } else
                {
                    WriteOutput("Command for WindowRefreshThread to stop, but the Thread is null;", Enums.WindowLogFlags.INFOLOG);
                }
            }
        }

        private void asynRefreshWindowData()
        {
            while (myHaveWindow)
            {
                Thread.Sleep(myWindowDataSleepTimebeforRefresh);

                GetProcessCheck();
                GetStyles();
                GetExStyle();
                GetPositionSize();
                GetTimes();
                GetTitle();
            }
        }

        public void Dispose()
        {
            Dispose(_Dispose);
        }

        protected virtual void Dispose(bool bDispose)
        {
            if (!bDispose)
            {
                _Dispose = true;

                if (myWindowProcess != null)
                {
                    myWindowProcess.Disposed -= myWindowProcess_Disposed;
                    myWindowProcess.ErrorDataReceived -= myWindowProcess_ErrorDataReceived;
                    myWindowProcess.Exited -= myWindowProcess_Exited;
                    myWindowProcess.OutputDataReceived -= myWindowProcess_OutputDataReceived;

                    myWindowProcess.Dispose();
                    myWindowProcess = null;
                }

                if (myConfigWindow != null)
                {
                    myConfigWindow = null;
                }

                if (myWindowExStyleList != null)
                {
                    myWindowExStyleList.Clear();
                    myWindowExStyleList = null;
                }

                if (myWindowStyleList != null)
                {
                    myWindowStyleList.Clear();
                    myWindowStyleList = null;
                }
            }
        }

        private void WriteOutput(string strText, Enums.WindowLogFlags e)
        {
            try
            {
                string strInfoText = strText + Environment.NewLine;
                if (WindowDataTextChanged != null)
                    WindowDataTextChanged(this, new LogEventArgs(strText, e));
                switch (e)
                {
                    case Enums.WindowLogFlags.ERRORLOG:
                        myWindowErrorText += strInfoText;
                        break;
                    default:
                        //Infolog
                        myWindowInfoText += strInfoText;
                        break;
                }
            } catch (Exception)
            {
            }
        }

        public void UseDWMThumbnail(IntPtr ipHandle, System.Windows.Forms.Form frmParent, ref IntPtr ipThumbnailRef)
        {
            if (frmParent != null && ipHandle != IntPtr.Zero)
            {
                AWC.WindowHandle.NativStructs.RECT rec = new AWC.WindowHandle.NativStructs.RECT(7, 35, 150, 120);
                DWMThumbnail.Registry(frmParent, ipHandle, ref ipThumbnailRef);
                DWMThumbnail.Update(ipThumbnailRef, rec);
            }
        }

        public bool SetSizeLocation(int iNewPosX, int iNewPosY, int iNewSizeX, int iNewSizeY)
        {
            if (myHandle != IntPtr.Zero)
            {
                return SetSizeLocation(iNewPosX, iNewPosY, iNewSizeX, iNewSizeY, 0);
            }
            else
            {
                Log.cLogger.Log("Can't set Size Location because no intptr Handle is set");
                return false;
            }
        }

        public bool SetSizeLocation(int iNewPosX, int iNewPosY, int iNewSizeX, int iNewSizeY, int iNewFlags)
        {
            if (myHandle != IntPtr.Zero)
            {
                IntPtr iprInsertAfter; 
                if (IsTopMost)
                {
                    iprInsertAfter = NativStructs.HWND_TOPMOST;
                } else
                {
                    iprInsertAfter = NativStructs.HWND_NOTOPMOST;
                }

                return SetSizeLocation(iprInsertAfter, iNewPosX, iNewPosY, iNewSizeX, iNewSizeY, iNewFlags);
            } else
            {
                Log.cLogger.Log("Can't set Size Location because no intptr Handle is set");
                return false;
            }
        }

        public bool SetSizeLocation(IntPtr iprInsertAfter, int iNewPosX, int iNewPosY, int iNewSizeX, int iNewSizeY, int iNewFlags)
        {
            if (myHandle != IntPtr.Zero)
            {
                return Nativ.SetWindowPos(myHandle, iprInsertAfter, iNewPosX, iNewPosY, iNewSizeX, iNewSizeY, iNewFlags);
            }
            else
            {
                Log.cLogger.Log("Can't set Size Location because no intptr Handle is set");
                return false;
            }
        }

        public bool SetBorderStyle(IntPtr mNewStyle)
        {
            if (mNewStyle != IntPtr.Zero && myHandle != IntPtr.Zero)
            {
                //Set_GWLStyle
                //cPInvoke.SetWindowLongPtr(appIntPtr, cWindowHandleFlags.WindowLongFlags.GWL_STYLE, StyleIntPTR)
                Nativ.SetWindowLongPtr(myHandle, Enums.WindowLongFlags.GWL_STYLE, mNewStyle);

                //LoadClickable
                //Return cPInvoke.ShowWindow(appIntPtr, cWindowHandleFlags.ShowWindowCommands.Show)
                Nativ.ShowWindow(myHandle, Enums.ShowWindowCommands.Show);

                //if there is a problem with the border this could possible solve it
                //TA_Window_API.cHandleWindow.Set_WindowPosition(myHandleTasks.Handle, TA_Window_API.cWindowHandleFlags.HWND_NOTOPMOST, CInt(Me.txtXCor.Text), CInt(Me.txtYCor.Text), CInt(Me.txtWidth.Text), CInt(Me.txtHeigh.Text), inrWinPos)

                return true;
            } else
            {
                if (mNewStyle == IntPtr.Zero)
                {
                    Log.cLogger.Log("Can't set Border Style because no intptr for Border Style is set");
                } else
                {
                    Log.cLogger.Log("Can't set Border Style because no intptr Handle is set");
                }
                return false;
            }
        }

        public bool SaveCurrentSizeLocation()
        {
            if (myHandle != IntPtr.Zero)
            {
                if (myWindowRectangle != null)
                {
                    if (myConfigWindow == null)
                        myConfigWindow = new Save.ConfigWindow(myWindowProcessName);

                     myConfigWindow.WindowRectangle = myWindowRectangle;

                     return true;
                } else
                {
                    Log.cLogger.Log("Can't save current Size Location because the rectangle of the Window is not set");
                    return false;
                }
            } else
            {
                Log.cLogger.Log("Can't save current Size Location because no intptr Handle is set");
                return false;
            }
        }
    }



    public class LogEventArgs : EventArgs
    {
        private string mLogtext;
        private Enums.WindowLogFlags myFlag;

        public string Logtext
        {
            get { return mLogtext; }
        }

        public Enums.WindowLogFlags Flag
        {
            get { return myFlag; }
        }

        public LogEventArgs(string strLogText)
        {
            mLogtext = strLogText;
        }

        public LogEventArgs(string strLogText, Enums.WindowLogFlags eFlag)
        {
            mLogtext = strLogText;
            myFlag = eFlag;
        }
    }
}
