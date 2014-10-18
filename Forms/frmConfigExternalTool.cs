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
                mylExConfigs = new List<ExternalToolConfig>();
            else
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
                        dgvExternalToolConfig.Rows.Add(new object[] { mylExConfigs[i].Enable, mylExConfigs[i].ProcessName,ExternTools.ExternalToolConfig.GetStringEventTypValue(mylExConfigs[i].ProcessEventTyp), mylExConfigs[i].ProcessStartParameter });
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
                    _PrcEventType = ExternalToolConfig.GetEnumEventTypValue(strEventtyp);

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
