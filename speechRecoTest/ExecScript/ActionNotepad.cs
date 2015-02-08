using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speechRecoTest.ExecScript
{
    static class ActionNotepad
    {
        public static bool Exec()
        {
            Process p = new Process();
            p.StartInfo.FileName = "notepad.exe";
            p.Start();
            return true;
        }
    }
}
