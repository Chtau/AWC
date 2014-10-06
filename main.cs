using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AWC
{
    public partial class main : Form
    {
        private static AWC.Global.GProcessData myGPrc;

        public static AWC.Global.GProcessData GPRC
        {
            get
            {
                if (myGPrc == null)
                    main.InitGlobal();

                return myGPrc;
            }
        }

        private static void InitGlobal()
        {
            myGPrc = new Global.GProcessData();
        }

        public main()
        {
            InitializeComponent();

            this.Opacity = 0;

            InitLogger();
            InitGlobal();

#if (DEBUG)
            {
                this.Opacity = 1;
                Log.cLogger.Log("While Debug the main Form is shown");
            }
#endif
        }

        private void InitLogger()
        {
            Log.cLogger.SetDebugTextControl(rtbDebugText);
            Log.cLogger.Log("Init Logger for Debug");
        }

        private void main_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                showDebugWindowToolStripMenuItem.Checked = this.Visible;

            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void tsmiClose_Click(object sender, EventArgs e)
        {
            try
            {
                Log.cLogger.Log("Application will shutdown in a moment");

                Application.Exit();
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void showDebugWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Opacity == 1)
                {
                    this.Opacity = 0;
                } else
                {
                    this.Opacity = 1;
                }
                Log.cLogger.Log("Debug Window Opacity Changed, Opacity after change:" + this.Opacity.ToString());
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void windowStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Forms.frmWindow frmWS = new Forms.frmWindow();
                frmWS.Show();
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (myGPrc != null)
                {
                    myGPrc.InterupRefresh();
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

    }
}
