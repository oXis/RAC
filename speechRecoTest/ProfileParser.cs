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

        public ProfileParser(string path)
        {
            if (path != null)
            {
                _path = path;
            }
        }

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
                        cmdMan.Add(ParseNode(node.Elements(), "  "));
                    }
                    else if (node.Name.ToString() == "final")
                    {
                        cmdMan.Add(ParseFinal(node.Elements(), "  "));
                    }
                }

                return true;
            }
            catch (ParseErrorException)
            {
                return false;
            }

        }

        public CommandManager GetCmd()
        {
            return cmdMan;
        }

        public List<string> GetGrammar()
        {
            return grammar;
        }

        private CommandManager ParseNode(IEnumerable<XElement> nodes, string ind = "")
        {
            CommandManager cmdList = new CommandManager();

            List<string> words = null;

            foreach (XElement node in nodes)
            {
                switch (node.Name.ToString())
                {
                    case "node":
                        cmdList.Add(ParseNode(node.Elements(), ind + "  "));
                        break;
                    case "final":
                        cmdList.Add(ParseFinal(node.Elements(), ind + "  "));
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

        private Command ParseFinal(IEnumerable<XElement> nodes, string ind = "")
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
                        action = ParseAction(node.Elements(), ind + "  ");
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

        private Action ParseAction(IEnumerable<XElement> nodes, string ind = "")
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