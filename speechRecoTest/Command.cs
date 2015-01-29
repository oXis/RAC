using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WMPLib;

namespace speechRecoTest
{
    class Command
    {
        /// <summary>
        /// To play an mp3
        /// </summary>
        static WMPLib.WindowsMediaPlayer _wplayer = new WMPLib.WindowsMediaPlayer();

        /// <summary>
        /// List of words
        /// </summary>
        private List<string> _words;
        /// <summary>
        /// Command for the grammar
        /// </summary>
        public string _command { get; set; }
        /// <summary>
        /// Answer for text2speech
        /// </summary>
        public string _answer {get; set;}
        /// <summary>
        /// mp3 to be played
        /// </summary>
        public string _play { get; set; }

        private string _regex;

        /// <summary>
        /// List of words, update the regex if modified.
        /// </summary>
        public List<string> _Words
        {
            get
            {
                return _words;
            }
            set
            {
                _words = value;
                UpdateRegex();

            }
        }

        /// <summary>
        /// Return the regex.
        /// </summary>
        public string _Regex
        {
            get
            {
                return _regex;
            }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        public Command()
        {
        
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="words">List of words for the regex. ORDER VERY IMPORTANT</param>
        public Command(List<string> words)
        {
            _words = words;
            UpdateRegex();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="words">List of words for the regex. ORDER VERY IMPORTANT</param>
        /// <param name="command">Command for the grammar</param>
        public Command(List<string> words, string command)
        {
            _words = words;
            _command = command;
            UpdateRegex();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="words">List of words for the regex. ORDER VERY IMPORTANT</param>
        /// <param name="command">Command for the grammar</param>
        /// <param name="answer">Answer for text2speech</param>
        public Command(List<string> words, string command, string answer)
        {
            _words = words;
            _command = command;
            _answer = answer;
            UpdateRegex();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="words">List of words for the regex. ORDER VERY IMPORTANT</param>
        /// <param name="command">Command for the grammar</param>
        /// <param name="answer">Answer for text2speech</param>
        /// <param name="play">mp3 to be played</param>
        public Command(List<string> words, string command, string answer, string play)
        {
            _words = words;
            _command = command;
            _answer = answer;
            _play = play;
            UpdateRegex();
        }

        /// <summary>
        /// Does the text match the regex?
        /// </summary>
        /// <param name="text">Text recognised</param>
        /// <returns>True is recognised</returns>
        public bool Matches(string text)
        {
            MatchCollection mc = Regex.Matches(text, _regex);
            if (mc.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Play the mp3 file loaded.
        /// </summary>
        public void Play() 
        {
            _wplayer.URL = _play;
            _wplayer.controls.play();
        }

        /// <summary>
        /// Stop the player.
        /// </summary>
        public void Stop()
        {
            _wplayer.controls.stop();
        }

        /// <summary>
        /// Create the regex with the list of words.
        /// </summary>
        private void UpdateRegex()
        {
            _regex = "";
            foreach(string word in _words)
            {
                _regex += @".*\b" + word;
            }
        }
    }
}
