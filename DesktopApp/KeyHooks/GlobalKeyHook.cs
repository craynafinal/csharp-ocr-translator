using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DesktopApp
{
    /// <summary>
    /// GlobalKeyBoardHook from Mort project.
    /// </summary>
    class GlobalKeyHook
    {
        public struct KeyHookContainer
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [DllImport("user32.dll")]
        static extern int CallNextHookEx(IntPtr hhk, int code, int wParam, ref KeyHookContainer lParam);
        [DllImport("user32.dll")]
        static extern IntPtr SetWindowsHookEx(int idHook, LLKeyboardHook callback, IntPtr hInstance, uint theardID);
        [DllImport("user32.dll")]
        static extern bool UnhookWindowsHookEx(IntPtr hInstance);
        [DllImport("kernel32.dll")]
        static extern IntPtr LoadLibrary(string lpFileName);

        public delegate int LLKeyboardHook(int Code, int wParam, ref KeyHookContainer lParam);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_SYSKEYDOWN = 0x0104;
        private const int WM_SYSKEYUP = 0x0105;

        private readonly LLKeyboardHook llkh;
        public List<Keys> HookedKeys = new List<Keys>();

        private IntPtr hook = IntPtr.Zero;

        public event KeyEventHandler KeyDown;
        public event KeyEventHandler KeyUp;

        public GlobalKeyHook()
        {
            llkh = new LLKeyboardHook(HookProc);
        }

        ~GlobalKeyHook() {
            Unhook();
        }

        public void Hook()
        {
            IntPtr hInstance = LoadLibrary("User32");
            hook = SetWindowsHookEx(WH_KEYBOARD_LL, llkh, hInstance, 0);
        }

        public void Unhook()
        {
            UnhookWindowsHookEx(hook);
        }

        public int HookProc(int code, int wParam, ref KeyHookContainer lParam)
        {
            if (code >= 0)
            {
                Keys key = (Keys)lParam.vkCode;

                if (HookedKeys.Contains(key))
                {
                    KeyEventArgs kArg = new KeyEventArgs(key);
                    if ((wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN) && (KeyDown != null))
                        KeyDown(this, kArg);
                    else if ((wParam == WM_KEYUP || wParam == WM_SYSKEYUP) && (KeyUp != null))
                        KeyUp(this, kArg);
                    if (kArg.Handled)
                        return 1;
                }
            }

            return CallNextHookEx(hook, code, wParam, ref lParam);
        }
    }
}
