using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using TA_Window_API;

namespace AWC.HWND
{
    class HWNDList
    {
        private static WindowHandle.HWNDCollection myHWNDList;

        public static WindowHandle.HWNDCollection GetList()
        {
            try
            {
                Reload();
                return myHWNDList;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return null;
            }
        }

        public static WindowHandle.HWNDCollection GetList(bool bReload)
        {
            try
            {
                if (bReload)
                {
                    Reload();
                }
                
                return myHWNDList;
            }
            catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return null;
            }
        }

        public static void Reload()
        {
            try
            {
                Clear();
                if (myHWNDList == null)
                    myHWNDList = new WindowHandle.HWNDCollection();
                myHWNDList.Load();
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public static void Clear()
        {
            try
            {
                if (myHWNDList != null && myHWNDList.LHWND != null)
                {
                    foreach (WindowHandle.Window item in myHWNDList.LHWND)
                    {
                        item.Dispose();                      
                    }
                    myHWNDList = null;
                }

            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
            }
        }

        public static WindowHandle.Window FindHWNDByIntPTR(IntPtr mI)
        {
            try
            {
                foreach (WindowHandle.Window item in myHWNDList.LHWND)
                {
                    if (item.Handle == mI)
                    {
                        return item;
                    }
                }
                return null;
            } catch (Exception ex)
            {
                Log.cLogger.Log(ex);
                return null;
            }
        }
    }
}
