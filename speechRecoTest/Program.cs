using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.Speech;
using System.Speech.Recognition;
using System.Globalization;

using System.Windows.Forms;

using InputManager;
using System.Threading;
using System.Speech.Synthesis;

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

    class SpeechManager
    {
        static SpeechRecognitionEngine _recognizer;
        static SpeechSynthesizer _synthesizer;

        static ManualResetEvent manualResetEvent = null;

        static SpeechManager()
        {
            _recognizer = new SpeechRecognitionEngine();
            _synthesizer = new SpeechSynthesizer();

            
        }

        public static void Start()
        {
            manualResetEvent = new ManualResetEvent(false);

            SpeechRecognitionWithDictationGrammar();

            manualResetEvent.WaitOne();

            if (_recognizer != null)
            {
                _recognizer.Dispose();
            }
        }

        public static void Speak(string sentence)
        {
            _synthesizer.Speak(sentence);
        }

        public static void InstalledVoice(string culture)
        {
            foreach (InstalledVoice voice in
            _synthesizer.GetInstalledVoices(new CultureInfo(culture)))
            {
                VoiceInfo info = voice.VoiceInfo;
                OutputVoiceInfo(info);
            }
        }

        private static void OutputVoiceInfo(VoiceInfo info)
        {
            Console.WriteLine("  Name: {0}, culture: {1}, gender: {2}, age: {3}.",
              info.Name, info.Culture, info.Gender, info.Age);
            Console.WriteLine("    Description: {0}", info.Description);
        }

        static void SpeechRecognitionWithDictationGrammar()
        {
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("exit")));
            _recognizer.LoadGrammar(new DictationGrammar());
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(speechRecognitionWithDictationGrammar_SpeechRecognized);
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        static void speechRecognitionWithDictationGrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "exit")
            {
                manualResetEvent.Set();
                return;
            }
            Console.WriteLine("You said: " + e.Result.Text);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Express exp = new Express();

            Express.showMatch("Please, can you increase the speed", @"\bincrease\b");

            //Speech.Start();
            SpeechManager.Speak("Will you mary me?");
            SpeechManager.InstalledVoice("en-US");
        }
    }
}
