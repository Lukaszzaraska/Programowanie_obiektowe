using System;

namespace lab_1
{
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }
    public class Money 
    {
        private readonly decimal _value;
        private readonly Currency _currency;
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }
        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }
        public static Money OfWithException(decimal value, Currency currency)
        {
            return value < 0 ? throw new Exception("Error") : new Money(value, currency);
        }
         public static Money ParseValue(string valueStr, Currency currency)
        {
            decimal valueDec = decimal.Parse(valueStr);
            return new Money(valueDec, currency);
        }
        public decimal Value
        {
            get { return _value; }
        }

        public Currency Currency
        {
            get { return _currency; }
         

        }
       
}
    public class PersonPropertis
    {
        private string firstName;
        private  PersonPropertis(string name)
        {
            firstName = name;
        }
        public static PersonPropertis Of(string name)
        {
           
            if (name!=null && name.Length >= 2)
            {
                return new PersonPropertis(name);
            }
            else
            {
                throw new ArgumentException("Imie zbyt krótkie");
            }
        }
        public  string FirstName
        {
            get { return firstName; }
            set {
                firstName = value;             
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            PersonPropertis personPropertis = PersonPropertis.Of("Adam");
            Console.WriteLine(personPropertis.FirstName);
            Money money = Money.ParseValue("12,1", Currency.USD);
            Console.WriteLine($"{money.Value} { money.Currency}");
            
        }
    }
}
