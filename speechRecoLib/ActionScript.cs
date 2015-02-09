using speechRecoLib.ExecScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speechRecoLib
{
    public class ActionScript : ActionManager
    {
        string _script = null;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="script">Name of the script.</param>
        public ActionScript(string script)
        {
            _script = script;
        }

        /// <summary>
        /// Look for the custom script.
        /// </summary>
        /// <returns>true if success.</returns>
        public override bool Perform()
        {
            switch (_script)
            {
                case "script1":
                    return ActionNotepad.Exec();
                case "closeCurrentWindow":
                    return ActionClose.Exec();
            }

            return false;
        }
    }
}
