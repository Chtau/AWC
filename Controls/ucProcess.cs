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
    public partial class ucProcess : UserControl
    {
        private System.Diagnostics.Process myPrc;

        public ucProcess()
        {
            InitializeComponent();
        }

        public void SetDefault()
        {
            try
            {
                txtProcessID.Text = "";
                txtProcessName.Text = "";
                txtProcessStarttime.Text = "";
                txtProcessThreads.Text = "";
                txtProcessSessionID.Text = "";
                txtParams.Text = "";
                txtPrcFilename.Text = "";
                txtUser.Text = "";
                txtDomain.Text = "";
                chbPrcPrioBoost.Checked = false;
                chbPrcResponding.Checked = false;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public new void Load(System.Diagnostics.Process mPrc)
        {
            try
            {
                if (mPrc != null)
                {
                    myPrc = mPrc;

                    txtProcessID.Text = myPrc.Id.ToString();
                    txtProcessName.Text = myPrc.ProcessName;
                    txtProcessStarttime.Text = myPrc.StartTime.ToString();
                    txtProcessThreads.Text = myPrc.Threads.Count.ToString();
                    txtProcessSessionID.Text = myPrc.SessionId.ToString();
                    RefreshPrcData();
                    txtParams.Text = myPrc.StartInfo.Arguments;
                    txtPrcFilename.Text = myPrc.StartInfo.FileName;
                    txtUser.Text = myPrc.StartInfo.UserName;
                    txtDomain.Text = myPrc.StartInfo.Domain;
                } else
                {
                    SetDefault();
                    throw new ArgumentNullException("mPrc");
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void RefreshPrcData()
        {
            try
            {
                chbPrcPrioBoost.Checked = myPrc.PriorityBoostEnabled;
                chbPrcResponding.Checked = myPrc.Responding;
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }
    }
}
