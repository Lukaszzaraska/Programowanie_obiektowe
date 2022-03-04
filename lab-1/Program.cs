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
        //Konstruktor
        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }
        //Metody statyczne zapobiegające Podaniu ujemnej wartości 
        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }
        public static Money OfWithException(decimal value, Currency currency)
        {
            return value < 0 ? throw new Exception("Error") : new Money(value, currency);
        }
        //Parsowanie złożonego typu własnego 
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
        //Definiowanie operatorów matematycznych własnych typów
        public static Money operator *(Money money, decimal factor)
        {
            return new Money(money.Value * factor, money.Currency);
        }
        public static Money operator +(Money money, Money money1)
        {
            return new Money(money.Value + money1.Value, money.Currency);
        }
        //Deklaracja działania < wymaga deklaracji >
        public static bool operator >(Money a, Money b)
        {
            return a.Value > b.Value;
        }
        public static bool operator <(Money a, Money b)
        {
            return a.Value < b.Value;
        }
        //Rzutowanie w sposób jawny (explicit) kiedy istnieje możliwość utraty danych i nie jawny (implicit) naprzykład z int na double nie możemy starcić danych
        public static explicit operator float(Money money)
        {
            return (float)money.Value;
        }
        
    }
    public class Tank
    {
        public readonly int Capacity;
        private int _level;
        public Tank(int capacity)
        {
            Capacity = capacity;
        }
        public int Level
        {
            get
            {
                return _level;
            }
            // Uniemożliwia ustawienie wartości mniejszej niż zero
            private set
            {
                if (value < 0 || value > Capacity)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _level = value;
            }
        }
        public bool Refuel(int amount)
        {
            if (amount < 0)
            {
                return false;
            }
            if (_level + amount > Capacity)
            {
                return false;
            }
            _level += amount;
            return true;
        }
        public bool Consume(int amount)
        {
            if(amount < _level)
            {
                return false;
            }
            if(amount < 0)
            {
                return false;
            }
            _level -= amount;
            return true;
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
            var price = Money.Of(100m, Currency.PLN);
            var result = price * 0.25m;
            Console.WriteLine($"result.Value: {result.Value} result.Currency:{result.Currency}"); 
        }
    }
}
