namespace TestApp.Chapters.Chapter1and2
{
    public class Class1
    {


        char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
        public char GetCharAt(char[] vowels)
        {
            char lastElement = vowels[^1]; // 'u'
            char secondToLast = vowels[^2]; // 'o'

            char[] firstTwo = vowels[..2]; // 'a', 'e'
            char[] lastThree = vowels[2..]; // 'i', 'o', 'u'
            char[] middleOne = vowels[2..3]; // 'i'
            char[] lastTwo = vowels[^2..]; // 'o', 'u'
            return lastElement;
        }

        static string x = "Edit Me By Ref";
        public ref string Getx() => ref x;

        public void outKeyword()
        {
            Split("Stevie Ray Vaughn", out a1, out b1);
            Console.WriteLine(a1); // Stevie Ray
            Console.WriteLine(b1); // Vaughn
        }
        public void Split(string name, out string firstNames, out string lastName)
        {
            int i = name.LastIndexOf(' ');
            firstNames = name.Substring(0, i);
            lastName = name.Substring(i + 1);
        }

        //Print " inside string
        public void PrintTime() => Console.WriteLine($$"""{ "TimeStamp": "{{DateTime.Now}}" }""");

        //Readability with _
        int million = 1_000_000;

        //Multiple line interpolation
        string multiLineRaw = $"""
                Line 1
                Line 2
                The date and time is {DateTime.Now}
                """;
        public void Cast()
        {
            //Impicit and Explicit casting
            int x = 12345; // int is a 32-bit integer
            long y = x; // Implicit conversion to 64-bit integer
            short z = (short)x; // Explicit conversion to 16-bit integer

            //Value - Refrence type
            int i = 0, j = 1;
            i = j;
            j = 3; //i will stay 1
            Console.WriteLine("i is :" + i);

        }

        //Checked
        int a = 1000000;
        int b = 1000000;
        //int c = checked(a * b); // Checks just the expression.
        /*checked*/ // Checks all expressions
                    //{ // in statement block.

        //    c = a * b;

        //}

        //verbatim string literal is prefixed with @
        string a2 = @"\\server\fileshare\helloworld.cs";
        string escaped = "First Line\r\nSecond Line";
        string verbatim = @"First Line
        Second Line";

        //Console.WriteLine(escaped);


        //out key word
        public string a1, b1;

        //Null safety operator
        //string s1 = null;
        //string s2 = s1 ?? "nothing"; // s2 evaluates to "nothing"

        //myVariable ??= someDefault;
        //    if (myVariable == null) myVariable = someDefault;

        //    System.Text.StringBuilder sb = null;
        //string s = sb?.ToString(); // No error; s instead evaluates to null

        //string[] words = null;
        //string word = words?[1]; // word is null

        //Switch 
        //        You can also switch on multiple values(the tuple pattern) :
        //        int cardNumber = 12;
        //        string suite = "spades";
        //        string cardName = (cardNumber, suite) switch
        //        {
        //            (13, "spades") => "King of spades",
        //            (13, "clubs") => "King of clubs",
        //            ...
        //            OR
        //string cardName = cardNumber switch
        //{
        //    13 => "King",
        //    12 => "Queen",
        //    11 => "Jack",
        //    _ => "Pip card" // equivalent to 'default'
        //};

    }


    //You can define a static field in an interface
    interface ISomeI
    {
        static string Hello = "Hello From An Interface";
    };
}
