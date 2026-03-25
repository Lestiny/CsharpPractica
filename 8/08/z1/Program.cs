using System;

namespace PhoneValidationApp
{
    public class InvalidPhoneNumberException : Exception
    {
        public InvalidPhoneNumberException() : base() { }

        public InvalidPhoneNumberException(string message) : base(message) { }

        public InvalidPhoneNumberException(string message, Exception inner)
            : base(message, inner) { }
    }

    public class PhoneNumberValidator
    {
        public void ValidatePhoneNumber(string phone)
        {
            foreach (char c in phone)
            {
                if (!char.IsDigit(c))
                {
                    throw new InvalidPhoneNumberException("Номер содержит недопустимые символы");
                }
            }

            if (phone.Length != 10)
            {
                throw new InvalidPhoneNumberException("Номер должен содержать 10 цифр");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PhoneNumberValidator validator = new PhoneNumberValidator();

            Console.Write("Введите номер телефона: ");
            string input = Console.ReadLine();

            try
            {
                validator.ValidatePhoneNumber(input);
                Console.WriteLine("Номер корректный");
            }
            catch (InvalidPhoneNumberException ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Неизвестная ошибка: " + ex.Message);
            }

        }
    }
}
