using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AWC.ExternTools;

namespace AWC.Forms
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
            {
                mylExConfigs = new List<ExternalToolConfig>();
            }

            if (mylExConfigs.Count == 0)
            {
                if (Save.ConfigFileManager.Load(Save.ConfigFileManager.ConfigFileFullName))
                {
                    mylExConfigs = Save.ConfigFileManager.ExternalToolConfigs;
                }
            }

            LoadExternalToolConfigData(mylExConfigs);
            
        }

        public void LoadExternalToolConfigData(List<ExternalToolConfig> _lExConfigs)
        {
            try
            {
                if (_lExConfigs != null && _lExConfigs.Count > 0)
                {
                    mylExConfigs = _lExConfigs;

                    dgvExternalToolConfig.Rows.Clear();

                    for (int i = 0; i < mylExConfigs.Count; i++)
                    {
                        dgvExternalToolConfig.Rows.Add(new object[] { mylExConfigs[i].Enable, mylExConfigs[i].ProcessName,ExternTools.ExternalToolConfig.GetStringEventTypValue(mylExConfigs[i].ProcessEventTyp),ExternalToolConfig.GetStringEventExecuteTypValue(mylExConfigs[i].ProcessEventExecuteTyp), mylExConfigs[i].ProcessStartParameter });
                    }
                } else
                {
                    if (mylExConfigs == null)
                        mylExConfigs = new List<ExternalToolConfig>();
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
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

                Save.ConfigFileManager.ExternalToolConfigs = mylExConfigs;
                Save.ConfigFileManager.Save(Save.ConfigFileManager.ConfigFileFullName);

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
                string strExeEventtyp = "";
                bool bEnable = false;
                ExternTool.ProcessEventTyp _PrcEventType = ExternTool.ProcessEventTyp.ProcessStart;
                ExternTool.ProcessEventExecuteTyp _PrcExeEventType = ExternTool.ProcessEventExecuteTyp.Command;

                strProcessName = _row.Cells["colProcessName"].Value as string;
                strCommand = _row.Cells["colStartCommand"].Value as string;
                strEventtyp = _row.Cells["colWindowEventTyp"].Value as string;
                strExeEventtyp = _row.Cells["colExecuteEventtyp"].Value as string;
                bEnable = Convert.ToBoolean(_row.Cells["colEnable"].Value);

                if (!string.IsNullOrEmpty(strCommand) && !string.IsNullOrEmpty(strCommand) && !string.IsNullOrEmpty(strEventtyp) && !string.IsNullOrEmpty(strExeEventtyp))
                {
                    _PrcEventType = ExternalToolConfig.GetEnumEventTypValue(strEventtyp);
                    _PrcExeEventType = ExternalToolConfig.GetEnumEventExecuteTypValue(strExeEventtyp);

                    ExternalToolConfig exCon = new ExternalToolConfig(strProcessName, _PrcEventType, strCommand, bEnable, _PrcExeEventType);
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

        private void tsbtnAddRow_Click(object sender, EventArgs e)
        {
            try
            {
                dgvExternalToolConfig.Rows.Add();

            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void tsbtnRemoveRow_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvExternalToolConfig.Rows.Count; i++)
                {
                    if (dgvExternalToolConfig.Rows[i].Selected || dgvExternalToolConfig["colProcessName", i].Selected
                        || dgvExternalToolConfig["colStartCommand", i].Selected || dgvExternalToolConfig["colWindowEventTyp", i].Selected
                        || dgvExternalToolConfig["colEnable", i].Selected || dgvExternalToolConfig["colExecuteEventtyp", i].Selected)
                    {
                        dgvExternalToolConfig.Rows.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void dgvExternalToolConfig_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvExternalToolConfig.Columns["colEditCommand"].Index)
                {
                    OnButtonCommandEdit(dgvExternalToolConfig.Rows[e.RowIndex]);
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        protected virtual void OnButtonCommandEdit(DataGridViewRow dgvRow)
        {
            try
            {
                frmEditExecuteCommand frmEditExe = new frmEditExecuteCommand();
                frmEditExe.ExecuteCommandString = (string)dgvRow.Cells["colStartCommand"].Value;
                frmEditExe.Load(ExternalToolConfig.GetEnumEventExecuteTypValue((string)dgvRow.Cells["colExecuteEventtyp"].Value));
                if (frmEditExe.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    dgvRow.Cells["colStartCommand"].Value = frmEditExe.ExecuteCommandString;
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void dgvExternalToolConfig_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgvExternalToolConfig.Columns["colEditCommand"].Index && e.RowIndex >= 0)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                    e.Graphics.DrawImage(Res.Icons.config_dark_24, e.CellBounds.Left + 2, e.CellBounds.Top + 2, 17, 17);
                    e.Handled = true;
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

    }
}
