using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace speechRecoTest.ExecScript
{
    static class ActionClose
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        public static bool Exec()
        {
            IntPtr handle = GetForegroundWindow();
            const UInt32 WM_CLOSE = 0x0010;

            SendMessage(handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);

            return true;
        }
    }
}
