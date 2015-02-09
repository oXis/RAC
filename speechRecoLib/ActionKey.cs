using InputManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace speechRecoLib
{
    public class ActionKey : ActionManager
    {
        private Keys _key;
        private Keys _keyMod = Keys.None;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="key">Key to be pressed.</param>
        public ActionKey(Keys key)
        {
            _key = key;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="key">Key to be pressed.</param>
        /// <param name="keyMod">Modifier</param>
        public ActionKey(Keys key, Keys keyMod)
        {
            _key = key;
            _keyMod = keyMod;
        }

        /// <summary>
        /// Press the _key (with _keyMod).
        /// </summary>
        /// <returns></returns>
        public override bool Perform()
        {
            if (_keyMod == Keys.None)
            {
                Keyboard.KeyPress(_key);
            } else
            {
                Keyboard.ShortcutKeys(new Keys[] { _keyMod, _key });
            }
            
            return true;
        }
    }
}
