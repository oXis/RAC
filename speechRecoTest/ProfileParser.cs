using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace speechRecoTest
{
    class ProfileParser
    {
        private CommandManager cmdMan = new CommandManager();
        private List<string> grammar = new List<string>();

        public ProfileParser(string path)
        {

            XElement xelement = XElement.Load(path);
            IEnumerable<XElement> nodes = xelement.Elements();

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
        }

        public CommandManager GetCmd() {
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
                        Console.WriteLine("Unknow command");
                        break;
                }
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
                        Console.WriteLine("Unknow command");
                        break;
                }
            }

            return new Command(words, command, answer, play, action);
        }

        private Action ParseAction(IEnumerable<XElement> nodes, string ind = "")
        {
            string key = null;
            string exec = null;

            foreach (XElement node in nodes)
            {
                switch (node.Name.ToString())
                {
                    case "key":
                        key = node.Value;
                        break;
                    case "exec":
                        exec = node.Value;
                        break;
                    default:
                        Console.WriteLine("Unknow command");
                        break;
                }
            }

            return new Action(key);
        }
    }
}