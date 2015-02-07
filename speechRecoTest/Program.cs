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
using System.Diagnostics;


namespace speechRecoTest
{
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

            /*CommandManager cmdMan = new CommandManager();

            cmdMan.Add(new Command(null, "boost", "Powa!" ,new Action("Exec: boost")));
            
            List<CommandManager> cmdList = new List<CommandManager>();
            cmdList.Add(new Command(new List<string> { "speed" }, new Action("Exec: decrease speed")));
            cmdList.Add(new Command(new List<string> { "front", "shield" }, null, "I'm decrasing the frint shield", new Action("Exec: decrease front shield")));
            cmdList.Add(new Command(new List<string> { "back", "shield" }, new Action("Exec: decrease back shield")));

            List<CommandManager> cmdList2 = new List<CommandManager>();
            cmdList2.Add(new Command(new List<string> { "speed" }, null, "I'm increasing the speed", new Action("Exec: increase speed")));
            cmdList2.Add(new Command(new List<string> { "front", "shield" }, new Action("Exec: increase front shield")));
            cmdList2.Add(new Command(new List<string> { "back", "shield" }, new Action("Exec: increase back shield")));

            cmdMan.Add(new CommandManager(cmdList, new List<string> { "decrease"}));
            cmdMan.Add(new CommandManager(cmdList2, new List<string> { "increase"}));
            */

            ProfileParser profile = new ProfileParser("../../profile.xml");
            CommandManager cmd = profile.GetCmd();

            if (cmd.Exec("Please, can you decrease the front shield and increase the speed. Boost."))
            {
                Console.Write("Ok!\n");
            }
            else
            {
                Console.Write("Not Ok!\n");
            }

            SpeechManager.Start(cmd.HandleSpeechRecognized, profile.GetGrammar());

            /*
            Command cmd2 = new Command("increase speed", ref action);
            if (cmd2.Matches("increase speed"))
            {
                Console.Write("Ok!\n");
            }
            */
            /*
            Command cmd = new Command();
            cmd._play = @"C:\Users\Public\Music\Sample Music\Kalimba.mp3";

            Command cmd2 = new Command();
            cmd2._play = @"C:\Users\Public\Music\Sample Music\Sleep Away.mp3";

            cmd.Play();
            Thread.Sleep(1500);
            cmd2.Play();
            
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-GB");
            Console.WriteLine("Parle en Français, dire \"exit\" pour quiter");
            SpeechManager.Start(cmdMan.HandleSpeechRecognized, new List<string> {"shield"});
            */

            ConsoleKeyInfo pressedKey;
            char keychar;
            do
            {
                pressedKey = Console.ReadKey(true);
                keychar = pressedKey.KeyChar;
                Console.WriteLine("You pressed '{0}'", keychar);
            } while (!keychar.Equals('q'));

            SpeechManager.Stop();
        }

        static void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("You said: " + e.Result.Text);
            //Speak(e.Result.Text);
        }
    }
}
