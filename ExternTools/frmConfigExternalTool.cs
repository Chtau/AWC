﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AWC.ExternTools
{
    public partial class frmConfigExternalTool : Form
    {
        public event EventHandler ConfigDataChanged;

        private List<ExternalToolConfig> mylExConfigs;

        public List<ExternalToolConfig> ExToolConfig
        {
            get { return mylExConfigs; }
        }

        public frmConfigExternalTool()
        {
            InitializeComponent();

            if (mylExConfigs == null)
                mylExConfigs = new List<ExternalToolConfig>();
        }

        private void tsbtnSaveConfig_Click(object sender, EventArgs e)
        {
            try
            {
                dgvExternalToolConfig.EndEdit();

                mylExConfigs.Clear();

                for (int i = 0; i < dgvExternalToolConfig.Rows.Count; i++)
                {
                    GetConfigFromRow(dgvExternalToolConfig.Rows[i]);
                }

                OnConfigDataChanged();
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        protected virtual void OnConfigDataChanged()
        {
            try
            {
                if (ConfigDataChanged != null)
                    ConfigDataChanged(this, new EventArgs());
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void GetConfigFromRow(DataGridViewRow _row)
        {
            try
            {
                string strProcessName = "";
                string strCommand = "";
                string strEventtyp = "";
                bool bEnable = false;
                ExternTool.ProcessEventTyp _PrcEventType = ExternTool.ProcessEventTyp.ProcessStart;

                strProcessName = _row.Cells["colProcessName"].Value as string;
                strCommand = _row.Cells["colStartCommand"].Value as string;
                strEventtyp = _row.Cells["colEventTyp"].Value as string;
                bEnable = Convert.ToBoolean(_row.Cells["colEnable"].Value);

                if (!string.IsNullOrEmpty(strCommand) && !string.IsNullOrEmpty(strCommand) && !string.IsNullOrEmpty(strEventtyp))
                {
                    switch (strEventtyp)
	                {
                        case "Start":
                            _PrcEventType = ExternTool.ProcessEventTyp.ProcessStart;
                            break;
                        case "End":
                            _PrcEventType = ExternTool.ProcessEventTyp.ProcessEnd;
                            break;
                        case "Basicdata":
                            _PrcEventType = ExternTool.ProcessEventTyp.BasicData;
                            break;
                        case "Title":
                            _PrcEventType = ExternTool.ProcessEventTyp.WindowTitle;
                            break;
                        case "Style":
                            _PrcEventType = ExternTool.ProcessEventTyp.WindowStyle;
                            break;
                        case "PositionSize":
                            _PrcEventType = ExternTool.ProcessEventTyp.PositionSize;
                            break;
                        case "ExStyle":
                            _PrcEventType = ExternTool.ProcessEventTyp.WindowExStyle;
                            break;
                        case "Datatext":
                            _PrcEventType = ExternTool.ProcessEventTyp.DataText;
                            break;
		                default:
                            _PrcEventType = ExternTool.ProcessEventTyp.ProcessStart;
                            break;
	                }

                    ExternalToolConfig exCon = new ExternalToolConfig(strProcessName, _PrcEventType, strCommand, bEnable);
                    if (exCon != null)
                    {
                        mylExConfigs.Add(exCon);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }
    }
}
