using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.Speech;
using System.Speech.Recognition;

using System.Windows.Forms;
using InputManager;

namespace speechRecoTest
{
    class Express
    {
        public static void showMatch(string text, string expr)
        {
            Console.WriteLine("The Expression: " + expr);
            MatchCollection mc = Regex.Matches(text, expr);
            foreach (Match m in mc)
            {
                Console.WriteLine(m);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Express exp = new Express();

            Express.showMatch("Please, can you increase the speed", @"\bincrease\b");
            System.Threading.Thread.Sleep((int)(3000));
            Keyboard.KeyDown(Keys.H);
            Keyboard.KeyUp(Keys.H);

            Keyboard.KeyDown(Keys.E);
            Keyboard.KeyUp(Keys.E);

            Keyboard.KeyDown(Keys.L);
            Keyboard.KeyUp(Keys.L);

            Keyboard.KeyDown(Keys.L);
            Keyboard.KeyUp(Keys.L);

            Keyboard.KeyDown(Keys.O);
            Keyboard.KeyUp(Keys.O);
        }
    }
}
