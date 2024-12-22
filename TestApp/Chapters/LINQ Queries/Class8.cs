namespace TestApp.Chapters.Chapter_8
{
    public class Class8
    {
        //LINQ
        public static string[] names = { "Tom", "Dick", "Harry", "Mary", "Jay" };
        public static int[] numbers = { 10, 9, 8, 7, 6 };
        public static int[] seq1 = { 1, 2, 3 };
        public static int[] seq2 = { 3, 4, 5 };
        public static void SimpleLinq()
        {
            IEnumerable<string> filtered = names.Where(n => n.Contains("a"));
            IEnumerable<string> sorted = filtered.OrderBy(n => n.Length);
            IEnumerable<string> finalQuery = sorted.Select(n => n.ToUpper());

            IEnumerable<int> firstThree = numbers.Take(3); // { 10, 9, 8 }
            IEnumerable<int> lastTwo = numbers.Skip(3); // { 7, 6 }
            IEnumerable<int> reversed = numbers.Reverse(); // { 6, 7, 8, 9, 10 }

            int firstNumber = numbers.First(); // 10
            int lastNumber = numbers.Last(); // 6
            int secondNumber = numbers.ElementAt(1); // 9
            int secondLowest = numbers.OrderBy(n => n).Skip(1).First(); // 7
            int count = numbers.Count(); // 5;
            int min = numbers.Min(); // 6;

            bool hasTheNumberNine = numbers.Contains(9); // true
            bool hasMoreThanZeroElements = numbers.Any(); // true
            bool hasAnOddElement = numbers.Any(n => n % 2 != 0); // true

            IEnumerable<int> concat = seq1.Concat(seq2); // { 1, 2, 3, 3, 4, 5 }
            IEnumerable<int> union = seq1.Union(seq2); // { 1, 2, 3, 4, 5 }

            int matches = (from n in names where n.Contains("a") select n).Count();
            string first = (from n in names orderby n select n).First(); // Dick
        }

        //This is called deferred or lazy execution and is the
        //same as what happens with delegates:
        // Capture the rusult using .ToList or .ToArray
        public static void LazyExecution()
        {
            var numbers = new List<int> { 1 };
            IEnumerable<int> query = numbers.Select(n => n * 10); // Build query
            numbers.Add(2); // Sneak in an extra element
            foreach (int n in query) //this will call the query and execute it on the current numbers {1,2} like a delegate
                Console.Write(n + "|"); // 10|20|
        }
    }
}
