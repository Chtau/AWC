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
    public partial class ucEditExecuteCommandCommand : ucEditExecuteCommandBase
    {
        public ucEditExecuteCommandCommand()
        {
            InitializeComponent();
        }

        public override void Load(string strCommand)
        {
            try
            {
                txtApplication.Text = strCommand;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public override string Accept()
        {
            try
            {
                return txtApplication.Text + " " + txtParameter.Text;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return "";
            }
        }
    }
}
