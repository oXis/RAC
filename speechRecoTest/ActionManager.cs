using InputManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace speechRecoTest
{
    /// <summary>
    /// Class to manage action like press key or exec bin.
    /// </summary>
    abstract class ActionManager
    {
        public abstract bool Perform();
    }
}
