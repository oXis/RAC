using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace speechRecoTest
{
    class ProfileParser
    {
        private CommandManager cmdMan = new CommandManager();
        private List<string> grammar = new List<string>();
        private string _path = "profile/profile.xml";

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">path to the profile.</param>
        public ProfileParser(string path)
        {
            if (path != null)
            {
                _path = path;
            }
        }

        /// <summary>
        /// Parse the profile located at _path.
        /// </summary>
        /// <returns>true if success</returns>
        public bool Parse()
        {
            XElement xelement;

            try
            {
                xelement = XElement.Load(_path, LoadOptions.SetLineInfo);
            }
            catch (XmlException e)
            {
                MessageBox.Show(e.Message, "Error parsing profile " + _path, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            IEnumerable<XElement> nodes = xelement.Elements();

            try
            {
                foreach (XElement node in nodes)
                {
                    if (node.Name.ToString() == "node")
                    {
                        cmdMan.Add(ParseNode(node.Elements()));
                    }
                    else if (node.Name.ToString() == "final")
                    {
                        cmdMan.Add(ParseFinal(node.Elements()));
                    }
                }

                return true;
            }
            catch (ParseErrorException)
            {
                return false;
            }
        }

        /// <summary>
        /// Get the CommandManager created by the parser.
        /// </summary>
        /// <returns>CommandManager</returns>
        public CommandManager GetCmd()
        {
            return cmdMan;
        }

        /// <summary>
        /// Get the grammar created by the parser.
        /// </summary>
        /// <returns>Grammar created</returns>
        public List<string> GetGrammar()
        {
            return grammar;
        }


        /// <summary>
        /// Parse a node, i.e a CommandManager class.
        /// </summary>
        /// <param name="nodes">XML node</param>
        /// <returns>CommandManager created</returns>
        private CommandManager ParseNode(IEnumerable<XElement> nodes)
        {
            CommandManager cmdList = new CommandManager();

            List<string> words = null;

            foreach (XElement node in nodes)
            {
                switch (node.Name.ToString())
                {
                    case "node":
                        cmdList.Add(ParseNode(node.Elements()));
                        break;
                    case "final":
                        cmdList.Add(ParseFinal(node.Elements()));
                        break;
                    case "words":
                        words = node.Value.ToLower().Split(new string[] { " " }, StringSplitOptions.None).OfType<string>().ToList();
                        break;
                    default:
                        IXmlLineInfo info = node;
                        throw new ParseErrorException("The element <" + node.Name + "> is not recognised.\n Line: " + info.LineNumber , _path);
                }
            }

            if (words == null)
            {
                throw new ParseErrorException("Missing element <words> in node <node>", _path);
            }

            return new CommandManager(cmdList, words);
        }

        /// <summary>
        /// Parse a final node, i.e a Command class.
        /// </summary>
        /// <param name="nodes">XML node</param>
        /// <returns>Command created</returns>
        private Command ParseFinal(IEnumerable<XElement> nodes)
        {
            List<string> words = null;
            string command = null;
            string answer = null;
            string play = null;
            Action action = null;

            foreach (XElement node in nodes)
            {
                switch (node.Name.ToString())
                {
                    case "words":
                        words = node.Value.ToLower().Split(new string[] { " " }, StringSplitOptions.None).OfType<string>().ToList();
                        break;
                    case "command":
                        command = node.Value;
                        grammar.Add(node.Value);
                        break;
                    case "answer":
                        answer = node.Value;
                        break;
                    case "play":
                        play = node.Value;
                        break;
                    case "action":
                        action = ParseAction(node.Elements());
                        break;
                    default:
                        IXmlLineInfo info = node;
                        throw new ParseErrorException("The element <" + node.Name + "> is not recognised.\n Line: " + info.LineNumber, _path);
                }
            }

            if ((words == null && command == null) || action == null)
            {
                throw new ParseErrorException("Missing element (<words> or <command> or <action>) in node <final>", _path);
            }

            return new Command(words, command, answer, play, action);
        }

        /// <summary>
        /// Parse a Action node, i.e a Action class.
        /// </summary>
        /// <param name="nodes">XML node</param>
        /// <returns>Action created</returns>
        private Action ParseAction(IEnumerable<XElement> nodes)
        {
            string key = null;
            string keyMod = null;
            string exec = null;

            foreach (XElement node in nodes)
            {
                switch (node.Name.ToString())
                {
                    case "key":
                        key = node.Value;
                        break;
                    case "modifier":
                        keyMod = node.Value;
                        break;
                    case "exec":
                        exec = node.Value;
                        break;
                    default:
                        IXmlLineInfo info = node;
                        throw new ParseErrorException("The element <" + node.Name + "> is not recognised.\n Line: " + info.LineNumber, _path);
                }
            }

            if (key == null && exec == null)
            {
                throw new ParseErrorException("Missing element (<key> or <exec>) in node <action>", _path);
            }

            return new Action(key);
        }

        /// <summary>
        /// Exception in case of error while parsing.
        /// </summary>
        [SerializableAttribute] //remove the warning
        private class ParseErrorException : Exception
        {
            public ParseErrorException()
            {
            }
            public ParseErrorException(string message, string path)
                : base(message)
            {
                MessageBox.Show(message, "Error parsing profile " + path, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}