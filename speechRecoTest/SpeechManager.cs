using System;
using System.Collections.Generic;
using System.Globalization;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Threading;

class SpeechManager
{
    static private SpeechRecognitionEngine _recognizer = null;
    static private SpeechSynthesizer _synthesizer = null;
    static private CultureInfo _culture = null;

    public delegate void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e);

    static SpeechManager()
    {
        _synthesizer = new SpeechSynthesizer();
    }

    #region SpeechRecognitionEngine
    public static void Start(HandleSpeechRecognized f)
    {
        _culture = Thread.CurrentThread.CurrentCulture;

        _recognizer = new SpeechRecognitionEngine(_culture);

        LoadDictationGrammar();
        LoadGammar();

        _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(f);
        _recognizer.SetInputToDefaultAudioDevice();
        _recognizer.RecognizeAsync(RecognizeMode.Multiple);

    }

    public static void Stop()
    {
        if (_recognizer != null)
        {
            _recognizer.Dispose();
        }
    }

    static private void LoadDictationGrammar()
    {
        _recognizer.LoadGrammar(new DictationGrammar());
    }

    static private void LoadGammar()
    {
        GrammarBuilder gb = new GrammarBuilder();
        gb.Culture = _culture;
        gb.Append("exit");
        _recognizer.LoadGrammar(new Grammar(gb));
    }

    /*static void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        Console.WriteLine("You said: " + e.Result.Text);
        //Speak(e.Result.Text);
    }*/
    #endregion SpeechRecognitionEngine

    #region SpeechSynthesizer
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

    static private void OutputVoiceInfo(VoiceInfo info)
    {
        Console.WriteLine("  Name: {0}, culture: {1}, gender: {2}, age: {3}.",
          info.Name, info.Culture, info.Gender, info.Age);
        Console.WriteLine("    Description: {0}", info.Description);
    }
    #endregion SpeechSynthesizer
}