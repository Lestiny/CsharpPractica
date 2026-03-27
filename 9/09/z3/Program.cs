using System;
using System.Collections.Generic;
using NotificationSystem;

class Program
{
    static void Main()
    {
        var email = new EmailNotifier<string>();
        var manager = new NotifierManager<string>(email);

        var msgs = new List<string>()
        {
            "Доброе утро",
            "Как Вы поживаете?",
            "Это рассылка"
        };

        manager.SendBulk(msgs);
    }
}
