using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace speechRecoLib.ExecScript
{
    public static class ActionClose
    {
        internal static class NativeMethods
        {
            [DllImport("user32.dll")]
            internal static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        public static bool Exec()
        {
            IntPtr handle = NativeMethods.GetForegroundWindow();
            const UInt32 WM_CLOSE = 0x0010;

            NativeMethods.SendMessage(handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            return true;
        }
    }
}
