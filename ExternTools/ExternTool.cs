using System;
using System.Collections.Generic;

namespace AWC.ExternTools
{
    public class ExternTool:IDisposable
    {
        private AWC.Global.GProcessData myGPrc;
        private bool _Dispose = false;
        private Dictionary<string ,List<ExternalToolConfig>> myProcessToWatch;
        private List<WindowHandle.Window> myWindowsForWatching;
        private List<ExternalToolConfig> myExternalToolConfig;

        public List<ExternalToolConfig> ExternalToolConfig
        {
            get { return myExternalToolConfig; }
        }

        public Dictionary<string, List<ExternalToolConfig>> ProcessToWatch
        {
            get { return myProcessToWatch; }
        }

        /// <summary>
        /// eventtyp for which a Window should raise a Event
        /// </summary>
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

        public enum ProcessEventExecuteTyp
        {
            Command = 0,
            Position = 1,
            Size = 2,
            Border = 3
        }

        /// <summary>
        /// starts the checker function for the loaded processes
        /// </summary>
        /// <param name="mGPrc"></param>
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
                    myGPrc.HWND.Load();

                    Events_GProcessData(true);

                    WindowToWatch();
                } else
                {
                    throw new ArgumentNullException("mGPrc");
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        /// <summary>
        /// loads a collection of configurated process to watch
        /// </summary>
        /// <param name="_lExToolConfig"></param>
        /// <returns></returns>
        public bool Load(List<ExternalToolConfig> _lExToolConfig)
        {
            try
            {
                myExternalToolConfig = _lExToolConfig;

                if (_lExToolConfig != null && _lExToolConfig.Count > 0)
                {
                    if (myProcessToWatch != null)
                        myProcessToWatch.Clear();

                    foreach (ExternalToolConfig _exconf in _lExToolConfig)
                    {
                        Load(_exconf);
                    }
                    return true;
                }
                else
                    return false;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return false;
            }
        }

        private bool Load(ExternalToolConfig _ExToolConfig)
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

        private ExternalToolConfig CheckProcessForEvents(string strProcessName, ProcessEventTyp ePrcEventTyp)
        {
            try
            {
                if (!string.IsNullOrEmpty(strProcessName))
                {
                    if (myProcessToWatch != null && myProcessToWatch.ContainsKey(strProcessName))
                    {
                        foreach (ExternalToolConfig _exToolC in myProcessToWatch[strProcessName])
                        {
                            if (ePrcEventTyp == _exToolC.ProcessEventTyp && _exToolC.Enable)
                            {
                                return _exToolC;
                            }
                        }
                        return null;
                    } else
                    {
                        return null;
                    }
                } else
                {
                    return null;
                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// looks if a Process exist already which is in the Watcher Config and add it's Window for the Watcher
        /// </summary>
        private void WindowToWatch()
        {
            try
            {
                if (myGPrc != null)
                {
                    bool _bWinFound = false;

                    WindowHandle.Window[] _AWin = new WindowHandle.Window[myGPrc.HWND.LHWND.Count];
                    myGPrc.HWND.LHWND.CopyTo(_AWin);

                    for (int i = 0; i < _AWin.Length; i++)
                    {
                        _bWinFound = false;
                        if (myProcessToWatch.ContainsKey(_AWin[i].Processname))
                        {
                            foreach (AWC.WindowHandle.Window _WinIn in myWindowsForWatching)
                            {
                                if (_WinIn.Processname == _AWin[i].Processname)
                                {
                                    _bWinFound = true;
                                    break;
                                }
                            }
                            if (!_bWinFound)
                            {
                                //add Window to the Window for Watch
                                myWindowsForWatching.Add(_AWin[i]);
                                Events_WatchingWindows(_AWin[i], true);
                                Log.cLogger.Log(string.Format("Add Window from Process:{0} to Windows for Watching list", _AWin[i].Processname));
                            }
                        }
                    }
                    if (_AWin != null)
                    {
                        for (int i = 0; i < _AWin.Length; i++)
                        {
                            _AWin[i].Dispose();
                            _AWin[i] = null;
                        }
                        _AWin = null;
                    }

                }
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
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
                    ExternalToolConfig _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.ProcessEnd);
                    if (_ret != null)
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
                    ExternalToolConfig _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.ProcessStart);
                    if (_ret != null)
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

        /// <summary>
        /// If a Window is added or removed to the Watching collection
        /// </summary>
        /// <param name="mWindow">WindowHandle.Window to from a Process</param>
        /// <param name="bEnable">Enable or Disable the WindowHandle.Window</param>
        private void Events_WatchingWindows(WindowHandle.Window mWindow, bool bEnable)
        {
            try
            {
                if (bEnable)
                {
                    mWindow.WindowRefreshThread(true);

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
                    mWindow.WindowRefreshThread(false);

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
                ExternalToolConfig _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.BasicData);
                if (_ret != null)
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
                ExternalToolConfig _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.WindowExStyle);
                if (_ret != null)
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
                ExternalToolConfig _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.PositionSize);
                if (_ret != null)
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
                ExternalToolConfig _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.ProcessEnd);
                if (_ret != null)
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
                ExternalToolConfig _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.WindowStyle);
                if (_ret != null)
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
                ExternalToolConfig _ret = CheckProcessForEvents(e.Window.Processname, ProcessEventTyp.WindowTitle);
                if (_ret != null)
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

        /// <summary>
        /// handle/execute the Event for a Window
        /// </summary>
        /// <param name="win">the Window which raised the event</param>
        /// <param name="exConfig">the Parameter for the execution</param>
        protected virtual void OnLoadedEvent(WindowHandle.Window win, ExternalToolConfig exConfig)
        {
            try
            {
                if (win != null)
                {
                    Log.cLogger.Log(string.Format("Loaded event raised for Process:'{0}'", win.Processname));

                    switch (exConfig.ProcessEventExecuteTyp)
                    {
                        case ProcessEventExecuteTyp.Command:
                            Log.cLogger.Log(string.Format("Command execute event , Process:'{0}' ParamString:'{1}'", win.Processname, exConfig.ProcessStartParameter));
                            System.Diagnostics.Process.Start(exConfig.ProcessStartParameter);
                            break;
                        case ProcessEventExecuteTyp.Position:
                            string[] strPos = exConfig.ProcessStartParameter.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);                         
                            int iXCor = win.Rectangle.X;
                            int iYCor = win.Rectangle.Y;
                            if (strPos.Length > 0)
                            {
                                for (int i = 0; i < strPos.Length; i++)
                                {
                                    if (strPos[i].StartsWith("X:"))
                                    {
                                        iXCor = Convert.ToInt32(strPos[i].Replace("X:", "").Trim());
                                    }
                                    else if (strPos[i].StartsWith("Y:"))
                                    {
                                        iYCor = Convert.ToInt32(strPos[i].Replace("Y:", "").Trim());
                                    } else
                                    {
                                        Log.cLogger.Log(string.Format("Position execute event has a unkown parameter, Process:'{0}' ParamString:'{1}' Current Param:'{2}'", win.Processname, exConfig.ProcessStartParameter, strPos[i]));
                                    }
                                }

                                Log.cLogger.Log(string.Format("Position execute event change Value (new X Cor:'{2}', new Y Cor:'{3}'), Process:'{0}' ParamString:'{1}'", win.Processname, exConfig.ProcessStartParameter, iXCor.ToString(), iYCor.ToString()));
                                win.SetSizeLocation(iXCor, iYCor, win.Rectangle.Width, win.Rectangle.Height);
                            } else
                            {
                                Log.cLogger.Log(string.Format("Position execute event has no arguments, Process:'{0}' ParamString:'{1}'", win.Processname, exConfig.ProcessStartParameter));
                            }
                            
                            break;
                        case ProcessEventExecuteTyp.Size:
                            string[] strSize = exConfig.ProcessStartParameter.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);                         
                            int iWidth = win.Rectangle.Width;
                            int iHeight = win.Rectangle.Height;
                            if (strSize.Length > 0)
                            {
                                for (int i = 0; i < strSize.Length; i++)
                                {
                                    if (strSize[i].StartsWith("H:"))
                                    {
                                        iHeight = Convert.ToInt32(strSize[i].Replace("H:", "").Trim());
                                    } else if (strSize[i].StartsWith("W:"))
                                    {
                                        iWidth = Convert.ToInt32(strSize[i].Replace("W:", "").Trim());
                                    } else
                                    {
                                        Log.cLogger.Log(string.Format("Size execute event has a unkown parameter, Process:'{0}' ParamString:'{1}' Current Param:'{2}'", win.Processname, exConfig.ProcessStartParameter, strSize[i]));
                                    }
                                }

                                Log.cLogger.Log(string.Format("Size execute event change Value (new Width:'{2}', new Height:'{3}'), Process:'{0}' ParamString:'{1}'", win.Processname, exConfig.ProcessStartParameter, iWidth.ToString(), iHeight.ToString()));
                                win.SetSizeLocation(win.Rectangle.X, win.Rectangle.Y, iWidth, iHeight);
                            } else
                            {
                                Log.cLogger.Log(string.Format("Size execute event has no arguments, Process:'{0}' ParamString:'{1}'", win.Processname, exConfig.ProcessStartParameter));
                            }
                            break;
                        case ProcessEventExecuteTyp.Border:
                            int iBorder = Convert.ToInt32(exConfig.ProcessStartParameter);
                            IntPtr ipBorder = IntPtr.Zero;
                            switch (iBorder)
	                        {
                                case 1:
                                    ipBorder = (IntPtr)WindowHandle.Enums.WindowStyles.MAXIMIZEBOX;
                                    break;
                                case 2:
                                    ipBorder = (IntPtr)WindowHandle.Enums.WindowStyles.OVERLAPPEDWINDOW;
                                    break;
                                case 3:
                                    ipBorder = (IntPtr)WindowHandle.Enums.WindowStyles.SIZEFRAME;
                                    break;
                                case 4:
                                    ipBorder = (IntPtr)WindowHandle.Enums.WindowStyles.BORDER;
                                    break;
                                default:
                                    ipBorder = (IntPtr)WindowHandle.Enums.WindowStyles.MAXIMIZEBOX;
                                    break;
	                        }

                            Log.cLogger.Log(string.Format("Border execute event change Value (new Bordertyp:'{2}' Border IntPtr:'{3}'), Process:'{0}' ParamString:'{1}'", win.Processname, exConfig.ProcessStartParameter, iBorder.ToString(), ipBorder.ToString()));
                            win.SetBorderStyle(ipBorder);
                            break;
                        default:
                            Log.cLogger.Log(string.Format("Command execute event , Process:'{0}' ParamString:'{1}'", win.Processname, exConfig.ProcessStartParameter));
                            System.Diagnostics.Process.Start(exConfig.ProcessStartParameter);
                            break;
                    }
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
