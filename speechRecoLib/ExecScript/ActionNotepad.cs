using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speechRecoLib.ExecScript
{
    /// <summary>
    /// Exemple script. Called by ActionExec.
    /// </summary>
    public static class ActionNotepad
    {
        /// <summary>
        /// Function called by ActionExec.
        /// </summary>
        /// <returns>true if success.</returns>
        public static bool Exec()
        {
            Process p = new Process();
            p.StartInfo.FileName = "notepad";
            return p.Start();
        }
    }
}
