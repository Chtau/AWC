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
    public partial class frmEditExecuteCommand : Form
    {
        private string myExecuteCommandString = "";
        private ExternTools.ExternTool.ProcessEventExecuteTyp myPrcExeTyp;
        private Controls.ucEditExecuteCommandBase ucEditExecuteCommandCommand1;

        public string ExecuteCommandString
        {
            get { return myExecuteCommandString; }
            set { myExecuteCommandString = value; }
        }

        public ExternTools.ExternTool.ProcessEventExecuteTyp PrcExeTyp
        {
            get { return myPrcExeTyp; }
        }

        public frmEditExecuteCommand()
        {
            InitializeComponent();
        }

        public new void Load(ExternTools.ExternTool.ProcessEventExecuteTyp prcExeTyp)
        {
            try
            {
                myPrcExeTyp = prcExeTyp;
                switch (myPrcExeTyp)
                {
                    case AWC.ExternTools.ExternTool.ProcessEventExecuteTyp.Command:
                        LoadCommandControl(true, new AWC.Controls.ucEditExecuteCommandCommand());
                        break;
                    case AWC.ExternTools.ExternTool.ProcessEventExecuteTyp.Position:
                        LoadCommandControl(true, new AWC.Controls.ucEditExecuteCommandPosition());
                        break;
                    case AWC.ExternTools.ExternTool.ProcessEventExecuteTyp.Size:
                        LoadCommandControl(true, new AWC.Controls.ucEditExecuteCommandSize());
                        break;
                    case AWC.ExternTools.ExternTool.ProcessEventExecuteTyp.Border:
                        LoadCommandControl(true, new AWC.Controls.ucEditExecuteCommandBorder());
                        break;
                    default:
                        break;
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                myExecuteCommandString = ucEditExecuteCommandCommand1.Accept();
                LoadCommandControl(false, null);

                if (!string.IsNullOrEmpty(myExecuteCommandString))
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.No;
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                this.DialogResult = System.Windows.Forms.DialogResult.No;
            }
        }

        private void LoadCommandControl(bool bEnable, AWC.Controls.ucEditExecuteCommandBase ucEditControl)
        {
            try
            {
                if (bEnable)
                {
                    if (this.ucEditExecuteCommandCommand1 == null)
                    {
                        this.ucEditExecuteCommandCommand1 = ucEditControl;
                        this.ucEditExecuteCommandCommand1.BackColor = System.Drawing.Color.White;
                        this.ucEditExecuteCommandCommand1.Dock = System.Windows.Forms.DockStyle.Fill;
                        this.ucEditExecuteCommandCommand1.Location = new System.Drawing.Point(0, 0);
                        this.ucEditExecuteCommandCommand1.Name = "ucEditExecuteCommandCommand1";
                        this.ucEditExecuteCommandCommand1.Size = new System.Drawing.Size(256, 153);
                        this.ucEditExecuteCommandCommand1.TabIndex = 0;
                    }
                    this.plContent.Controls.Add(this.ucEditExecuteCommandCommand1);
                    if (!this.ucEditExecuteCommandCommand1.Visible)
                        this.ucEditExecuteCommandCommand1.Visible = true;

                    this.ucEditExecuteCommandCommand1.Load(myExecuteCommandString);
                } else
                {
                    if (this.ucEditExecuteCommandCommand1 != null)
                    {
                        this.ucEditExecuteCommandCommand1.Visible = false;
                        this.ucEditExecuteCommandCommand1.Dispose();
                        this.ucEditExecuteCommandCommand1 = null;
                    }
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

    }
}
