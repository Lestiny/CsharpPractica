using System;
using System.Collections.Generic;
using System.Linq;

namespace TerminalHistoryApp
{
    public class TerminalHistory
    {
        private Stack<TerminalCommand> stack = new Stack<TerminalCommand>();

        public void Add(string text)
        {
            stack.Push(new TerminalCommand(text));
        }

        public TerminalCommand Remove()
        {
            if (stack.Count > 0)
                return stack.Pop();
            return null;
        }

        public List<TerminalCommand> Find(string word)
        {
            return stack.Where(x => x.CommandText.Contains(word)).ToList();
        }

        public void Print()
        {
            foreach (var c in stack)
                Console.WriteLine(c.CommandText);
        }
    }
}
