using System;

namespace TerminalHistoryApp
{
    public class TerminalCommand
    {
        public string CommandText { get; set; }
        public DateTime Timestamp { get; set; }

        public TerminalCommand(string text)
        {
            CommandText = text;
            Timestamp = DateTime.Now;
        }
    }
}
