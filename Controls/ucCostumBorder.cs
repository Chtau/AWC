using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AWC.Controls
{
    public partial class ucCostumBorder : UserControl
    {
        public ucCostumBorder()
        {
            InitializeComponent();
        }

        public new void Load(WindowHandle.Enums.WindowStyles mStyles)
        {
            try
            {
                if (mStyles != 0)
                {

                } else
                {
                    SetDefault();
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public new void Load(List<WindowHandle.Enums.WindowStyles> mStyles)
        {
            try
            {
                if (mStyles != null && mStyles.Count > 0)
                {
                    chbBorder.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.BORDER);
                    chbCaption.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.CAPTION);
                    chbClipChildren.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.CLIPCHILDREN);
                    chbDisabeld.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.DISABLED);
                    chbDlgFrame.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.DLGFRAME);
                    chbGroup.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.GROUP);
                    chbHScroll.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.HSCROLL);
                    chbMaximize.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.MAXIMIZE);
                    chbMaximizeBox.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.MAXIMIZEBOX);
                    chbMinimize.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.MINIMIZE);
                    chbMinimizeBox.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.MINIMIZEBOX);
                    chbOverlapped.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.OVERLAPPED);
                    chbOverlappedWindow.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.OVERLAPPEDWINDOW);
                    chbPopup.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.POPUP);
                    chbPopupWindow.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.POPUPWINDOW);
                    chbSizeFrame.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.SIZEFRAME);
                    chbSysMenu.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.SYSMENU);
                    chbTabstop.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.TABSTOP);
                    chbVisible.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.VISIBLE);
                    chbVScroll.Checked = mStyles.Contains(WindowHandle.Enums.WindowStyles.VSCROLL);
                }
                else
                {
                    SetDefault();
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public WindowHandle.Enums.WindowStyles GetSelected()
        {
            try
            {
                WindowHandle.Enums.WindowStyles mSelectedStyle = 0;

                if (chbBorder.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.BORDER;

                if (chbCaption.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.CAPTION;

                if (chbChild.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.CHILD;

                if (chbClipChildren.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.CLIPCHILDREN;

                if (chbDisabeld.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.DISABLED;

                if (chbDlgFrame.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.DLGFRAME;

                if (chbGroup.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.GROUP;

                if (chbHScroll.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.HSCROLL;

                if (chbMaximize.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.MAXIMIZE;

                if (chbMaximizeBox.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.MAXIMIZEBOX;

                if (chbMinimize.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.MINIMIZE;

                if (chbMinimizeBox.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.MINIMIZEBOX;

                if (chbOverlapped.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.OVERLAPPED;

                if (chbOverlappedWindow.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.OVERLAPPEDWINDOW;

                if (chbPopup.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.POPUP;

                if (chbPopupWindow.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.POPUPWINDOW;

                if (chbSizeFrame.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.SIZEFRAME;

                if (chbSysMenu.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.SYSMENU;

                if (chbTabstop.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.TABSTOP;

                if (chbVisible.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.VISIBLE;

                if (chbVScroll.Checked)
                    mSelectedStyle = mSelectedStyle | WindowHandle.Enums.WindowStyles.VSCROLL;



                return mSelectedStyle;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return 0;
            }
        }

        public void ControlStatus(bool bEndable)
        {
            try
            {
                chbBorder.Enabled = bEndable;
                chbCaption.Enabled = bEndable;
                chbChild.Enabled = bEndable;
                chbClipChildren.Enabled = bEndable;
                chbDisabeld.Enabled = bEndable;
                chbDlgFrame.Enabled = bEndable;
                chbGroup.Enabled = bEndable;
                chbHScroll.Enabled = bEndable;
                chbMaximize.Enabled = bEndable;
                chbMaximizeBox.Enabled = bEndable;
                chbMinimize.Enabled = bEndable;
                chbMinimizeBox.Enabled = bEndable;
                chbOverlapped.Enabled = bEndable;
                chbOverlappedWindow.Enabled = bEndable;
                chbPopup.Enabled = bEndable;
                chbPopupWindow.Enabled = bEndable;
                chbSizeFrame.Enabled = bEndable;
                chbSysMenu.Enabled = bEndable;
                chbTabstop.Enabled = bEndable;
                chbVisible.Enabled = bEndable;
                chbVScroll.Enabled = bEndable;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public void SetDefault()
        {
            try
            {
                chbBorder.Checked = false;
                chbCaption.Checked = false;
                chbChild.Checked = false;
                chbClipChildren.Checked = false;
                chbDisabeld.Checked = false;
                chbDlgFrame.Checked = false;
                chbGroup.Checked = false;
                chbHScroll.Checked = false;
                chbMaximize.Checked = false;
                chbMaximizeBox.Checked = false;
                chbMinimize.Checked = false;
                chbMinimizeBox.Checked = false;
                chbOverlapped.Checked = false;
                chbOverlappedWindow.Checked = false;
                chbPopup.Checked = false;
                chbPopupWindow.Checked = false;
                chbSizeFrame.Checked = false;
                chbSysMenu.Checked = false;
                chbTabstop.Checked = false;
                chbVisible.Checked = false;
                chbVScroll.Checked = false;
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }
    }
}
