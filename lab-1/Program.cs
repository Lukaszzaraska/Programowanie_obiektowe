using System;

namespace lab_1
{
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }
    public class Money : IComparable<Money>
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
        public static Money Of(decimal value, Currency currency)
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
            return Of(money.Value* factor, money.Currency);
        }
        public static Money operator *(decimal factor, Money money)
        {
            return Of(money.Value * factor, money.Currency);
        }
        public static Money operator +(Money money, Money money1)
        {
            if (money.Currency!=money1.Currency) throw new Exception() ;
            return Of(money.Value + money1.Value, money.Currency);
        }
        //Deklaracja działania < wymaga deklaracji >
        public static bool operator >(Money a, Money b)
        {
            if (a.Currency != b.Currency) throw new Exception();
            return a.Value > b.Value;
        }
        public static bool operator <(Money a, Money b)
        {
            if (a.Currency != b.Currency) throw new Exception();
            return a.Value < b.Value;
        }
        public static bool operator ==(Money a, Money b)
        {
            return a.Value == b.Value&&a.Currency==b.Currency;
        }
        public static bool operator !=(Money a, Money b)
        {
            return !(a == b);
        }
        //Rzutowanie w sposób jawny (explicit) kiedy istnieje możliwość utraty danych i nie jawny (implicit) naprzykład z int na double nie możemy starcić danych
        public static explicit operator float(Money money)
        {
            return (float)money.Value;
        }
        public static explicit operator decimal(Money money)
        {
            return money.Value;
        }
        //Nadpisanie Tostring we własnych metodach jest wskazane żeby lepiej reprezentować dane domyśłnie zwraca nazwe klasy
        public override string ToString()
        {
            return $"{_value} {_currency}";
        }
        // Żeby to zadziałało trzeba dodać do klasy interfejs : IComparable<typ>
        public int CompareTo(Money other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;
            var currencyComparison = _currency.CompareTo(other._currency);
            if (currencyComparison != 0) return currencyComparison;
            return _value.CompareTo(other._value);
        }
        public bool Equals(Money other)
        {
            if (other is not null)
            {
                if (ReferenceEquals(this, other)) return true;
                return _value == other._value && _currency.Equals(other._currency);
            }

            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Money)obj);
        }
    }
    public static class MoneyExtension
    {
        public static Money Percent(this Money money, decimal percent)
        {
            return Money.Of(money.Value * percent / 100m, money.Currency) ?? throw new
           ArgumentException();
        }
        public static Money ToCurency(this Money money,Currency NewCurrency,decimal course)
        {
            if (money.Currency == NewCurrency) return money;
            return Money.Of(money.Value * course,NewCurrency) ?? throw new
           ArgumentException();
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
            if (amount > _level)
            {
                return false;
            }
            if (amount < 0)
            {
                return false;
            }
            _level -= amount;
            return true;
        }
        public bool Refuel(Tank sourceTank, int amount)
        {
            if (amount + _level > Capacity)
            {
                return false;
            }
            if (sourceTank._level - amount < 0)
            {
                return false;
            }
            if (amount < 0)
            {
                return false;
            }
            sourceTank._level -= amount;
            _level += amount;
            return true;
        }
        public override string ToString()
        {
            return $" Capacity = {Capacity}, Level = {_level}";
        }
    }

    public class PersonPropertis
    {
        private string firstName;
        private PersonPropertis(string name)
        {
            firstName = name;
        }
        public static PersonPropertis Of(string name)
        {

            if (name != null && name.Length >= 2)
            {
                return new PersonPropertis(name);
            }
            else
            {
                throw new ArgumentException("Imie zbyt krótkie");
            }
        }
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
            }
        }
    }
    class Student : IComparable<Student>
    {
        private readonly string Nazwisko;
        private readonly string Imie;
        private readonly decimal Średnia;

        public Student(string name, string surName, decimal grade)
        {
            Imie = name;
            Nazwisko = surName;
            Średnia = grade;
        }



        public int CompareTo(Student other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (other is null) return 1;
            return Średnia.CompareTo(other.Średnia);
        }
        public override string ToString()
        {
            return $"{Imie} {Nazwisko} {Średnia}";
        }
        public static Student Of(string Name, string SurName, decimal Grade)
        {
            return Grade < 0 ? null : new Student(Name, SurName, Grade);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /* PersonPropertis personPropertis = PersonPropertis.Of("Adam");
             Console.WriteLine(personPropertis.FirstName);
             Money money = Money.ParseValue("12,1", Currency.USD);
             var price = Money.Of(100m, Currency.PLN);
             var result = price * 0.25m;
             Console.WriteLine($"result.Value: {result.Value} result.Currency:{result.Currency}"); */

            /* Tank Russian_Tank = new(100);
             Tank Ukrainian_Tank = new(100);
             Russian_Tank.Refuel(50);
             Ukrainian_Tank.Refuel(60);
             Console.WriteLine($"Ruski czołg: {Russian_Tank}");
             Console.WriteLine($"Ukrainski czołg: {Ukrainian_Tank}");
             Ukrainian_Tank.Consume(10);
             Russian_Tank.Refuel(Ukrainian_Tank, 10);
             Console.WriteLine($"Ruski czołg: {Russian_Tank}");
             Console.WriteLine($"Ukrainski czołg: {Ukrainian_Tank}");*/

            /* Student[] Class =
             {
                Student.Of("Adam","Miasko",3.5M),
                Student.Of("Filip","Chyl",4.5M),
                Student.Of("Łukasz","Zaraska",5M),
                Student.Of("Tomasz","Rożnowski",3.5M),
         };
             Class.ToList().ForEach(a => Console.WriteLine(a + " "));
             Array.Sort(Class);
             Array.Reverse(Class);
             Console.WriteLine("-------------");
             Class.ToList().ForEach(a => Console.WriteLine(a + " "));*/

            /*Money[] bank =
                {
                 Money.Of(3, Currency.PLN),
                 Money.Of(3m, Currency.USD),
                 Money.Of(1m, Currency.PLN),
                 Money.Of(4m, Currency.USD),
                };
            Array.Sort(bank);
            bank.ToList().ForEach(a => Console.Write(a + " "));
        }*/

            /*  Money money = Money.Of(100, Currency.PLN);
               Console.WriteLine(money.Percent(23.45m));*/

            /*  Money money = Money.Of(100, Currency.PLN);
              var result = money.ToCurency(Currency.PLN, 3.1m);
              var result1 = money.ToCurency(Currency.USD, 4.1m);
              Console.WriteLine(result);
              Console.WriteLine(result1);*/
            
        }
    }
}
