using System;
using System.Windows.Forms;

namespace AWC.Controls
{
    public partial class ucModules : UserControl
    {
        public ucModules()
        {
            InitializeComponent();
        }

        public new void Load(System.Diagnostics.Process mPrc)
        {
            try
            {
                if (mPrc != null)
                {
                    FillGrid(mPrc);
                } else
                {
                    SetDefault();
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public void SetDefault()
        {
            try
            {
                dgvModules.DataSource = null;
                if (dgvModules.Rows != null)
                    dgvModules.Rows.Clear();
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private void FillGrid(System.Diagnostics.Process mPrc)
        {
            try
            {

                if (mPrc != null && mPrc.Modules != null)
                {
                    for (int i = 0; i < mPrc.Modules.Count; i++)
                    {
                        dgvModules.Rows.Add(mPrc.Modules[i].ModuleName, mPrc.Modules[i].FileName, GetMemorySizeString(mPrc.Modules[i].ModuleMemorySize));
                    }
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private string GetMemorySizeString(int mSize)
        {
            try
            {
                decimal calcMem = Math.Round((decimal)(mSize / 1024), 2);
                if (calcMem == 0)
                {
                    //bytes
                    return mSize + " BYTES";
                } else
                {
                    decimal calcMBMem = Math.Round((decimal)(calcMem / 1024), 2);
                    if (calcMem == 0)
                    {
                        //KB
                        return calcMem + " KB";
                    }
                    else
                    {
                        //MB
                        return calcMBMem + " MB";
                    }
                }               
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return string.Empty;
            }
        }
    }
}
