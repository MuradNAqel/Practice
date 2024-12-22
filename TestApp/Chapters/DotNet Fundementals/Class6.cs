using System.Globalization;
using System.Text;

namespace TestApp.Chapters.Chapter_6
{
    public class Class6
    {
        public static void Print()
        {
            Console.Write(new string('*', 10)); // **********

            char[] ca = "Hello".ToCharArray();
            string s = new string(ca); // s = "Hello"

            //IndexOf is also overloaded to accept a startPosition(an index from which to
            //begin searching) as well as a StringComparison enum:
            Console.WriteLine("abcde abcde".IndexOf("CD", 6,
            StringComparison.CurrentCultureIgnoreCase)); // 8

            //IndexOfAny returns the first matching position of any one of a set of characters:
            Console.Write("ab,cd ef".IndexOfAny(new char[] { ' ', ',' })); // 2
            Console.Write("pas5w0rd".IndexOfAny("0123456789".ToCharArray())); // 3

            //SubString
            string mid3 = "12345".Substring(1, 3); // mid3 = "234";

            //Replace
            Console.WriteLine("to be done".Replace(" ", "")); // tobedone

            //Split and Join
            string[] words = "The quick brown fox".Split();
            string together = string.Join(" ", words); // The quick brown fox

            //Format and composition
            string composite = "It's {0} degrees in {1} on this {2} morning";
            string s1 = string.Format(composite, 35, "Perth", DateTime.Now.DayOfWeek);
            // s1 == "It's 35 degrees in Perth on this Friday morning"
            //Allignment
            string composite1 = "Name={0,-20} Credit Limit={1,15:C}";
            Console.WriteLine(string.Format(composite1, "Mary", 500));
            Console.WriteLine(string.Format(composite1, "Elizabeth", 20000));

            //StringBuilder mutable
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 50; i++) sb.Append(i).Append(",");

            //Encodings 
            Console.WriteLine("Encodings");
            foreach (EncodingInfo info in Encoding.GetEncodings())
                Console.WriteLine(info.Name);

            //Byte Array to string 
            byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes("0123456789");
            byte[] utf16Bytes = System.Text.Encoding.Unicode.GetBytes("0123456789");
            byte[] utf32Bytes = System.Text.Encoding.UTF32.GetBytes("0123456789");
            Console.WriteLine(utf8Bytes.Length); // 10
            Console.WriteLine(utf16Bytes.Length); // 20
            Console.WriteLine(utf32Bytes.Length); // 40
            string original1 = System.Text.Encoding.UTF8.GetString(utf8Bytes);
            string original2 = System.Text.Encoding.Unicode.GetString(utf16Bytes);
            string original3 = System.Text.Encoding.UTF32.GetString(utf32Bytes);
            Console.WriteLine(original1); // 0123456789
            Console.WriteLine(original2); // 0123456789
            Console.WriteLine(original3); // 0123456789

            //Time and date
            var t = TimeSpan.FromDays(10) - TimeSpan.FromSeconds(1); // 9.23:59:59

            //immutable
            Console.WriteLine("New datetime object");
            DateTime time = new DateTime();
            Console.WriteLine(time);

            //Time Zones
            Console.WriteLine("Time Zones");
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
                Console.WriteLine(z.Id);

            //Invariant Culture fixes the problem of a point being a decimal comma in other languages 
            double x = double.Parse("1.234", CultureInfo.InvariantCulture);
            string x1 = 1.234.ToString(CultureInfo.InvariantCulture);

            //String interpolation
            Console.WriteLine(10.3.ToString("C")); // $10.30
            Console.WriteLine(10.3.ToString("F4")); // 10.3000 (Fix to 4 D.P.)

            //
            string composite2 = "Credit={0:C}";
            Console.WriteLine(string.Format(composite2, 500)); // Credit=$500.00

            //Current culture 
            Console.WriteLine("Current Culture is :-");
            Console.WriteLine(CultureInfo.CurrentCulture);
            //Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("tr-TR"); //Turkish culture where // i.ToUpper != I  // and decimal point . is a comma 
            //Console.WriteLine("Current Culture became  :-");
            //Console.WriteLine(CultureInfo.CurrentCulture);

            var gg = @""" fgfdghfgfdg
                fgfgfdgrfgrfgrf
rfrgfgfgfg
fdgdfgfdg""";
        }
    }
}
