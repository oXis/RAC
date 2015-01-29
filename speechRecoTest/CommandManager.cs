using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace speechRecoTest
{
    /// <summary>
    /// Command Manager. Can contain Command or other CommandManager. It's a node.
    /// </summary>
    class CommandManager
    {

        /// <summary>
        /// List of words. Private, see _word.
        /// </summary>
        protected List<string> _Words;

        /// <summary>
        /// Command for the grammar
        /// </summary>
        public string _command { get; set; }

        protected string _regex;

        /// <summary>
        /// List of words, update the regex if modified.
        /// </summary>
        public List<string> _words
        {
            get
            {
                return _Words;
            }
            set
            {
                _Words = value;
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
        /// Default.
        /// </summary>
        public CommandManager()
        {
        
        }

        /// <summary>
        /// Constructor.
        /// List of words and action.
        /// </summary>
        /// <param name="words">List of words for the regex. ORDER VERY IMPORTANT</param>
        public CommandManager(List<string> words)
        {
            _words = words;
        }

        /// <summary>
        /// Constructor.
        /// Command and action
        /// </summary>
        /// <param name="command">Command for the grammar</param>
        public CommandManager(string command)
        {
            _command = command;
        }

        /// <summary>
        /// Constructor.
        /// List of word, command and action.
        /// </summary>
        /// <param name="words">List of words for the regex. ORDER VERY IMPORTANT</param>
        /// <param name="command">Command for the grammar</param>
        public CommandManager(List<string> words, string command)
        {
            _words = words;
            _command = command;
        }

        /// <summary>
        /// Does the text match the regex?
        /// </summary>
        /// <param name="text">Text recognised</param>
        /// <returns>True is recognised</returns>
        public bool Matches(string text)
        {
            if (_words != null)
            {
                if (Regex.Matches(text, _regex).Count > 0)
                {
                    return true;
                }
            }

            if (Regex.Matches(text, _command).Count > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Create the regex with the list of words.
        /// </summary>
        protected void UpdateRegex()
        {
            _regex = "";
            foreach(string word in _words)
            {
                _regex += @".*\b" + word;
            }
        }
    }
}
