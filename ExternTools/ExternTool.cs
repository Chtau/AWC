using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWC.ExternTools
{
    public class ExternTool:IDisposable
    {
        private AWC.Global.GProcessData myGPrc;
        private bool _Dispose = false;
        private List<string> myProcessToWatch;

        public void StartCheck(AWC.Global.GProcessData mGPrc)
        {
            try
            {
                if (mGPrc != null)
                {
                    Log.cLogger.Log("ExternTool checker started");

                    if (myProcessToWatch == null)
                        myProcessToWatch = new List<string>();
                    
                    myGPrc = mGPrc;

                    Events_GProcessData(true);
                } else
                {
                    throw new ArgumentNullException("mGPrc");
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        private bool Load(string strProcessName)
        {
            try
            {
                if (!string.IsNullOrEmpty(strProcessName))
                {
                    if (myProcessToWatch == null)
                        myProcessToWatch = new List<string>();

                    if (!myProcessToWatch.Contains(strProcessName))
                    {
                        myProcessToWatch.Add(strProcessName);
                        Log.cLogger.Log(string.Format("Added Processname:'{0}' to the Watching list", strProcessName));
                    } else
                    {
                        Log.cLogger.Log(string.Format("Process already added to the Watching list, Processname:'{0}'", strProcessName));
                    }                    

                    return true;
                } else
                {
                    throw new ArgumentNullException("strProcessName");
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return false;
            }
        }

        private void Events_GProcessData(bool bEnable)
        {
            try
            {
                if (myGPrc != null)
                {
                    if (bEnable)
                    {
                        myGPrc.ProcessAdded += myGPrc_ProcessAdded;
                        myGPrc.ProcessRemoved += myGPrc_ProcessRemoved;
                    }
                    else
                    {
                        myGPrc.ProcessAdded -= myGPrc_ProcessAdded;
                        myGPrc.ProcessRemoved -= myGPrc_ProcessRemoved;
                    }
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void myGPrc_ProcessRemoved(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                if (myProcessToWatch.Contains(e.Window.Processname))
                {
                    Log.cLogger.Log(string.Format("Watcher found Process in Collection are getting removed, Processname:'{0}'", e.Window.Processname));

                    OnProcessFound_Removed(sender, e);
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void myGPrc_ProcessAdded(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                if (myProcessToWatch.Contains(e.Window.Processname))
                {
                    Log.cLogger.Log(string.Format("Watcher found Process in Collection are getting added, Processname:'{0}'", e.Window.Processname));

                    OnProcessFound_Added(sender, e);
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        protected virtual void OnProcessFound_Added(object sender, Public.ProcessEventArgs e)
        {
            try
            {

            } catch (Exception ex)
            {

            }
        }

        protected virtual void OnProcessFound_Removed(object sender, Public.ProcessEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        public void Dispose()
        {
            Dispose(_Dispose);
        }

        protected virtual void Dispose(bool mDispose)
        {
            try
            {
                if (!mDispose)
                {
                    mDispose = true;

                    if (myProcessToWatch != null)
                    {
                        myProcessToWatch.Clear();
                        myProcessToWatch = null;
                    }

                    Events_GProcessData(false);

                    myGPrc = null;
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }
    }
}
