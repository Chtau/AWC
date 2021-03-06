﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWC.Global
{
    public class GProcessData
    {
        private System.Threading.Thread myProcessListThread;
        private int myProcessListRetry = 0;
        private int myProcessListSleepTime = 1000;
        private int myGCMemoryMaxDiff = 100000;
        private long myStartGCMemory = 0;
        private System.Diagnostics.Stopwatch mytsGarbageWatch;

        private AWC.WindowHandle.HWNDCollection myHWNDCol;
        private List<WindowHandle.Window> myLastHWNDWindows;

        public event EventHandler<Public.ProcessEventArgs> ProcessRemoved;
        public event EventHandler<Public.ProcessEventArgs> ProcessAdded;

        public AWC.WindowHandle.HWNDCollection HWND
        {
            get { return myHWNDCol; }
        }

        public GProcessData()
        {
            myProcessListRetry = 0;

            //start new refresh Thread
            StartPrcoessListThread();

        }

        #region Process List
        private void StartPrcoessListThread()
        {
            myProcessListThread = new System.Threading.Thread(new System.Threading.ThreadStart(asynRefreshProcessList));
            myProcessListThread.Name = "ProcessListThread";
            myProcessListThread.Start();

            Log.cLogger.Log("Refresh Thread for Process List running");
        }

        private void asynRefreshProcessList()
        {
            try
            {
                bool bfound = true;

                if (myHWNDCol == null)
                    myHWNDCol = new AWC.WindowHandle.HWNDCollection();

                if (myLastHWNDWindows == null)
                    myLastHWNDWindows = new List<WindowHandle.Window>();

                while (myProcessListThread.IsAlive)
                {
                    for (int i = 0; i < myLastHWNDWindows.Count; i++)
                    {
                        myLastHWNDWindows[i].Dispose();
                        myLastHWNDWindows[i] = null;
                    }
                    myLastHWNDWindows.Clear();


                    foreach (WindowHandle.Window w in myHWNDCol.LHWND)
                    {
                        myLastHWNDWindows.Add(w);
                    }

                    myHWNDCol.Load();

                    foreach (WindowHandle.Window wOld in myLastHWNDWindows)
                    {
                        if (wOld != null)
                        {
                            bfound = false;

                            foreach (WindowHandle.Window winNew in myHWNDCol.LHWND)
                            {
                                if (winNew != null)
                                {
                                    if (wOld.Processname == winNew.Processname)
                                    {
                                        bfound = true;
                                    }
                                }
                            }

                            if (!bfound)
                            {
                                //process not found in the new process list
                                //process was removed
                                Log.cLogger.Log(string.Format("Process removed Name:'{0}'", wOld.Processname));
                                OnProcessRemoved(new Public.ProcessEventArgs(wOld));
                            }
                        }
                    }

                    foreach (WindowHandle.Window wind in myHWNDCol.LHWND)
                    {
                        if (wind != null)
                        {
                            bfound = false;

                            foreach (WindowHandle.Window winOld in myLastHWNDWindows)
                            {
                                if (winOld != null)
                                {
                                    if (wind.Processname == winOld.Processname)
                                    {
                                        bfound = true;
                                    }
                                }
                            }

                            if (!bfound)
                            {
                                //process not found in the new process list
                                //process was added
                                Log.cLogger.Log(string.Format("new Process added Name:'{0}'", wind.Processname));
                                OnProcessAdded(new Public.ProcessEventArgs(wind));
                            }
                        }
                    }


                    long _currentGCMemory = GC.GetTotalMemory(true);

                    if (myStartGCMemory != 0)
                    {
                        if (_currentGCMemory > (myStartGCMemory + myGCMemoryMaxDiff))
                        {
                            mytsGarbageWatch.Stop();

                            System.Diagnostics.Debug.Print("Current Garbage:" + (_currentGCMemory - myStartGCMemory) + " Stop Watch:" + mytsGarbageWatch.ElapsedMilliseconds + " Milliseconds");

                            myGCMemoryMaxDiff += 100000;

                            mytsGarbageWatch.Reset();
                            mytsGarbageWatch.Start();
                        }
                    }
                    else
                    {
                        myStartGCMemory = _currentGCMemory;
                        mytsGarbageWatch = new System.Diagnostics.Stopwatch();
                        mytsGarbageWatch.Start();
                    }

                    System.Diagnostics.Debug.Print("GC Memory:" + _currentGCMemory.ToString());

                    System.Threading.Thread.Sleep(myProcessListSleepTime);

                }
            } catch (System.Threading.ThreadAbortException exTA)
            {
                Log.cLogger.Log(exTA, "Abort refresh GProcessData Thread");
            } catch (Exception ex)
            {
                
                if (myProcessListRetry <= 5)
                {
                    myProcessListRetry += 1;
                    Log.cLogger.Log(ex, "Error while in asyncRefreshProcessList retry StartProcessListThread with new Thread Retrycount:" + myProcessListRetry.ToString());
                    StartPrcoessListThread();
                } else
                {
                    Log.cLogger.Log(ex, "Error while in asyncRefreshProcessList retry StartProcessListThread, max retry reached");
                }
            }
        }

        public void InterupRefresh()
        {
            if (myProcessListThread != null)
            {
                try
                {
                    //myProcessListThread.Interrupt();
                    myProcessListThread.Abort();
                } catch (System.Threading.ThreadInterruptedException ex)
                {
                    Log.cLogger.Log(ex,"Process List Refresh Thread Interrupted with Exception");
                }
                catch (System.Threading.ThreadAbortException ex)
                {
                    Log.cLogger.Log(ex, "Process List Refresh Thread Abort with Exception");
                }
                Log.cLogger.Log("Process List Refresh Thread Interrupted");
            }
        }
        #endregion

        protected virtual void OnProcessRemoved(Public.ProcessEventArgs e)
        {
            if (ProcessRemoved != null)
                ProcessRemoved(this, e);
        }

        protected virtual void OnProcessAdded(Public.ProcessEventArgs e)
        {
            if (ProcessAdded != null)
                ProcessAdded(this, e);
        }

    }
}
