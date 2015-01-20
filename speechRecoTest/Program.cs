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
using System.Threading;

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

    class SpeechReco
    {
        static SpeechRecognitionEngine _recognizer = null;
        static ManualResetEvent manualResetEvent = null;
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

        #region Speech recognition with DictationGrammar
        static void SpeechRecognitionWithDictationGrammar()
        {
            _recognizer = new SpeechRecognitionEngine();
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
        #endregion

    }
    class Program
    {
        static void Main(string[] args)
        {
            //Express exp = new Express();

            Express.showMatch("Please, can you increase the speed", @"\bincrease\b");
            System.Threading.Thread.Sleep((int)(3000));

            SpeechReco.Start();
        }
    }
}
