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
    public partial class ucEditExecuteCommandBorder : ucEditExecuteCommandBase
    {
        public ucEditExecuteCommandBorder()
        {
            InitializeComponent();
        }

        public override void Load(string strCommand)
        {
            try
            {
                switch (Convert.ToInt32(strCommand))
                {
                    case 1:
                        rbtnBorderless.Checked = true;
                        break;
                    case 2:
                        rbtnOverlappedBorder.Checked = true;
                        break;
                    case 3:
                        rbtnSizingBorder.Checked = true;
                        break;
                    case 4:
                        rbtnThinLineBorder.Checked = true;
                        break;
                    default:
                        rbtnBorderless.Checked = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public override string Accept()
        {
            try
            {
                if (rbtnBorderless.Checked)
                {
                    return "1";
                } else if (rbtnOverlappedBorder.Checked)
                {
                    return "2";
                } else if (rbtnSizingBorder.Checked)
                {
                    return "3";
                } else
                {
                    return "4";
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return "";
            }
        }
    }
}
