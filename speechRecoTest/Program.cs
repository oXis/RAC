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
            //Express exp = new Express();

            Express.showMatch("Please, can you increase the speed", @"\bincrease\b");

            //Speech.Start();

            SpeechManager.CheckInstalledVoice();
            List<VoiceInfo> infos = SpeechManager.GetInstalledVoice();

            Console.WriteLine("\n\n");

            /*if (SpeechManager.SelectInstalledVoice("en-GB"))
            {
                SpeechManager.Speak("I love you!");
            }
            if (SpeechManager.SelectInstalledVoice("en-US", 1))
            {
                SpeechManager.Speak("I love you!");
            }
            if (SpeechManager.SelectInstalledVoice("fr-FR"))
            {
                SpeechManager.Speak("Je t'aime !");
            }
            if (SpeechManager.SelectInstalledVoice("es-ES"))
            {
                SpeechManager.Speak("Te quiero !");
            }
            if (SpeechManager.SelectInstalledVoice("pl-PL"))
            {
                SpeechManager.Speak("Kocham cię !");
            }*/

            //Keyboard.ShortcutKeys(new Keys[] { Keys.P });

            Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
            Console.WriteLine("Parle en Français, dire \"exit\" pour quiter");
            SpeechManager.Start(HandleSpeechRecognized);

            ConsoleKeyInfo pressedKey = Console.ReadKey(true);
            char keychar = pressedKey.KeyChar;
            do
            {
                pressedKey = Console.ReadKey(true);
                keychar = pressedKey.KeyChar;
                Console.WriteLine("You pressed '{0}'", keychar);

            } while (!keychar.Equals('q'));

            SpeechManager.Stop();

            /*Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
            Console.WriteLine("Speak in English, say \"exit\" to quit");
            SpeechManager.Start();*/
        }

        static void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("You said: " + e.Result.Text);
            //Speak(e.Result.Text);
        }
    }
}
