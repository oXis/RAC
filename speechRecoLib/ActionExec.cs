using speechRecoLib.ExecScript;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speechRecoLib
{
    /// <summary>
    /// Class that execute a program based on its name. Like notepad or chrome.
    /// </summary>
    public class ActionExec : ActionManager
    {
        string _exec = null;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="exec">Name of the script.</param>
        public ActionExec(string exec)
        {
            _exec = exec;
        }

        public override bool Perform()
        {
            Process p = new Process();
            p.StartInfo.FileName = _exec;
            try
            {
                return p.Start();
            }
            catch
            {
                SpeechManager.Speak("Software " + _exec + "not recognised.");
                return false;
            }
        }
    }
}
