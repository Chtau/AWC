﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WindowHandle
{
    public class WMSG : FinalizerBase
    {
         [DllImport("user32", EntryPoint = "SetWindowsHookExA")]
        static extern IntPtr SetWindowsHookEx(int idHook, Delegate lpfn, IntPtr hmod, IntPtr dwThreadId);

        [DllImport("user32", EntryPoint = "UnhookWindowsHookEx")]
        private static extern int UnhookWindowsHookEx(IntPtr hHook);

        [DllImport("user32", EntryPoint = "CallNextHookEx")]
        static extern int CallNextHook(IntPtr hHook, int ncode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThreadId();

        IntPtr hook;
        public readonly int HookId;
        public readonly GlobalHookTypes HookType;

        /*public WMSG(GlobalHookTypes Type):this(Type,false)
        {
        }*/

        public WMSG(GlobalHookTypes Type, bool OnThread, IntPtr handle)
        {
            this.HookType = Type;
            this.HookId = (int)Type;
            del = ProcessMessage;
            if (OnThread)
                hook = SetWindowsHookEx(HookId, del, IntPtr.Zero, handle);//GetCurrentThreadId()
            else
            {
                var hmod = IntPtr.Zero; // Marshal.GetHINSTANCE(GetType().Module);
                hook = SetWindowsHookEx(HookId, del, hmod, IntPtr.Zero);
            }

            if (hook == IntPtr.Zero)
            {
                int err = Marshal.GetLastWin32Error();
                if (err != 0)
                    OnHookFailed(err);
            }
        }

        protected virtual void OnHookFailed(int Error)
        {
            //throw TranslateError(Error);
        }

        private const int HC_ACTION = 0;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        private MessageDelegate del;

        private delegate int MessageDelegate(int code, IntPtr wparam, IntPtr lparam);

        private int ProcessMessage(int hookcode, IntPtr wparam, IntPtr lparam)
        {
            if (HC_ACTION == hookcode)
            {
                try
                {
                    //if (Handle(wparam, lparam)) return 1;
                }
                catch { }
            }
            return CallNextHook(hook, hookcode, wparam, lparam);
        }

        //protected abstract bool Handle(IntPtr wparam, IntPtr lparam);



        protected override sealed void OnDispose()
        {
            UnhookWindowsHookEx(hook);
            AfterDispose();
        }

        protected virtual void AfterDispose()
        {
        }

    }



    /// <summary>
    /// Base class to relatively safely register global windows hooks
    /// </summary>
    public abstract class GlobalHookTrapper : FinalizerBase
    {
       

    }

    public enum GlobalHookTypes
    {
        BeforeWindow = 4, //WH_CALLWNDPROC 
        AfterWindow = 12, //WH_CALLWNDPROCRET 
        KeyBoard = 2, //WH_KEYBOARD
        KeyBoard_Global = 13,  //WH_KEYBOARD_LL
        Mouse = 7, //WH_MOUSE
        Mouse_Global = 14, //WH_MOUSE_LL
        JournalRecord = 0, //WH_JOURNALRECORD
        JournalPlayback = 1, //WH_JOURNALPLAYBACK
        ForeGroundIdle = 11, //WH_FOREGROUNDIDLE
        SystemMessages = 6, //WH_SYSMSGFILTER
        MessageQueue = 3, //WH_GETMESSAGE
        ComputerBasedTraining = 5, //WH_CBT 
        Hardware = 8, //WH_HARDWARE 
        Debug = 9, //WH_DEBUG 
        Shell = 10, //WH_SHELL
    }

    public abstract class FinalizerBase : IDisposable
    {
        protected readonly AppDomain domain;
        public FinalizerBase()
        {
            /*System.Windows.Forms.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            domain = AppDomain.CurrentDomain;
            domain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);
            domain.DomainUnload += new EventHandler(domain_DomainUnload);   */         
        }

        private bool disposed;
        public bool IsDisposed{get{return disposed;}}
        public void Dispose()
        {
            if (!disposed)
            {
                GC.SuppressFinalize(this);
                if (domain != null)
                {
                   /* domain.ProcessExit -= new EventHandler(CurrentDomain_ProcessExit);
                    domain.DomainUnload -= new EventHandler(domain_DomainUnload);
                    System.Windows.Forms.Application.ApplicationExit -= new EventHandler(Application_ApplicationExit);*/
                }
                disposed = true;
                OnDispose();
            }
        }

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            Dispose();
        }

        void domain_DomainUnload(object sender, EventArgs e)
        {
            Dispose();
        }

        void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            Dispose();
        }

        protected abstract void OnDispose();
                /// Destructor
        ~FinalizerBase()
        {
            Dispose();
        }
    }

}
