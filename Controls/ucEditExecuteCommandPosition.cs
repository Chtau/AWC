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
    public partial class ucEditExecuteCommandPosition : ucEditExecuteCommandBase
    {
        public ucEditExecuteCommandPosition()
        {
            InitializeComponent();
        }

        public override void Load(string strCommand)
        {
            try
            {
                string[] strPos = strCommand.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                if (strPos.Length > 0)
                {
                    for (int i = 0; i < strPos.Length; i++)
                    {
                        if (strPos[i].StartsWith("X:"))
                        {
                            txtXCor.Text = strPos[i].Replace("X:", "").Trim();
                        }
                        else if (strPos[i].StartsWith("Y:"))
                        {
                            txtYCor.Text = strPos[i].Replace("Y:", "").Trim();
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
                return "X:" + txtXCor.Text + ";Y:" + txtYCor.Text;
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return "";
            }
        }
    }
}
