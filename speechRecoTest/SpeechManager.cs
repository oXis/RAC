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

    static private List<string> _sentenceBuffer = new List<string>();

    public delegate void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e);

    /// <summary>
    /// Initiate the SpeechSynthesizer. You can call text2speech functions without starting the main recognition engine.
    /// </summary>
    static SpeechManager()
    {
        _synthesizer = new SpeechSynthesizer();
    }

    #region SpeechRecognitionEngine

    /// <summary>
    /// Start the speech recognition engine, load the dictation grammar and the grammar. Set the callback for SpeechRecognised.
    /// </summary>
    /// <param name="f">callback for SpeechRecognised</param>
    /// <param name="words">grammar</param>
    public static void Start(HandleSpeechRecognized f, List<string> words = null)
    {
        _culture = Thread.CurrentThread.CurrentCulture;

        _recognizer = new SpeechRecognitionEngine(_culture);

        LoadDictationGrammar();

        if (words != null)
        {
            LoadGammar(words);
        }

        _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(f);
        _recognizer.SetInputToDefaultAudioDevice();
        _recognizer.RecognizeAsync(RecognizeMode.Multiple);

    }

    /// <summary>
    /// Stop the speech recognition engine.
    /// </summary>
    public static void Stop()
    {
        if (_recognizer != null)
        {
            _recognizer.Dispose();
        }
    }

    /// <summary>
    /// Load the dicatation grammar.
    /// </summary>
    static private void LoadDictationGrammar()
    {
        _recognizer.LoadGrammar(new DictationGrammar());
    }

    /// <summary>
    /// Load the grammar contained in words.
    /// </summary>
    /// <param name="words">List of word to load into the grammar builder</param>
    static private void LoadGammar(List<string> words)
    {
        GrammarBuilder gb = new GrammarBuilder();
        gb.Culture = _culture;
        foreach (string word in words)
        {
            gb.Append(word);
        }
        _recognizer.LoadGrammar(new Grammar(gb));
    }

    /*
    static void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
    {
        Console.WriteLine("You said: " + e.Result.Text);
        //Speak(e.Result.Text);
    }
    */

    #endregion SpeechRecognitionEngine

    #region SpeechSynthesizer

    /// <summary>
    /// Text2Speech function.
    /// </summary>
    public static void Speak(string sentence)
    {
        _synthesizer.SpeakAsync(sentence);
    }

    /// <summary>
    /// Print all the installed voices in stdout.
    /// </summary>
    /// <returns>Number of voices</returns>
    public static int PrintInstalledVoice()
    {
        List<VoiceInfo> infos = new List<VoiceInfo>();

        foreach (InstalledVoice voice in _synthesizer.GetInstalledVoices())
        {
            OutputVoiceInfo(voice.VoiceInfo);
            infos.Add(voice.VoiceInfo);
        }

        return infos.Count;
    }

    /// <summary>
    /// Select an installed voice.
    /// </summary>
    /// <param name="i">default 0, select the voice number i.</param>
    /// <returns>True if the voice i can be selected, false otherwise.</returns>
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

    /// <summary>
    /// Select an installed voice, with the Culture parameter.
    /// </summary>
    /// <param name="culture">Can be en-US, en-GB, fr-FR. Depends on installed culture.</param>
    /// <param name="i">default 0, select the voice number i.</param>
    /// <returns>True if the voice i can be selected, false otherwise.</returns>
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

    /// <summary>
    /// Select installed voice based on voice info
    /// </summary>
    /// <param name="info">Voice info.</param>
    public static void SelectInstalledVoice(VoiceInfo info)
    {
        _synthesizer.SelectVoice(info.Name);
    }

    /// <summary>
    /// Return the list of installed voices.
    /// </summary>
    /// <returns>List of voice</returns>
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

    /// <summary>
    /// Print all infos in stdout.
    /// </summary>
    /// <param name="info">Voice info</param>
    static private void OutputVoiceInfo(VoiceInfo info)
    {
        Console.WriteLine("  Name: {0}, culture: {1}, gender: {2}, age: {3}.",
          info.Name, info.Culture, info.Gender, info.Age);
        Console.WriteLine("    Description: {0}", info.Description);
    }
    #endregion SpeechSynthesizer
}