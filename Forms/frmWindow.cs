using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using TA_Window_API;
using AWC.Interface;

namespace AWC.Forms
{
    public partial class frmWindow : Form
    {
        private WindowHandle.Window myHWND;
        private IntPtr myRefIntPtrThumb = IntPtr.Zero;

        /// <summary>
        /// True if the Process Refreshed and set the Values
        /// </summary>
        private bool bProcessSetValue = true;

        /// <summary>
        /// True when the User changed a value
        /// </summary>
        private bool bUserSetValue = false;

        private delegate void TextDelegate(string strText);
        private delegate void StyleLoadDelegate(List<global::AWC.WindowHandle.Enums.WindowStyles> styleList);
        private delegate void EmptyDelegate();
        private delegate void ExStyleLoadDelegate(List<global::AWC.WindowHandle.Enums.WindowExStyle> exList);
        private delegate void BoolCallback(bool bBool);

        public frmWindow()
        {
            InitializeComponent();
            InitProcessCollection();


            FillWindowComboBox();
            FillScreenCombo();
        }

        private void InitProcessCollection()
        {
            try
            {
                GPRC_Events(true);
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void GPRC_Events(bool bAddEvent)
        {
            try
            {
                if (bAddEvent)
                {
                    main.GPRC.ProcessAdded += GPRC_ProcessAdded;
                    main.GPRC.ProcessRemoved += GPRC_ProcessRemoved;
                } else
                {
                    main.GPRC.ProcessAdded -= GPRC_ProcessAdded;
                    main.GPRC.ProcessRemoved -= GPRC_ProcessRemoved;
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void GPRC_ProcessRemoved(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                if (myHWND != null)
                {
                    if (myHWND.Processname == e.Window.Processname)
                    {
                        //current selected Window is removed (closed)
                    }
                    else
                    {

                    }
                    if (this.InvokeRequired && this.IsHandleCreated)
                    {
                        this.BeginInvoke(new EmptyDelegate(FillWindowComboBox));
                    }
                    else
                    {
                        FillWindowComboBox();
                    }
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void GPRC_ProcessAdded(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                if (this.InvokeRequired && this.IsHandleCreated)
                {
                    this.BeginInvoke(new EmptyDelegate(FillWindowComboBox));
                }
                else
                {
                    FillWindowComboBox();
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private BindingList<WindowHandle.Window> myBSWindow;

        private void FillWindowComboBox()
        {
            try
            {
                WindowHandle.Window selection = (WindowHandle.Window)cbWindows.SelectedItem;

                myBSWindow = new BindingList<WindowHandle.Window>(main.GPRC.HWND.LHWND);
                
                cbWindows.DataSource = myBSWindow;
                //cbWindows.DataSource = main.GPRC.HWND.LHWND;//HWND.HWNDList.GetList().LHWND;
                cbWindows.DisplayMember = "Title";
                cbWindows.ValueMember = "Handle";

                if (selection != null)
                {
                    if (main.GPRC.HWND.FindByIntPtr(selection.Handle) != null)
                    {
                        cbWindows.SelectedValue = selection.Handle;
                    } else
                    {
                        //last selection exist no longer
                    }
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void cbWindows_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbWindows != null && cbWindows.SelectedValue != null)
                {
                    if (cbWindows.SelectedValue.GetType() == typeof(IntPtr))
                    {
                        //unreg old intptr
                        if (myHWND != null)
                        {
                            if (myRefIntPtrThumb != IntPtr.Zero)
                            {
                                AWC.WindowHandle.DWMThumbnail.Unregistry(myRefIntPtrThumb);
                            }
                        }
                        DisableControls(false);
                        Load((IntPtr)cbWindows.SelectedValue);
                    }
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public new void Load(IntPtr newHandle)
        {
            try
            {
                bProcessSetValue = true;
                SetDefaultWindowLocationSize();
                SetDefaultStyleCheckBox();
                SetDefaultExStyle();
                SetDefaultBasic();
                SetDefaultBorderStyle();
                if (ucCostumBorder1 != null)
                    ucCostumBorder1.SetDefault();
                if (ucProcess1 != null)
                    ucProcess1.SetDefault();
                if (ucModules1 != null)
                    ucModules1.SetDefault();

                //stop the old HWND refresh
                if (myHWND != null)
                {
                    HWND_Events(false);
                    myHWND.WindowRefreshThread(false);
                    myHWND.Dispose();
                    myHWND = null;
                    myRefIntPtrThumb = IntPtr.Zero;
                }

                //get the new selected intptr
                //myHWND = HWND.HWNDList.FindHWNDByIntPTR(newHandle);
                myHWND = main.GPRC.HWND.FindByIntPtr(newHandle);

                if (myHWND != null)
                {
                    Log.cLogger.Log("Window Config: Load after HWND Changed, new HWND Name:" + myHWND.Title);

                    txtAppPath.Text = myHWND.ProcessPath;
                    LoadPositionLocation();
                    LoadStyle(myHWND.Stylelist);
                    LoadExStyle(myHWND.ExStylelist);
                    LoadBasic();
                    LoadBorderStyle();
                    LoadProcessData();
                    LoadModulsData();
                    myHWND.UseDWMThumbnail(myHWND.Handle, this, ref myRefIntPtrThumb);

                    HWND_Events(true);

                    myHWND.WindowRefreshThread(true);
                }
                else
                {
                    Log.cLogger.Log("Window Config: IntPtr is Null");
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            } finally
            {
                bProcessSetValue = false;
            }
        }

        private void HWND_Events(bool bAddEvents)
        {
            try
            {
                if (myHWND != null)
                {
                    if (!bAddEvents)
                    {
                        myHWND.WindowDataTextChanged -= myHWND_WindowDataTextChanged;
                        myHWND.WindowExStyleChanged -= myHWND_WindowExStyleChanged;
                        myHWND.WindowPositionSizeChanged -= myHWND_WindowPositionSizeChanged;
                        myHWND.WindowProcessExit -= myHWND_WindowProcessExit;
                        myHWND.WindowStyleChanged -= myHWND_WindowStyleChanged;
                        myHWND.WindowBasicChanged -= myHWND_WindowBasicChanged;
                        myHWND.WindowTitleChanged -= myHWND_WindowTitleChanged;
                    } else
                    {
                        myHWND.WindowDataTextChanged += myHWND_WindowDataTextChanged;
                        myHWND.WindowExStyleChanged += myHWND_WindowExStyleChanged;
                        myHWND.WindowPositionSizeChanged += myHWND_WindowPositionSizeChanged;
                        myHWND.WindowProcessExit += myHWND_WindowProcessExit;
                        myHWND.WindowStyleChanged += myHWND_WindowStyleChanged;
                        myHWND.WindowBasicChanged += myHWND_WindowBasicChanged;
                        myHWND.WindowTitleChanged += myHWND_WindowTitleChanged;
                    }
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void myHWND_WindowTitleChanged(object sender, EventArgs e)
        {
            if (!bUserSetValue)
            {
                bProcessSetValue = true;

                Log.cLogger.Log(string.Format("Window Title has Changed for Process:'{0}' new Title:'{1}'", myHWND.Processname, myHWND.Title));

                bProcessSetValue = false;
            }
        }

        #region Window events

        void myHWND_WindowBasicChanged(object sender, EventArgs e)
        {
            if (!bUserSetValue)
            {
                bProcessSetValue = true;
                if (this.InvokeRequired)
                {
                    if (this.IsHandleCreated)
                        this.Invoke(new EmptyDelegate(LoadBasic));
                }
                else
                {
                    LoadBasic();
                }
                bProcessSetValue = false;
            }
        }

        void myHWND_WindowStyleChanged(object sender, EventArgs e)
        {
            if (!bUserSetValue)
            {
                bProcessSetValue = true;
                if (this.InvokeRequired)
                {
                    if (this.IsHandleCreated)
                        this.Invoke(new StyleLoadDelegate(LoadStyle), myHWND.Stylelist);
                }
                else
                {
                    LoadStyle(myHWND.Stylelist);
                }
                bProcessSetValue = false;
            }
        }

        void myHWND_WindowProcessExit(object sender, EventArgs e)
        {
            bProcessSetValue = true;
            myHWND.WindowRefreshThread(false);
            Log.cLogger.Log("Process Close for this Window. \rName:" + myHWND.Processname);
            DisableControls(true);
            bProcessSetValue = false;
        }

        void myHWND_WindowPositionSizeChanged(object sender, EventArgs e)
        {
            if (!bUserSetValue)
            {
                bProcessSetValue = true;
                if (this.InvokeRequired)
                {
                    if (this.IsHandleCreated)
                        this.Invoke(new EmptyDelegate(LoadPositionLocation));
                }
                else
                {
                    LoadPositionLocation();
                }
                bProcessSetValue = false;
            }
        }

        void myHWND_WindowExStyleChanged(object sender, EventArgs e)
        {
            if (!bUserSetValue)
            {
                bProcessSetValue = true;
                if (this.InvokeRequired)
                {
                    if (this.IsHandleCreated)
                        this.Invoke(new ExStyleLoadDelegate(LoadExStyle), myHWND.ExStylelist);
                }
                else
                {
                    LoadExStyle(myHWND.ExStylelist);
                }
                bProcessSetValue = false;
            }
        }

        void myHWND_WindowDataTextChanged(object sender, WindowHandle.LogEventArgs e)
        {
            if (!bUserSetValue)
            {
                bProcessSetValue = true;
                if (this.InvokeRequired)
                {
                    if (this.IsHandleCreated)
                        this.Invoke(new TextDelegate(WindowDataTextChanged), e.Logtext);
                }
                else
                {
                    WindowDataTextChanged(e.Logtext);
                }
                bProcessSetValue = false;
            }
        }

        #endregion

        private void WindowDataTextChanged(string Text)
        {
            Log.cLogger.Log(Text);
        }

        private void SetDefaultWindowLocationSize()
        {
            try
            {
                txtHeigh.Text = "";
                txtWidth.Text = "";
                txtXCor.Text = "";
                txtYCor.Text = "";
                cbTopMost.Checked = false;


            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void LoadPositionLocation()
        {
            if (myHWND != null && myHWND.Rectangle != null)
            {
                txtHeigh.Text = myHWND.Rectangle.Height.ToString();
                txtWidth.Text = myHWND.Rectangle.Width.ToString();
                txtXCor.Text = myHWND.Rectangle.Location.X.ToString();
                txtYCor.Text = myHWND.Rectangle.Location.Y.ToString();
                LoadScreenFromPosition(myHWND.Rectangle.Location);
            }
        }

#region Screen
        private void FillScreenCombo()
        {
            try
            {
                foreach (Screen scr in Screen.AllScreens)
                {
                    if (scr.Primary)
                    {
                        cbScreenSelect.Items.Add("Primar Screen | " + scr.DeviceName);
                        cbScreenSelect.SelectedItem = "Primar Screen | " + scr.DeviceName;
                    } else
                    {
                        cbScreenSelect.Items.Add(scr.DeviceName);
                    }
                }

                if (cbScreenSelect.Items.Count <= 1 )
                {
                    cbScreenSelect.Enabled = false;
                } else
                {
                    cbScreenSelect.Enabled = true;
                }

            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void LoadScreenFromPosition(Point locPoint)
        {
            try
            {
                if (locPoint != null)
                {
                    if (cbScreenSelect.Items.Count > 1)
                    {
                        foreach (Screen scr in Screen.AllScreens)
                        {
                            //check if the location is in the bound from the Screen 
                            //if so this screen should be selected
                            if ((scr.Bounds.X < locPoint.X) && ((scr.Bounds.X + scr.Bounds.Width) > locPoint.X) && (scr.Bounds.Y < locPoint.Y) && ((scr.Bounds.Y + scr.Bounds.Height) > locPoint.Y) )
                            {
                                if (scr.Primary)
                                {
                                    cbScreenSelect.SelectedItem = "Primar Screen | " + scr.DeviceName;
                                } else
                                {
                                    cbScreenSelect.SelectedItem = scr.DeviceName;
                                }
                            }
                        }
                    }
                }

            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }
#endregion

        private void SetDefaultBasic()
        {
            txtprcRuntime.Text = "";
            txtPrcStartTime.Text = "";
        }

        private void LoadBasic()
        {
            if (myHWND != null && myHWND.StartTime != DateTime.MinValue && myHWND.Runtime != TimeSpan.Zero)
            {
                txtPrcStartTime.Text = myHWND.StartTime.ToString();
                txtprcRuntime.Text = "Days:" + myHWND.Runtime.Days + " Hours:" + myHWND.Runtime.Hours + " Minutes:" + myHWND.Runtime.Minutes;
            }
        }

        #region Window Style

        private void LoadStyle(List<global::AWC.WindowHandle.Enums.WindowStyles> mStyle)
        {
            try
            {
                if (mStyle != null)
                {
                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.BORDER))
                    {
                        cbBorder.Checked = true;
                    }
                    else
                    {
                        cbBorder.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.CAPTION))
                    {
                        cbCaption.Checked = true;
                        //includes WS_BORDER
                    }
                    else
                    {
                        cbCaption.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.CHILD))
                    {
                        cbChild.Checked = true;
                        //WS_POPUP cant be used
                    }
                    else
                    {
                        cbChild.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.CLIPCHILDREN))
                    {
                        cbClipChildren.Checked = true;
                    }
                    else
                    {
                        cbClipChildren.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.CLIPSIBLINGS))
                    {
                        cbClipSiblings.Checked = true;
                    }
                    else
                    {
                        cbClipSiblings.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.DISABLED))
                    {
                        cbDisabled.Checked = true;
                    }
                    else
                    {
                        cbDisabled.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.DLGFRAME))
                    {
                        cbDLGFrame.Checked = true;
                    }
                    else
                    {
                        cbDLGFrame.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.GROUP))
                    {
                        cbGroup.Checked = true;
                    }
                    else
                    {
                        cbGroup.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.HSCROLL))
                    {
                        cbHScroll.Checked = true;
                    }
                    else
                    {
                        cbHScroll.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.MAXIMIZE))
                    {
                        cbMaximize.Checked = true;
                    }
                    else
                    {
                        cbMaximize.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.MAXIMIZEBOX))
                    {
                        cbMaximizebox.Checked = true;
                        //WS_EX_CONTEXTHELP not allowed
                        //WS_SYSMENU must set
                    }
                    else
                    {
                        cbMaximizebox.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.MINIMIZE))
                    {
                        cbMinimize.Checked = true;
                    }
                    else
                    {
                        cbMinimize.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.MINIMIZEBOX))
                    {
                        cbMinimizebox.Checked = true;
                        //WS_EX_CONTEXTHELP not allowed
                        //WS_SYSMENU must set
                    }
                    else
                    {
                        cbMinimizebox.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.OVERLAPPED))
                    {
                        cbOverlapped.Checked = true;
                    }
                    else
                    {
                        cbOverlapped.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.POPUP))
                    {
                        cbPopup.Checked = true;
                        //WS_CHILD not allowed
                    }
                    else
                    {
                        cbPopup.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.POPUPWINDOW))
                    {
                        cbPopupwindow.Checked = true;
                    }
                    else
                    {
                        cbPopupwindow.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.SIZEFRAME))
                    {
                        cbSizeframe.Checked = true;
                    }
                    else
                    {
                        cbSizeframe.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.TABSTOP))
                    {
                        cbTabstop.Checked = true;
                    }
                    else
                    {
                        cbTabstop.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.VISIBLE))
                    {
                        cbVisible.Checked = true;
                    }
                    else
                    {
                        cbVisible.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.VSCROLL))
                    {
                        cbVScroll.Checked = true;
                    }
                    else
                    {
                        cbVScroll.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.SYSMENU))
                    {
                        cbSysmenu.Checked = true;
                        //WS_CAPTION must set
                    }
                    else
                    {
                        cbSysmenu.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.OVERLAPPEDWINDOW))
                    {
                        cbOverlappedWindow.Checked = true;
                        //Overlappedwindow have "WS_BORDER | WS_DLGFRAME | WS_GROUP | WS_MAXIMIZEBOX | WS_SIZEFRAME | WS_SYSMENU"
                    }
                    else
                    {
                        cbOverlappedWindow.Checked = false;
                    }

                    if (mStyle.Contains(global::AWC.WindowHandle.Enums.WindowStyles.MAXIMIZE))
                    {
                        cbFullsizeOnScreen.Checked = true;
                    }
                    else
                    {
                        cbFullsizeOnScreen.Checked = false;
                    }


                }
                else
                {
                    Log.cLogger.Log("No Style Data for the selected Window");
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }


        private void cbCaption_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbBorder.Checked = cbCaption.Checked;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void cbChild_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbPopup.Checked = !(cbChild.Checked);
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void cbMaximizebox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbSysmenu.Checked = cbMaximizebox.Checked;
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void cbMinimizebox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbSysmenu.Checked = cbMinimizebox.Checked;
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void cbPopup_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbChild.Checked = false;
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void cbSysmenu_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbCaption.Checked = cbSysmenu.Checked;
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void cbOverlappedWindow_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                cbBorder.Checked = cbOverlappedWindow.Checked;
                cbDLGFrame.Checked = cbOverlappedWindow.Checked;
                cbGroup.Checked = cbOverlappedWindow.Checked;
                cbMaximizebox.Checked = cbOverlappedWindow.Checked;
                cbSizeframe.Checked = cbOverlappedWindow.Checked;
                cbSysmenu.Checked = cbOverlappedWindow.Checked;
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void SetDefaultStyleCheckBox()
        {
            try
            {
                cbBorder.Checked = false;
                cbCaption.Checked = false;
                cbChild.Checked = false;
                cbClipChildren.Checked = false;
                cbClipSiblings.Checked = false;
                cbDisabled.Checked = false;
                cbDLGFrame.Checked = false;
                cbGroup.Checked = false;
                cbHScroll.Checked = false;
                cbMaximize.Checked = false;
                cbMaximizebox.Checked = false;
                cbMinimize.Checked = false;
                cbMinimizebox.Checked = false;
                cbOverlapped.Checked = false;
                cbPopup.Checked = false;
                cbPopupwindow.Checked = false;
                cbSizeframe.Checked = false;
                cbTabstop.Checked = false;
                cbVScroll.Checked = false;
                cbSysmenu.Checked = false;
                cbOverlappedWindow.Checked = false;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }
        #endregion

        #region Window ExStyle

        private void SetDefaultExStyle()
        {
            try
            {
                cbExAcceptFiles.Checked = false;
                cbExAppwindow.Checked = false;
                cbExClientedge.Checked = false;
                cbExComposited.Checked = false;
                cbExContexthelp.Checked = false;
                cbExControlparent.Checked = false;
                cbExDLGModalFrame.Checked = false;
                cbExLayered.Checked = false;
                cbExLayoutRTL.Checked = false;
                cbExLeft.Checked = false;
                cbExLeftScrollbar.Checked = false;
                cbExLTRReading.Checked = false;
                cbExMDIChild.Checked = false;
                cbExNoActivate.Checked = false;
                cbExNoInheritLayout.Checked = false;
                cbExNoParentNotify.Checked = false;
                cbExOverlappedwindow.Checked = false;
                cbExPalettewindow.Checked = false;
                cbExRight.Checked = false;
                cbExRightscrollbar.Checked = false;
                cbExRTLReading.Checked = false;
                cbExStaticedge.Checked = false;
                cbExToolwindow.Checked = false;
                cbExTopMost.Checked = false;
                cbExTransparent.Checked = false;
                cbExWindowedge.Checked = false;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void LoadExStyle(List<global::AWC.WindowHandle.Enums.WindowExStyle> mExStyle)
        {
            try
            {
                if (mExStyle != null)
                {
                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.ACCEPTFILES))
                    {
                        cbExAcceptFiles.Checked = true;
                    } else
                    {
                        cbExAcceptFiles.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.APPWINDOW))
                    {
                        cbExAppwindow.Checked = true;
                    }
                    else
                    {
                        cbExAppwindow.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.CLIENTEDGE))
                    {
                        cbExClientedge.Checked = true;
                    }
                    else
                    {
                        cbExClientedge.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.COMPOSITED))
                    {
                        cbExComposited.Checked = true;
                    }
                    else
                    {
                        cbExComposited.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.CONTEXTHELP))
                    {
                        cbExContexthelp.Checked = true;
                    }
                    else
                    {
                        cbExContexthelp.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.CONTROLPARENT))
                    {
                        cbExControlparent.Checked = true;
                    }
                    else
                    {
                        cbExControlparent.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.DLGMODALFRAME))
                    {
                        cbExDLGModalFrame.Checked = true;
                    }
                    else
                    {
                        cbExDLGModalFrame.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.LAYERED))
                    {
                        cbExLayered.Checked = true;
                    }
                    else
                    {
                        cbExLayered.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.LAYOUTRTL))
                    {
                        cbExLayoutRTL.Checked = true;
                    }
                    else
                    {
                        cbExLayoutRTL.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.LEFT))
                    {
                        cbExLeft.Checked = true;
                    }
                    else
                    {
                        cbExLeft.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.LEFTSCROLLBAR))
                    {
                        cbExLeftScrollbar.Checked = true;
                    }
                    else
                    {
                        cbExLeftScrollbar.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.LTRREADING))
                    {
                        cbExLTRReading.Checked = true;
                    }
                    else
                    {
                        cbExLTRReading.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.MDICHILD))
                    {
                        cbExMDIChild.Checked = true;
                    }
                    else
                    {
                        cbExMDIChild.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.NOACTIVATE))
                    {
                        cbExNoActivate.Checked = true;
                    }
                    else
                    {
                        cbExNoActivate.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.NOINHERITLAYOUT))
                    {
                        cbExNoInheritLayout.Checked = true;
                    }
                    else
                    {
                        cbExNoInheritLayout.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.NOPARENTNOTIFY))
                    {
                        cbExNoParentNotify.Checked = true;
                    }
                    else
                    {
                        cbExNoParentNotify.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.OVERLAPPEDWINDOW))
                    {
                        cbExOverlappedwindow.Checked = true;
                    }
                    else
                    {
                        cbExOverlappedwindow.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.PALETTEWINDOW))
                    {
                        cbExPalettewindow.Checked = true;
                    }
                    else
                    {
                        cbExPalettewindow.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.RIGHT))
                    {
                        cbExRight.Checked = true;
                    }
                    else
                    {
                        cbExRight.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.RIGHTSCROLLBAR))
                    {
                        cbExRightscrollbar.Checked = true;
                    }
                    else
                    {
                        cbExRightscrollbar.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.RTLREADING))
                    {
                        cbExRTLReading.Checked = true;
                    }
                    else
                    {
                        cbExRTLReading.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.STATICEDGE))
                    {
                        cbExStaticedge.Checked = true;
                    }
                    else
                    {
                        cbExStaticedge.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.TOOLWINDOW))
                    {
                        cbExToolwindow.Checked = true;
                    }
                    else
                    {
                        cbExToolwindow.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.TOPMOST))
                    {
                        cbExTopMost.Checked = true;
                    }
                    else
                    {
                        cbExTopMost.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.TRANSPARENT))
                    {
                        cbExTransparent.Checked = true;
                    }
                    else
                    {
                        cbExTransparent.Checked = false;
                    }

                    if (mExStyle.Contains(global::AWC.WindowHandle.Enums.WindowExStyle.WINDOWEDGE))
                    {
                        cbExWindowedge.Checked = true;
                    }
                    else
                    {
                        cbExWindowedge.Checked = false;
                    }

                    cbTopMost.Checked = myHWND.IsTopMost;

                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        #endregion

        private void DisableControls(bool bDisable)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke(new BoolCallback(DisableControls), bDisable);
                }
                else
                {
        
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void frmWindowConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (myHWND != null)
                {
                    HWND_Events(false);

                    myHWND.WindowRefreshThread(false);
                    myHWND.Dispose();
                    myHWND = null;
                }
                GPRC_Events(false);
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void btnOpenPath_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtAppPath.Text))
                {
                    System.Diagnostics.Process.Start(System.IO.Path.GetDirectoryName(txtAppPath.Text));
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void ChangeSizeLocation(string strNewPosX, string strNewPosY, string strNewSizeX, string strNewSizeY)
        {
            try
            {
                if (!bProcessSetValue)
                {
                    if (myHWND != null && myHWND.HaveWindow)
                    {
                        int iNPosX = Convert.ToInt32(strNewPosX);
                        int iNPosY = Convert.ToInt32(strNewPosY);
                        int iNSizeX = Convert.ToInt32(strNewSizeX);
                        int iNSizeY = Convert.ToInt32(strNewSizeY);

                        if (iNPosX != myHWND.Rectangle.Location.X || iNPosY != myHWND.Rectangle.Location.Y || iNSizeX != myHWND.Rectangle.Width || iNSizeY != myHWND.Rectangle.Height)
                            myHWND.SetSizeLocation(iNPosX, iNPosY, iNSizeX, iNSizeY);
                    }
                    else
                    {
                        throw new ArgumentNullException("No Window Class exist or the process has no Window");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void ChangeSizeLocation(string strNewPosX, string strNewPosY, string strNewSizeX, string strNewSizeY, bool IsTopMost)
        {
            try
            {
                if (!bProcessSetValue || bUserSetValue)
                {
                    if (myHWND != null && myHWND.HaveWindow)
                    {
                        int iNPosX = Convert.ToInt32(strNewPosX);
                        int iNPosY = Convert.ToInt32(strNewPosY);
                        int iNSizeX = Convert.ToInt32(strNewSizeX);
                        int iNSizeY = Convert.ToInt32(strNewSizeY);
                        IntPtr itrTopMost;
                        short inrWinPos = 0;

                        if (IsTopMost)
                        {
                            itrTopMost = AWC.WindowHandle.NativStructs.HWND_TOPMOST;
                            inrWinPos += (short)AWC.WindowHandle.Enums.WindowPosition.TopMost;
                        } else
                        {
                            itrTopMost = AWC.WindowHandle.NativStructs.HWND_NOTOPMOST;
                        }
                        
                        if (iNPosX != myHWND.Rectangle.Location.X || iNPosY != myHWND.Rectangle.Location.Y || iNSizeX != myHWND.Rectangle.Width || iNSizeY != myHWND.Rectangle.Height)
                        {
                            //can moved and size changed
                        } else
                        {
                            //same position and location as before
                            inrWinPos += (short)AWC.WindowHandle.Enums.WindowPosition.NoMove;
                            inrWinPos += (short)AWC.WindowHandle.Enums.WindowPosition.NoSize;
                        }

                        myHWND.SetSizeLocation(itrTopMost, iNPosX, iNPosY, iNSizeX, iNSizeY, inrWinPos);
                    }
                    else
                    {
                        throw new ArgumentNullException("No Window Class exist or the process has no Window");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void LoadProcessData()
        {
            try
            {
                if (ucProcess1 != null)
                    ucProcess1.Load(myHWND.Process);
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void LoadModulsData()
        {
            try
            {
                if (ucModules1 != null)
                    ucModules1.Load(myHWND.Process);
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void SetDefaultBorderStyle()
        {
            rbtnCostumBorder.Checked = true;
        }

        private void LoadBorderStyle()
        {
            try
            {
                if (myHWND != null && myHWND.HaveWindow)
                {
                    if (myHWND.Stylelist.Contains(WindowHandle.Enums.WindowStyles.SIZEFRAME))
                    {
                        rbtnSizingBorder.Checked = true;
                    }
                    else if (myHWND.Stylelist.Contains(WindowHandle.Enums.WindowStyles.MAXIMIZEBOX))
                    {
                        rbtnBorderless.Checked = true;
                    }
                    else if (myHWND.Stylelist.Contains(WindowHandle.Enums.WindowStyles.BORDER))
                    {
                        rbtnThinLineBorder.Checked = true;
                    }
                    else if (myHWND.Stylelist.Contains(WindowHandle.Enums.WindowStyles.OVERLAPPEDWINDOW))
                    {
                        rbtnOverlappedBorder.Checked = true;
                    }
                    else 
                    {
                        rbtnCostumBorder.Checked = true;
                        if (ucCostumBorder1 != null)
                            ucCostumBorder1.Load(myHWND.Stylelist);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void SetBorderStyle()
        {
            try
            {
                if (myHWND != null && myHWND.HaveWindow)
                {
                    if (rbtnBorderless.Checked)
                    {
                        myHWND.SetBorderStyle((IntPtr)WindowHandle.Enums.WindowStyles.MAXIMIZEBOX);
                    }
                    else if (rbtnOverlappedBorder.Checked)
                    {
                        myHWND.SetBorderStyle((IntPtr)WindowHandle.Enums.WindowStyles.OVERLAPPEDWINDOW);
                    }
                    else if (rbtnSizingBorder.Checked)
                    {
                        myHWND.SetBorderStyle((IntPtr)WindowHandle.Enums.WindowStyles.SIZEFRAME);
                    }
                    else if (rbtnThinLineBorder.Checked)
                    {
                        myHWND.SetBorderStyle((IntPtr)WindowHandle.Enums.WindowStyles.BORDER);
                    }
                    else if (rbtnCostumBorder.Checked)
                    {
                        myHWND.SetBorderStyle((IntPtr)ucCostumBorder1.GetSelected());
                    }
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void btnSetSizeLocation_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeSizeLocation(txtXCor.Text, txtYCor.Text, txtWidth.Text, txtHeigh.Text);
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void btnSetBorderStyle_Click(object sender, EventArgs e)
        {
            try
            {
                SetBorderStyle();
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void rbtnCostumBorder_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ucCostumBorder1.ControlStatus(rbtnCostumBorder.Checked);
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void cbTopMost_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bProcessSetValue)
                {
                    bUserSetValue = true;
                    //user changed top most for the Window
                    ChangeSizeLocation(txtXCor.Text, txtYCor.Text, txtWidth.Text, txtHeigh.Text, cbTopMost.Checked);
                    bUserSetValue = false;
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            } finally
            {

            }
        }

        private void cbFullsizeOnScreen_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bProcessSetValue)
                {
                    bUserSetValue = true;

                    if (cbFullsizeOnScreen.Checked)
                    {
                        //set full size for this window
                        SetFullScreenOnSelectedDesktopScreen();

                    } else
                    {
                        //restore old size and position of the window
                        AWC.WindowHandle.Nativ.ShowWindow(myHWND.Handle, AWC.WindowHandle.Enums.ShowWindowCommands.Restore);
                    }

                    bUserSetValue = false;
                }

            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void SetFullScreenOnSelectedDesktopScreen()
        {
            try
            {
                string strSelectedScreen = cbScreenSelect.SelectedItem.ToString();
                foreach (Screen scr in Screen.AllScreens)
                {
                    if (strSelectedScreen.Contains(scr.DeviceName))
                    {
                        //set location to this screen before maximize
                        ChangeSizeLocation(scr.Bounds.X.ToString(), scr.Bounds.Y.ToString(), myHWND.Rectangle.Width.ToString(), myHWND.Rectangle.Height.ToString());
                    }
                }


                AWC.WindowHandle.Nativ.ShowWindow(myHWND.Handle, AWC.WindowHandle.Enums.ShowWindowCommands.Maximize);
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void cbScreenSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!bProcessSetValue)
                {
                    bUserSetValue = true;
                    if (cbFullsizeOnScreen.Checked)
                        SetFullScreenOnSelectedDesktopScreen();

                    bUserSetValue = false;
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

    }
}
