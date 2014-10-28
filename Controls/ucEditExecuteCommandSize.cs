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
    public partial class ucEditExecuteCommandSize : ucEditExecuteCommandBase
    {
        public ucEditExecuteCommandSize()
        {
            InitializeComponent();
        }

        public override void Load(string strCommand)
        {
            try
            {
                string[] strSize = strCommand.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (strSize.Length > 0)
                {
                    for (int i = 0; i < strSize.Length; i++)
                    {
                        if (strSize[i].StartsWith("H:"))
                        {
                            txtHeight.Text = strSize[i].Replace("H:", "").Trim();
                        }
                        else if (strSize[i].StartsWith("W:"))
                        {
                            txtWidth.Text = strSize[i].Replace("W:", "").Trim();
                        }
                    }
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public override string Accept()
        {
            try
            {
                return "H:" + txtHeight.Text + ";W:" + txtWidth.Text;
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return "";
            }
        }
    }
}
