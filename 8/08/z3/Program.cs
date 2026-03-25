using System;
using System.Text.RegularExpressions;

class InvalidPhoneNumberException : Exception
{
    public InvalidPhoneNumberException(string message)
        : base(message)
    {
    }
}

class PhoneNumberValidator
{
    public void ValidatePhoneNumber(string phone)
    {
        if (!Regex.IsMatch(phone, @"^\+375\d{9}$"))
        {
            throw new InvalidPhoneNumberException("неправильный номер");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        PhoneNumberValidator validator = new PhoneNumberValidator();

        string[] phones = { "+375291234567", "+37529123abc" };

        foreach (var phone in phones)
        {
            try
            {
                validator.ValidatePhoneNumber(phone);
                Console.WriteLine($"Номер {phone} правильный");
            }
            catch (InvalidPhoneNumberException ex)
            {
                Console.WriteLine($"Номер {phone} неправильный: {ex.Message}");
            }
        }
    }
}