using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;

using System.Globalization;
using System.Threading;

using speechRecoLib;
using System.Speech.Recognition;

namespace RobertArtificialCopilot
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());
            
        }

        static void Start()
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
  
        }

        static void stop()
        {
            SpeechManager.Stop();
        }

        static void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("You said: " + e.Result.Text);
            //Speak(e.Result.Text);
        }
    }
}
