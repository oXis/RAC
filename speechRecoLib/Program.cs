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


namespace speechRecoLib
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            //SpeechManager.PrintInstalledVoice();
            List<VoiceInfo> infos = SpeechManager.GetInstalledVoice();

            foreach (VoiceInfo info in infos)
            {
                Console.WriteLine(info.Description);
            }

            ProfileParser profile = new ProfileParser("../../profile.xml");

            if (!profile.Parse())
            {
                Console.WriteLine("caca");
                System.Environment.Exit(-10);   
            }
            
            CommandManager cmd = profile.GetCmd();

            SpeechManager.Start(cmd.HandleSpeechRecognized, profile.GetGrammar());

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
            cmd.Exec(e.Result.Text);
        }
    }
}
