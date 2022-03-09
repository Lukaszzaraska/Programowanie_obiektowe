using System;
using System.Collections.Generic;

namespace Lab_1_Zadanie_Domowe
{
    public class Meter
    {
        private readonly decimal _Meter;
        public Meter(decimal Meter)
        {
            _Meter = Meter; 
        }
        /// <summary>
        /// display Meter class. Format: number + "M"
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        public override string ToString()
        {
            return $" {_Meter} M ";
        }
        /// <summary>
        /// Addition operator for two Meter objects.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        public static Meter operator+(Meter a,Meter b)
        {
            return new(a._Meter + b._Meter);
        }
        /// <summary>
        /// Multiply operator for Meter objects and int.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        public static Meter operator *(Meter a,int b)
        {
            return new(a._Meter * b);
        }
        /// <summary>
        /// Division operator for Meter and int.
        /// </summary>
        /// <param name="a">Left operand.</param>
        /// <param name="b">Right operand.</param>
        public static Meter operator /(Meter a,int b)
        {
            return new(a._Meter / b);
        }

    }
    public class Fraction
    {
        private readonly decimal fraction; // ułamek
        private readonly decimal counter; // licznik
        private readonly decimal denominator; // mianownik
        public int whole; //całość

        public Fraction(decimal fraction)
        {
            counter = fraction % 1;
            whole = (int)fraction;
            denominator = 10 * (counter.ToString().Length);
            this.fraction = fraction;
        }
        public decimal Value
        {
            get { return fraction; }
        }
        public static decimal operator +(Fraction a, Fraction b)
        {
            var new_denominator = a.denominator > b.denominator ? a.denominator : b.denominator;
            int new_whole =  a.whole+b.whole;
            var new_counter = a.counter+b.counter;
            if((new_counter / new_denominator)>1) { new_whole+=1; new_counter /= new_denominator; }
           return new_whole+new_counter;
        }
        public static decimal operator *(Fraction a, Fraction b)
        {
          return a.fraction * b.fraction;
        }
    }
   
    public class Coins
    {
      //  private int value;
        private int face_value;
        public Coins(int face_value)
        {
          this.face_value = face_value;
        }
       public int Value
        {
            set { face_value = value; }
            get { return face_value; }
        }
        public static Coins One()
        {
            return new(1);
        }
        public static Coins Two()
        {
            return new(2);
        }
        public static Coins Five()
        {
            return new(5);
        }
        public static int operator +(Coins a,Coins b)
        {
            return a.Value+b.Value;
        }
        public static Coins[] Of(int sum)
        {
            
            List<Coins> list = new();
            while(sum > 0)
            {
                if (sum - 5 >= 0) { list.Add(Five()); sum -= 5; continue; }
                if (sum - 2 >= 0) { list.Add(Two()); sum -= 2; continue; }
                if (sum - 1 >= 0) { list.Add(One()); sum -= 1; continue; }

            }
           return list.ToArray();
        }
    }
    public class Rectangle
    {
        private double Height = 4;
        private double Width = 5;
        private double PositionX,PositionY = 0;
        public Rectangle(int PositionX, int PositionY)
        {
            this.PositionX = PositionX;
            this.PositionY = PositionY;
        }
        public Rectangle(double Height, double Width)
        {
            height = Height;
            width = Width;
        }
        /*   public static Rectangle of(double height, double Width)
           {
                 Height = height;
           }*/
        public double height
        {
            get { return Height; }
            set {
                if (value > 0) { Height = Math.Round(value, 5); } else { throw new Exception(); }
            }
        }
        public double width
        {
            get { return Width; }
            set { 
                if (value > 0) { Width = Math.Round(value, 5); } else { throw new Exception(); } 
            }
        }
    
        public bool Move(int new_positionX, int new_positionY)
        {
            PositionX = new_positionX;
            PositionY = new_positionY;
            return true;
        }
        public bool Togather(Rectangle rectangle)
        {
            double[,] First = { { PositionX, PositionY }, { PositionX + Width, PositionY }, { PositionX + Width, PositionY + Height }, { PositionX, PositionY + Height } };
            double[,] Secend = { { rectangle.PositionX, rectangle.PositionY }, { rectangle.PositionX + Width, rectangle.PositionY }, { rectangle.PositionX + Width, rectangle.PositionY + Height }, { rectangle.PositionX, rectangle.PositionY + Height } };
            if (First[3, 1] < Secend[0, 1]) { return false; }
            if (First[0, 1] > Secend[3, 1]) { return false; }
            if (First[0, 0] > Secend[1, 0]) { return false; }
            if (First[1, 0] < Secend[0, 0]) { return false; }
            return true;
        }
      public static List<Rectangle> operator /(Rectangle a,double b)
        {
            List<Rectangle> output = new();
            if (b+1 == 0) { throw new Exception(); }
            a.width /= b+2;
            a.height /= b+2;
            Console.WriteLine(a.Width);
            Console.WriteLine(a.Height);
            for (int i = 0; i < Math.Pow(2,b); i++)
            {  
                output.Add(new(a.width, a.height));
            }
            return output;
        }
        public override string ToString()
        {
            return $"Width: {width}, Height: {height} PositionX: {PositionX} PositionY: {PositionY}";
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            /* Meter meter = new(10M);
             Meter meter1 = new(2.3M);
             Console.WriteLine(meter1+meter);
             Console.WriteLine(meter1 * 2);
             Console.WriteLine(meter1 / 2);*/

            /*Fraction fraction = new(0.5M);
            Fraction fraction1 = new(2.5M);
            Console.WriteLine(fraction+fraction1);
            Console.WriteLine(fraction * fraction1);*/

            /* var wynik = Coins.Of(19);
             Array.ForEach(wynik, element => Console.WriteLine(element.Value));*/
          /*  Rectangle rectangle1 = new(0, 0);
            Rectangle rectangle2 = new(2, 2);
            Console.WriteLine(rectangle1.Togather(rectangle2));
            rectangle2.Move(7, 7);
            Console.WriteLine(rectangle1.Togather(rectangle2));
            var podzielone = (rectangle1 / 1);
            Console.WriteLine(podzielone.Count);
            foreach(var x in podzielone)
            {
                Console.WriteLine(x.ToString());
            }*/
        }
    }
}
