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
        private Forms.frmWindow frmWS;
        private static AWC.ExternTools.ExternTool myExTool;

        public static AWC.ExternTools.ExternTool ExternTool
        {
            get
            {
                if (myExTool == null)
                    main.InitGlobal();

                return myExTool;
            }
        }

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
            myExTool = new ExternTools.ExternTool();
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
                frmWS = new Forms.frmWindow();
                frmWS.FormClosed += frmWS_FormClosed;
                frmWS.Show();
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void frmWS_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (frmWS != null && !frmWS.IsDisposed)
                {
                    frmWS.FormClosed -= frmWS_FormClosed;
                    frmWS.Dispose();
                    frmWS = null;
                }
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
                if (myExTool != null)
                {
                    myExTool.Dispose();
                    myExTool = null;
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void writeLogToFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Title = "Save log to File";
                saveFileDialog1.DefaultExt = "log";

                string mFileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\AWCDebug.log";
                saveFileDialog1.FileName = mFileName;

                 if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                 {
                     if (!string.IsNullOrEmpty(saveFileDialog1.FileName))
                     {
                         mFileName = saveFileDialog1.FileName;
                     }   
                 } else
                 {

                 }
                 Log.cLogger.CreateDebugLogFile(saveFileDialog1.FileName);

            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void processCheckerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessChecker(processCheckerToolStripMenuItem.Checked);
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void ProcessChecker(bool bStart)
        {
            try
            {
                if (bStart)
                {
                    if (myExTool == null)
                        myExTool = new ExternTools.ExternTool();

                    myExTool.StartCheck(myGPrc);
                }
                else
                {
                    myExTool.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

    }
}
