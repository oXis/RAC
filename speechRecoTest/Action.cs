using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace speechRecoTest
{
    /// <summary>
    /// Class to manage action like press key or exec bin.
    /// </summary>
    class Action
    {
        private string _text;

        public Action(string text)
        {
            _text = text;
        }
        public bool Perform()
        {
            Console.WriteLine(_text);
            return true;
        }
    }
}
