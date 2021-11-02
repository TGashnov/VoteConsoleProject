using System;
using System.Collections.Generic;
using System.Text;
using VoteModel;

namespace VoteConsoleProject.UserInterface
{
    abstract class MenuItem
    {
        public ConsoleKey Key { get; }
        public string Label { get; }
        public bool Hidden { get; }

        public MenuItem(ConsoleKey key, string label,
            bool hidden = false)
        {
            Key = key;
            Label = label;
            Hidden = hidden;
        }

        public void Print()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(Key);
            Console.Write(" ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(Label);
        }
    }

    class MenuAction : MenuItem
    {
        public Action Action { get; }

        public MenuAction(ConsoleKey key, string label, Action action,
            bool hidden = false) : base(key, label, hidden)
        {
            Action = action;
        }
    }

    class MenuClose : MenuItem
    {
        public MenuClose(ConsoleKey key, string label,
            bool hidden = false) : base(key, label, hidden) { }
    }
}