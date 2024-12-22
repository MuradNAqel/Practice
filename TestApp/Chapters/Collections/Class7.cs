using System.Collections.ObjectModel;

namespace TestApp.Chapters.Chapter_7
{
    public class Class7
    {
        //Enumeration Searching
        static string[] names = { "Rodney", "Jack", "Jill" };
        string? match = Array.Find(names, n => n.Contains("a")); // Jackstring match = Array.Find (names, n => n.Contains ("a")); // Jack
        //Sorting
        public static int[] SortOddThenEven(int[] numbers)
        {
            Array.Sort(numbers, (x, y) => x % 2 == y % 2 ? 0 : x % 2 == 1 ? -1 : 1);
            return numbers;
        }

        // numbers array is now { 1, 3, 5, 2, 4 }
        //Convert
        static float[] reals = { 1.3f, 1.5f, 1.8f };
        int[] wholes = Array.ConvertAll(reals, r => Convert.ToInt32(r));
        // wholes array is { 1, 2, 2 }

        //Lists
        public static void ListTesting()
        {
            var words = new List<string>(); // New string-typed list
            words.Add("melon");
            words.Add("avocado");
            words.AddRange(["banana", "plum"]);
            words.Insert(0, "lemon"); // Insert at start
            words.InsertRange(0, ["peach", "nashi"]); // Insert at start
            words.Remove("melon");
            words.RemoveAt(3); // Remove the 4th element
            words.RemoveRange(0, 2); // Remove first 2 elements
            Console.WriteLine("List : " + words[words.Count - 1]); // last word
            List<string> subset = words.GetRange(1, 2); // 2nd->3rd words
        }
        //LinkedList
        public static void PrinLinkedList()
        {
            var tune = new LinkedList<string>();
            tune.AddFirst("do"); // do
            tune.AddLast("so"); // do - so
            tune.AddAfter(tune.First, "re"); // do - re- so
            tune.AddAfter(tune.First.Next, "mi"); // do - re - mi- so
            tune.AddBefore(tune.Last, "fa"); // do - re - mi - fa- so
            tune.RemoveFirst(); // re - mi - fa - so
            tune.RemoveLast(); // re - mi - fa
            LinkedListNode<string> miNode = tune.Find("mi");
            tune.Remove(miNode); // re - fa
            tune.AddFirst(miNode); // mi- re - fa
            Console.WriteLine("LinkedList ");
            foreach (string s in tune) Console.WriteLine(s);
        }

        //Queue
        public static void PrintQueue()
        {
            Console.WriteLine("Queue");
            var q = new Queue<int>();
            q.Enqueue(10);
            q.Enqueue(20);
            int[] data = q.ToArray(); // Exports to an array
            Console.WriteLine(q.Count); // "2"
            Console.WriteLine(q.Peek()); // "10"
            Console.WriteLine(q.Dequeue()); // "10"
            Console.WriteLine(q.Dequeue()); // "20"
        }

        //Stack
        public static void PrintStack()
        {
            Console.WriteLine("Stack");
            var s = new Stack<int>();
            s.Push(1); // Stack = 1
            s.Push(2); // Stack = 1,2
            s.Push(3); // Stack = 1,2,3
            Console.WriteLine(s.Count); // Prints 3
            Console.WriteLine(s.Peek()); // Prints 3, Stack = 1,2,3
            Console.WriteLine(s.Pop()); // Prints 3, Stack = 1,2
            Console.WriteLine(s.Pop()); // Prints 2, Stack = 1
            Console.WriteLine(s.Pop()); // Prints 1, Stack = <empty>
        }

        //HashSet And SortedSet

        // destructive in that they modify the set:
        /*
        public static void UnionWith(IEnumerable<T> other); // Adds
        public static void IntersectWith(IEnumerable<T> other); // Removes
        public static void ExceptWith(IEnumerable<T> other); // Removes
        public static void SymmetricExceptWith(IEnumerable<T> other); // Removes
        */
        //query the set and so are nondestructive:
        /*
        public static bool IsSubsetOf(IEnumerable<T> other);
        public static bool IsProperSubsetOf(IEnumerable<T> other);
        public static bool IsSupersetOf(IEnumerable<T> other);
        public static bool IsProperSupersetOf(IEnumerable<T> other);
        public static bool Overlaps(IEnumerable<T> other);
        public static bool SetEquals(IEnumerable<T> other);
        */

        public static void PrintHashSetTest()
        {
            var letters = new HashSet<char>("the quick brown fox");
            Console.WriteLine(letters);

            Console.WriteLine("Intersects: ");
            letters.IntersectWith("aeiou");
            foreach (char c in letters) Console.Write(c); // euio

            Console.WriteLine("ExceptWith: ");
            letters.ExceptWith("aeiou");
            foreach (char c in letters) Console.Write(c); // th qckbrwnfx

            Console.WriteLine("SymmetricExceptWith: ");
            letters.SymmetricExceptWith("the lazy brown fox");
            foreach (char c in letters) Console.Write(c); // quicklazy
        }
        //SortedSet<T> offers all the members of HashSet<T>, plus the following:
        /*
        public virtual SortedSet<T> GetViewBetween(T lowerValue, T upperValue)
        public IEnumerable<T> Reverse()
        public T Min { get; }
        public T Max { get; }
        */
        public static void PrintSortedSetGetBetween()
        {
            var letters = new SortedSet<char>("the quick brown fox");
            foreach (char c in letters.GetViewBetween('f', 'i'))
                Console.Write(c);
        }

        //Dictionary
        //Immutable collections
        public class Test
        {
            List<string> names = new List<string>();
            public ReadOnlyCollection<string> Names { get; private set; }
            public Test() => Names = new ReadOnlyCollection<string>(names);
            public void AddInternally() => names.Add("test");
        }
        public static void PrintImmutableCollection()
        {
            //immutable collection 
            Test immutableTest = new();
            Console.WriteLine(immutableTest.Names.Count); // 0
            immutableTest.AddInternally();
            Console.WriteLine(immutableTest.Names.Count); // 1

            //immutableTest.Names.Add("test"); // Compiler error
            //((IList<string>)immutableTest.Names).Add("test"); // NotSupportedException
        }

        //Custom Equality Comparer
        /*
         public abstract class EqualityComparer<T> : IEqualityComparer,IEqualityComparer<T>
            {
            public abstract bool Equals (T x, T y);
            public abstract int GetHashCode (T obj);
            bool IEqualityComparer.Equals (object x, object y);
            int IEqualityComparer.GetHashCode (object obj);
            public static EqualityComparer<T> Default { get; }
            }
         */
        public class Student
        {
            public string LastName;
            public string FirstName;
            public Student(string last, string first)
            {
                LastName = last;
                FirstName = first;
            }
        }
        public class LastFirstEqComparer : EqualityComparer<Student>
        {
            public override bool Equals(Student x, Student y)
            => x.LastName == y.LastName && x.FirstName == y.FirstName;
            public override int GetHashCode(Student obj)
            => (obj.LastName + ";" + obj.FirstName).GetHashCode();
        }

        public static void CustomStudentEqualityComparer()
        {
            var studentEqComparer = new LastFirstEqComparer();
            Student s1 = new("joe", "Hattab");
            Student s2 = new("Ahmad", "Massad");
            Student s3 = new("joe", "Hattab");

            bool isEqual = studentEqComparer.Equals(s1, s3);
            Console.WriteLine("The two student obj where equal?\n" + isEqual.ToString());
        }

        //Comparer 
        /*  Override the Compare Method with custom implementation, Often used for order and || priorities
             * public interface IComparer
            {
            int Compare(object x, object y);
            }
            public interface IComparer <in T>
            {
            int Compare(T x, T y);
            }
        */

        //StringComparer
    }
}
