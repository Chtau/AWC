using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AWC.Forms
{
    public partial class frmWindowDataOutput : Form
    {
        public frmWindowDataOutput()
        {
            InitializeComponent();
        }

        private IntPtr myWindowHanle;
        private WindowHandle.Window myWindow;
        private delegate void FillOutputCallback(WindowHandle.LogEventArgs e);

        public IntPtr WindowHandle 
        {
            get { return myWindowHanle;}
            set { myWindowHanle = value;} 
        }

        private void frmWindowDataOutput_Load(object sender, EventArgs e)
        {
            try
            {
                cbWindows.DataSource = HWND.HWNDList.GetList().LHWND;
                cbWindows.DisplayMember = "Title";
                cbWindows.ValueMember = "Handle";
                if (myWindowHanle != IntPtr.Zero)
                {
                    cbWindows.SelectedValue = myWindowHanle;
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void cbWindows_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbWindows.SelectedValue.GetType() == typeof(IntPtr))
                {
                    if ((IntPtr)cbWindows.SelectedValue != IntPtr.Zero)
                    {
                        myWindowHanle = (IntPtr)cbWindows.SelectedValue;
                        Log.cLogger.Log(string.Format("frmWindowDataOutput Handle selection changed to Handle(IntPtr:'{0}')", myWindowHanle));
                        LoadWindow(myWindowHanle);
                    }
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void LoadWindow(IntPtr intHWnd)
        {
            try
            {
                if (intHWnd != IntPtr.Zero)
                {
                    if (myWindow != null)
                    {
                        myWindow.WindowRefreshThread(false);
                        myWindow = null;
                    }

                    AWC.WindowHandle.Window myHWND = HWND.HWNDList.FindHWNDByIntPTR(intHWnd);

                    if (myHWND != null)
                    {
                        myWindow = new WindowHandle.Window(myHWND.Process);
                        if (myWindow != null)
                        {
                            myWindow.WindowDataTextChanged += myWindow_WindowDataTextChanged;
                            myWindow.WindowRefreshThread(true);
                        } else
                        {
                            Log.cLogger.Log(string.Format("Init Window failed with IntPtr:'{0}' Name:'{1}'", myHWND.Handle, myHWND.Title));
                        }
                    } else
                    {
                        Log.cLogger.Log("Can't load new Window in WindowDataOutput because don't find process from handle(intptr)");
                    }
                } else
                {
                    Log.cLogger.Log("Can't load new Window in WindowDataOutput because handle(intptr) is Zero");
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void myWindow_WindowDataTextChanged(object sender, WindowHandle.LogEventArgs e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new FillOutputCallback(FillOutputData), e);
                } else
                {
                    FillOutputData(e);
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void FillOutputData(WindowHandle.LogEventArgs e)
        {
            try
            {
                rtxtOutput.Text += e.Logtext + Environment.NewLine;
                rtxtOutput.SelectionStart = rtxtOutput.Text.Length;
                rtxtOutput.ScrollToCaret();
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }
    }
}
