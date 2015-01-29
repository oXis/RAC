using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Speech.Synthesis;
using System.Windows.Forms;

using InputManager;
using System.Globalization;
using System.Threading;
using System.Speech.Recognition;


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

            //Express.showMatch("Please, can you increase the speed", @".*\bincrease.*\bspeed");

            //SpeechManager.PrintInstalledVoice();
            //List<VoiceInfo> infos = SpeechManager.GetInstalledVoice();

            /*if (SpeechManager.SelectInstalledVoice("en-GB"))
            {
                SpeechManager.Speak("I love you!");
            }*/

            //Keyboard.ShortcutKeys(new Keys[] { Keys.P });

            /*
            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            Console.WriteLine("Parle en Français, dire \"exit\" pour quiter");
            SpeechManager.Start(HandleSpeechRecognized);
            */

            Command cmd = new Command(new List<string> {"caca", "speed"});
            if (cmd.Matches("Please, can you increase the speed."))
            {
                Console.Write("Ok!\n");
                Console.Write(cmd._Regex);
            }
            else
            {
                Console.Write("Not OK!\n");
            }

            cmd._Words = new List<string> { "increase", "speed" };
            if (cmd.Matches("Please, can you increase the speed."))
            {
                Console.Write("Ok!\n");
                Console.Write(cmd._Words);
            }

            /*
            Command cmd = new Command();
            cmd._play = @"C:\Users\Public\Music\Sample Music\Kalimba.mp3";

            Command cmd2 = new Command();
            cmd2._play = @"C:\Users\Public\Music\Sample Music\Sleep Away.mp3";

            cmd.Play();
            Thread.Sleep(1500);
            cmd2.Play();
            */
            
            /*
            ConsoleKeyInfo pressedKey;
            char keychar;
            do
            {
                pressedKey = Console.ReadKey(true);
                keychar = pressedKey.KeyChar;
                Console.WriteLine("You pressed '{0}'", keychar);
                if (keychar.Equals('s'))
                {
                    cmd.Stop();
                    cmd2.Stop();
                }
            
            } while (!keychar.Equals('q'));
            */
            //SpeechManager.Stop();
            

        }

        static void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("You said: " + e.Result.Text);
            //Speak(e.Result.Text);
        }
    }
}
