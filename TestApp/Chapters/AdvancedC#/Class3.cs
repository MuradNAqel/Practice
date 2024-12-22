namespace TestApp.Chapters.Chapter_3
{
    public class Class3
    {
        int id = 0;
        //overloading 
        public Class3()
        {
        }
        public Class3(int id)
        {
        }
        //Deconstructor
        public void Deconstruct(out int id)
        {
            id = this.id;
        }
        //how to call deconstructor
        //(int id) = Class3;

        //Asset a = h; // Upcast always succeeds
        //Stock s = (Stock)a; // Downcast fails: a is not a Stock

        //object class 
        public Object obj;

        // virtual vs abstract
        //for providing a default implementation for the subclasses use virtual
        //for ensuring overriding and custom implementation for each sub class use abstract


        //Generic type <T>
        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        //public void Stack<T>(object obj) 
        //{
        //    this.obj = obj;
        //}

        // Covariance allows you to use a derived type (subclass) in place of a base type ((out)) keyword eg# assign a string to an object
        //contravariance is the opposite from sub to base using ((in)) keyword eg# assign an object to a string


        //delegate use to refrence this method from main use to utilize Util.Transform
        public static int Cube(int x) => (int)Math.Pow(x, 3);

        //labda delegate
        public static Func<string, string, int> totalLength = (s1, s2) => s1.Length + s2.Length;

        //Dictionaries
        static Dictionary<int, string> dict = new Dictionary<int, string>()
            {
                { 5, "five" },
                { 10, "ten" }
            };

        //Fibonacci using Yield
        public static IEnumerable<int> Fibs(int fibCount)
        {
            for (int i = 0, prevFib = 1, curFib = 1; i < fibCount; i++)
            {
                yield return prevFib;
                int newFib = prevFib + curFib;
                prevFib = curFib;
                curFib = newFib;
            }
        }
        public static IEnumerable<int> EvenNumbersOnly(IEnumerable<int> sequence)
        {
            foreach (int x in sequence)
                if ((x % 2) == 0)
                    yield return x;
        }


    }
    //Record example
    record Point(double X, double Y); //same as below
    //record Point
    //{
    //    public Point(double x, double y) => (X, Y) = (x, y);
    //    public double X { get; init; }
    //    public double Y { get; init; }
    //}
    record Point3D(double X, double Y, double Z) : Point(X, Y);  //same as below
    //class Point3D : Point
    //{
    //    public double Z { get; init; }
    //    public Point3D(double X, double Y, double Z) : base(X, Y)
    //    => this.Z = Z;
    //}

    //Extension Methods
    public static class StringHelper
    {
        public static string Pluralize(this string s) { return s + "s"; }
        public static string Capitalize(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;

            return char.ToUpper(s[0]) + s.Substring(1).ToLower();
        }
    }

    //Generic Delegate
    public class Util
    {
        public static void Transform(int[] values, Program.Transformer t)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = t(values[i]); // Apply the transformation
            }
        }
        public static void Transform<T>(T[] values, Program.Transformer<T> t)
        {
            for (int i = 0; i < values.Length; i++)
                values[i] = t(values[i]);
        }
    }

    //inheritance 
    public interface ILecture
    {
        public virtual void Vedio()
        {
            Console.WriteLine("ILecture Default implementation");
        }
    }

    public class Course : ILecture
    {
        public string[] Courses = ["C#", "Python", "JAVA"];

        public Course() { }

        public virtual void Vedio()
        {
            Console.WriteLine("Course implementation");
        }
    }

    public class Student : Course, ILecture
    {
        public string assighnedCourse;

        public Student()
        {
            assighnedCourse = base.Courses[0];
        }

        public override void Vedio()
        {
            Console.WriteLine("Student implementation");
        }
    }
    //enum
    public enum Material
    {
        Books,
        Slides,
        Resources,
        Lectures
    }


    public class Stock
    {
        string symbol;
        decimal price;
        public Stock(string symbol) => this.symbol = symbol;
        public event EventHandler<PriceChangedEventArgs> PriceChanged;
        protected virtual void OnPriceChanged(PriceChangedEventArgs e)
        {
            PriceChanged?.Invoke(this, e);
        }
        public decimal Price
        {
            get => price;
            set
            {
                if (price == value) return;
                decimal oldPrice = price;
                price = value;
                OnPriceChanged(new PriceChangedEventArgs(oldPrice, price));
            }
        }
    }

    public class PriceChangedEventArgs : EventArgs
    {
        public readonly decimal LastPrice;
        public readonly decimal NewPrice;
        public PriceChangedEventArgs(decimal lastPrice, decimal newPrice)
        {
            LastPrice = lastPrice; NewPrice = newPrice;
        }

    }

}