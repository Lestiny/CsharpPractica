using System;
using System.Net.Mail;
using System.IO;

class EmailSendingException : Exception
{
    public EmailSendingException(string message, Exception inner)
        : base(message, inner)
    {
    }
}

class EmailSender
{
    public void SendEmail(string email)
    {
        throw new SmtpException("Не удалось отправить письмо");
    }
}

class EmailManager
{
    private EmailSender sender = new EmailSender();

    public void Send(string email)
    {
        try
        {
            sender.SendEmail(email);
        }
        catch (SmtpException ex)
        {
            LogError(ex);
            throw new EmailSendingException("Ошибка при отправке email", ex);
        }
    }

    private void LogError(Exception ex)
    {
        string text = "Ошибка: " + ex.Message + "\n";
        text += "StackTrace: " + ex.StackTrace + "\n";

        if (ex.InnerException != null)
        {
            text += "InnerException: " + ex.InnerException.Message + "\n";
        }

        File.AppendAllText("log.txt", text);
    }
}

class Program
{
    static void Main(string[] args)
    {
        EmailManager manager = new EmailManager();

        try
        {
            manager.Send("test@mail.com");
        }
        catch (EmailSendingException ex)
        {
            Console.WriteLine("Не удалось отправить email");
            Console.WriteLine(ex.Message);

            if (ex.InnerException != null)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
        }
    }
}