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
        private Dictionary<string ,List<ExternalToolConfig>> myProcessToWatch;
        private List<WindowHandle.Window> myWindowsForWatching;

        public enum ProcessEventTyp
        {
            ProcessStart = 0,
            ProcessEnd = 1,
            BasicData = 2,
            WindowTitle = 3,
            WindowStyle = 4,
            PositionSize = 5,
            WindowExStyle = 6,
            DataText = 7
        }

        public void StartCheck(AWC.Global.GProcessData mGPrc)
        {
            try
            {
                if (mGPrc != null)
                {
                    Log.cLogger.Log("ExternTool checker started");

                    if (myProcessToWatch == null)
                    {
                        myProcessToWatch = new Dictionary<string, List<ExternalToolConfig>>();
                    }
                    
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

        public bool Load(ExternalToolConfig _ExToolConfig)
        {
            try
            {
                if (_ExToolConfig != null && !string.IsNullOrEmpty(_ExToolConfig.ProcessName))
                {
                    if (myProcessToWatch == null)
                    {
                        myProcessToWatch = new Dictionary<string, List<ExternalToolConfig>>();
                    }

                    if (myWindowsForWatching == null)
                        myWindowsForWatching = new List<WindowHandle.Window>();

                    if (!myProcessToWatch.ContainsKey(_ExToolConfig.ProcessName))
                    {
                        myProcessToWatch.Add(_ExToolConfig.ProcessName, new List<ExternalToolConfig>());
                        myProcessToWatch[_ExToolConfig.ProcessName].Add(_ExToolConfig);
                        Log.cLogger.Log(string.Format("Added Processname:'{0}' to the Watching list", _ExToolConfig.ProcessName));
                    } else
                    {
                        if (myProcessToWatch[_ExToolConfig.ProcessName].Contains(_ExToolConfig))
                        {
                            Log.cLogger.Log(string.Format("Process with Eventtyp already added to the Watching list, Processname:'{0}'", _ExToolConfig.ProcessName));
                        } else
                        {
                            myProcessToWatch[_ExToolConfig.ProcessName].Add(_ExToolConfig);
                        }
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

        private string CheckProcessForEvents(string strProcessName, ProcessEventTyp ePrcEventTyp)
        {
            try
            {
                if (!string.IsNullOrEmpty(strProcessName))
                {
                    if (myProcessToWatch != null && myProcessToWatch.ContainsKey(strProcessName))
                    {
                        foreach (ExternalToolConfig _exToolC in myProcessToWatch[strProcessName])
                        {
                            if (ePrcEventTyp == _exToolC.ProcessEventTyp)
                            {
                                return _exToolC.ProcessStartParameter;
                            }
                        }
                        return "";
                    } else
                    {
                        return "";
                    }
                } else
                {
                    return "";
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return "";
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
                if (myProcessToWatch != null && myProcessToWatch.ContainsKey(e.Window.Processname))
                {
                    string _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.ProcessEnd);
                    if (!string.IsNullOrEmpty(_ret))
                    {
                        //Process exit event
                        OnLoadedEvent(e.Window, _ret);
                    }

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
                if (myProcessToWatch != null && myProcessToWatch.ContainsKey(e.Window.Processname))
                {
                    string _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.ProcessStart);
                    if (!string.IsNullOrEmpty(_ret))
                    {
                        //Process exit event
                        OnLoadedEvent(e.Window, _ret);
                    }

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
                if (myWindowsForWatching != null)
                {
                    if (e != null && e.Window != null)
                    {
                        myWindowsForWatching.Add(e.Window);

                        Events_WatchingWindows(e.Window, true);
                    } else
                    {
                        Log.cLogger.Log("Can't add Window to the Watching collection, event or Window is null");
                    }
                } else
                {
                    throw new ArgumentNullException("myWindowsForWatching");
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        protected virtual void OnProcessFound_Removed(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                if (myWindowsForWatching != null)
                {
                    foreach (WindowHandle.Window w in myWindowsForWatching)
                    {
                        if (w != null && e != null && e.Window != null && w.Processname == e.Window.Processname)
                        {
                            Events_WatchingWindows(w, false);
                            w.Dispose();
                            break;
                        }
                    }
                }
                else
                {
                    throw new ArgumentNullException("myWindowsForWatching");
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
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

                    if (myWindowsForWatching != null)
                    {
                        foreach (WindowHandle.Window w in myWindowsForWatching)
                        {
                            if (w != null)
                            {
                                Events_WatchingWindows(w, false);
                                w.Dispose();
                            }
                        }
                        myWindowsForWatching.Clear();
                        myWindowsForWatching = null;
                    }

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

        private void Events_WatchingWindows(WindowHandle.Window mWindow, bool bEnable)
        {
            try
            {
                if (bEnable)
                {
                    mWindow.WindowTitleChanged += Window_WindowTitleChanged;
                    mWindow.WindowStyleChanged += Window_WindowStyleChanged;
                    mWindow.WindowProcessExit += Window_WindowProcessExit;
                    mWindow.WindowPositionSizeChanged += Window_WindowPositionSizeChanged;
                    mWindow.WindowExStyleChanged += Window_WindowExStyleChanged;
                    mWindow.WindowDataTextChanged += Window_WindowDataTextChanged;
                    mWindow.WindowBasicChanged += Window_WindowBasicChanged;
                }
                else
                {
                    mWindow.WindowTitleChanged -= Window_WindowTitleChanged;
                    mWindow.WindowStyleChanged -= Window_WindowStyleChanged;
                    mWindow.WindowProcessExit -= Window_WindowProcessExit;
                    mWindow.WindowPositionSizeChanged -= Window_WindowPositionSizeChanged;
                    mWindow.WindowExStyleChanged -= Window_WindowExStyleChanged;
                    mWindow.WindowDataTextChanged -= Window_WindowDataTextChanged;
                    mWindow.WindowBasicChanged -= Window_WindowBasicChanged;
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void Window_WindowBasicChanged(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                string _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.BasicData);
                if (!string.IsNullOrEmpty(_ret))
                {
                    //Process exit event
                    OnLoadedEvent(e.Window, _ret);
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void Window_WindowDataTextChanged(object sender, WindowHandle.LogEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void Window_WindowExStyleChanged(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                string _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.WindowExStyle);
                if (!string.IsNullOrEmpty(_ret))
                {
                    //Process exit event
                    OnLoadedEvent(e.Window, _ret);
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void Window_WindowPositionSizeChanged(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                string _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.PositionSize);
                if (!string.IsNullOrEmpty(_ret))
                {
                    //Process exit event
                    OnLoadedEvent(e.Window, _ret);
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void Window_WindowProcessExit(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                string _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.ProcessEnd);
                if (!string.IsNullOrEmpty(_ret))
                {
                    //Process exit event
                    OnLoadedEvent(e.Window, _ret);
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void Window_WindowStyleChanged(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                string _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.WindowStyle);
                if (!string.IsNullOrEmpty(_ret))
                {
                    //Process exit event
                    OnLoadedEvent(e.Window, _ret);
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        void Window_WindowTitleChanged(object sender, Public.ProcessEventArgs e)
        {
            try
            {
                string _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.WindowTitle);
                if (!string.IsNullOrEmpty(_ret))
                {
                    //Process exit event
                    OnLoadedEvent(e.Window, _ret);
                }
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        protected virtual void OnLoadedEvent(WindowHandle.Window win, string strStartParam)
        {
            try
            {
                if (win != null)
                {
                    Log.cLogger.Log(string.Format("Loaded event raised for Process:'{0}'", win.Processname));

                    System.Diagnostics.Process.Start(strStartParam);
                } else
                {
                    Log.cLogger.Log("Loaded event can't raised Window data is null");
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

    }
}
