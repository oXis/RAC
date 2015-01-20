using System;
using System.Collections.Generic;
using System.Globalization;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;

class SpeechManager
{
    static SpeechRecognitionEngine _recognizer = null;
    static SpeechSynthesizer _synthesizer = null;
    static CultureInfo _culture = null;
    
    static ManualResetEvent manualResetEvent = null;

    static SpeechManager()
    {
        _synthesizer = new SpeechSynthesizer();
    }

    public static void Start()
    {
        _culture = Thread.CurrentThread.CurrentCulture;

        _recognizer = new SpeechRecognitionEngine(_culture);
        Console.WriteLine(_culture.Name);
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

    public static int CheckInstalledVoice()
    {
        List<VoiceInfo> infos = new List<VoiceInfo>();

        foreach (InstalledVoice voice in _synthesizer.GetInstalledVoices())
        {
            OutputVoiceInfo(voice.VoiceInfo);
            infos.Add(voice.VoiceInfo);
        }

        return infos.Count;
    }

    public static bool SelectInstalledVoice(int i = 0)
    {
        List<VoiceInfo> infos = new List<VoiceInfo>();

        foreach (InstalledVoice voice in
        _synthesizer.GetInstalledVoices(_culture))
        {
            //OutputVoiceInfo(voice.VoiceInfo);
            infos.Add(voice.VoiceInfo);
        }

        if (infos.Count > 0 && i < infos.Count)
        {
            _synthesizer.SelectVoice(infos[i].Name);
            return true;
        }

        return false;
    }

    public static bool SelectInstalledVoice(string culture, int i = 0)
    {
        List<VoiceInfo> infos = new List<VoiceInfo>();

        foreach (InstalledVoice voice in
        _synthesizer.GetInstalledVoices(new CultureInfo(culture)))
        {
            //OutputVoiceInfo(voice.VoiceInfo);
            infos.Add(voice.VoiceInfo);
        }

        if (infos.Count > 0 && i < infos.Count)
        {
            _synthesizer.SelectVoice(infos[i].Name);
            return true;
        }

        return false;
    }

    public static void SelectInstalledVoice(VoiceInfo info)
    {
        _synthesizer.SelectVoice(info.Name);
    }

    public static List<VoiceInfo> GetInstalledVoice()
    {
        List<VoiceInfo> infos = new List<VoiceInfo>();

        foreach (InstalledVoice voice in _synthesizer.GetInstalledVoices())
        {
            //OutputVoiceInfo(voice.VoiceInfo);
            infos.Add(voice.VoiceInfo);
        }

        return infos;
    }

    private static void OutputVoiceInfo(VoiceInfo info)
    {
        Console.WriteLine("  Name: {0}, culture: {1}, gender: {2}, age: {3}.",
          info.Name, info.Culture, info.Gender, info.Age);
        Console.WriteLine("    Description: {0}", info.Description);
    }

    static void SpeechRecognitionWithDictationGrammar()
    {
        GrammarBuilder gb = new GrammarBuilder();
        gb.Culture = _culture;
        gb.Append("exit");
        _recognizer.LoadGrammar(new Grammar(gb));
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
        //Speak(e.Result.Text);
    }
}