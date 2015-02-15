using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace speechRecoLib
{
    /// <summary>
    /// Command Manager. Can contain Command or other CommandManager. It's a node.
    /// </summary>
    public class CommandManager
    {
        /// <summary>
        /// Contain the leaf. Each Command can be a node or a terminal node.
        /// </summary>
        private List<CommandManager> _commandList = new List<CommandManager>();

        /// <summary>
        /// List of words. Private, see _word.
        /// </summary>
        protected List<string> _Words;

        /// <summary>
        /// Command for the grammar
        /// </summary>
        public string _command { get; set; }

        private string _regex;

        /// <summary>
        /// Add a Command Object to the CommandManager
        /// </summary>
        /// <param name="commandManager">Command Object to be added</param>
        public void Add(CommandManager commandManager)
        {
            _commandList.Add(commandManager);
        }

        /// <summary>
        /// Add a list of Command Object to the CommandManager
        /// </summary>
        /// <param name="commandManagerList">List of Command Object to be added</param>
        public void Add(List<CommandManager> commandManagerList)
        {
            foreach (CommandManager commandManager in commandManagerList)
            {
                _commandList.Add(commandManager);
            }
        }

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
        public CommandManager(CommandManager commandManager, List<string> words)
        {
            this.Add(commandManager);
            _words = words;
        }
        public CommandManager(List<CommandManager> commandManagerList, List<string> words)
        {
            this.Add(commandManagerList);
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
        public CommandManager(CommandManager commandManager, string command)
        {
            this.Add(commandManager);
            _command = command;
        }
        public CommandManager(List<CommandManager> commandManagerList, string command)
        {
            this.Add(commandManagerList);
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
        public CommandManager(CommandManager commandManager, List<string> words, string command)
        {
            this.Add(commandManager);
            _words = words;
            _command = command;
        }
        public CommandManager(List<CommandManager> commandManagerList, List<string> words, string command)
        {
            this.Add(commandManagerList);
            _command = command;
        }

        /// <summary>
        /// Entry point for CommandManager. Chose if you can send severals command with one sentence.
        /// </summary>
        /// <param name="sentence">This sentence is digested by the decesion tree.</param>
        /// <param name="first">PerformFirst or not</param>
        /// <returns>true if recognised</returns>
        public bool Exec(string sentence, bool first=true)
        {
            bool did_something = false;
            string[] texts = {""};

            if (Thread.CurrentThread.CurrentCulture.Name == "en-US" || Thread.CurrentThread.CurrentCulture.Name == "en-GB")
            {
                texts = sentence.ToLower().Split(new string[] { "and", ",", "." }, StringSplitOptions.None);
            }
            else if (Thread.CurrentThread.CurrentCulture.Name == "fr-FR")
            {
                texts = sentence.ToLower().Split(new string[] { ",", "et", "." }, StringSplitOptions.None);
            }
            

            foreach (string text in texts)
            {
                if (first)
                {
                    if (PerformFirst(text))
                    {
                        did_something = true;
                    }
                }
                else
                {
                    if (Perform(text))
                    {
                        did_something = true;
                    }
                }
            }

            return did_something;
        }

        /// <summary>
        /// Callback for the SpeechManager.
        /// </summary>
        /// <param name="sender">SpeechEngine</param>
        /// <param name="e">Recognised speech</param>
        public void HandleSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("You said: " + e.Result.Text);
            Exec(e.Result.Text);
        }

        /// <summary>
        /// Does the text match the regex?
        /// </summary>
        /// <param name="text">Text recognised</param>
        /// <returns>True if recognised</returns>
        protected bool Matches(string text)
        {
            bool ret = true;

            if (_command != null)
            {
                if (Regex.Matches(text, _command).Count > 0)
                {
                    return true;
                }

                ret = false;
            }

            if (_words != null)
            {
                if (Regex.Matches(text, _regex).Count > 0)
                {
                    return true;
                }

                ret =  false;
            }

            return ret;
        }

        /// <summary>
        /// Decision tree start here.
        /// </summary>
        /// <param name="text">sentence to be analysed</param>
        /// <returns>true if command found</returns>
        protected virtual bool Perform(string text)
        {
            bool did_something = false;

            if (!Matches(text))
            {
                return did_something;
            }

            foreach (CommandManager cmdMan in _commandList)
            {
                if (cmdMan.Perform(text))
                {
                   did_something = true;
                }
            }

            return did_something;
        }

        /// <summary>
        /// Same as Perform but excecute the first command found.
        /// </summary>
        /// <param name="text">sentence to be analysed</param>
        /// <returns>true if command found</returns>
        protected virtual bool PerformFirst(string text)
        {

            if (!Matches(text))
            {
                return false;
            }

            foreach (CommandManager cmdMan in _commandList)
            {
                if (cmdMan.PerformFirst(text))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Create the regex with the list of words.
        /// </summary>
        protected void UpdateRegex()
        {
            if (_words != null)
            {
                _regex = "";
                foreach (string word in _words)
                {
                    _regex += @".*\b" + word;
                }
            }
        }
    }
}
