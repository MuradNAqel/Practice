using TestApp.Chapters.Chapter_14;
using TestApp.Chapters.Chapter_3;
using TestApp.Chapters.Chapter_6;
using TestApp.Chapters.Chapter_7;
using TestApp.Chapters.Chapter_8;
using TestApp.Chapters.Chapter1and2;
using TestApp.Chapters.Dependency_Injection;
using TestApp.Chapters.oop.polymorphysim;
using TestApp.Chapters.OOP.Abstraction;

namespace TestApp
{
    public class Program
    {



        public delegate int Transformer(int x);
        public delegate T Transformer<T>(T arg);
        //Tuple
        int AverageCelsiusTemperature(Season season, bool daytime) =>
        (season, daytime) switch
        {
            (Season.Spring, true) => 20,
            (Season.Spring, false) => 16,
            (Season.Summer, true) => 27,
            (Season.Summer, false) => 22,
            (Season.Fall, true) => 18,
            (Season.Fall, false) => 12,
            (Season.Winter, true) => 10,
            (Season.Winter, false) => -2,
            _ => throw new Exception("Unexpected combination")
        };
        enum Season { Spring, Summer, Fall, Winter };
        static void Main(string[] args)
        {
            DIClass.RunDIExample();
            Polymorphism.RunPolymorphism();
            Abstraction.RunAbstractionExample();
            ////////////////////////////////////////////////////////////////////////////// 1 + 2

            Class1 class1 = new Class1();

            ref string xRef = ref class1.Getx();   // Assign result to a ref local
            xRef = "New Value";

            //You can use a static field in an interface
            Console.WriteLine($"{ISomeI.Hello}{Environment.NewLine}");

            class1.outKeyword();

            ////////////////////////////////////////////////////////////////////////////// 3
            Console.WriteLine("//////////////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Chapter 3 output");
            Student student = new Student();
            student.Vedio();
            ((Course)student).Vedio();
            ((ILecture)student).Vedio();

            Console.WriteLine(Material.Books);
            Console.WriteLine("/////////////");
            Console.WriteLine("Delegate output");
            // Delegates
            int[] values = { 1, 2, 3 };
            Transformer tr = Class3.Cube;
            Util.Transform(values, tr); // Hook in Cube
            // Print the results
            foreach (int i in values)
                Console.Write(i + " ");
            //Generic Delegate 
            Transformer gt = Class3.Cube;
            Util.Transform(values, gt);
            Console.WriteLine("Generic Delegate output");
            foreach (int i in values)
                Console.Write(i + " ");

            //EventArgs
            Console.WriteLine("\n/////////////");
            Console.WriteLine("EventArgs output");

            Stock stock = new Stock("THPW");
            stock.Price = 27.10M;
            // Register with the PriceChanged event
            stock.PriceChanged += stock_PriceChanged;
            stock.Price = 31.59M;

            void stock_PriceChanged(object sender, PriceChangedEventArgs e)
            {
                if ((e.NewPrice - e.LastPrice) / e.LastPrice > 0.1M)
                    Console.WriteLine("Alert, 10% stock price increase!");
            }

            Console.WriteLine(
                    Class3.totalLength("Murad", "Aqel").ToString());

            //return using Yield 
            foreach (int fib in Class3.EvenNumbersOnly(Class3.Fibs(6)))
                Console.WriteLine(fib);

            //Extension Methods
            string x = "sausage".Pluralize().Capitalize();
            Console.WriteLine(x);

            //Tuples
            ValueTuple<string, int> bob1 = ValueTuple.Create("Bob", 23);
            (string, int) bob2 = ValueTuple.Create("Bob", 23);
            (string name, int age) bob3 = ValueTuple.Create("Bob", 23);
            var tuple = (name: "Bob", age: 23);

            //is pattern
            bool IsLetter(char c) => c is >= 'a' and <= 'z'
                                        or >= 'A' and <= 'Z';


            ////////////////////////////////////////////////////////////////////////////// 7
            Console.WriteLine("//////////////////////////////////////////////////////////////////////////////");
            int[] numbers = { 1, 2, 3, 4, 5 };

            Class7.SortOddThenEven(numbers);
            Console.WriteLine(numbers);

            Class7.ListTesting();

            Class7.PrinLinkedList();

            Class7.PrintQueue();

            Class7.PrintStack();

            Class7.PrintImmutableCollection();

            Class7.CustomStudentEqualityComparer();

            ////////////////////////////////////////////////////////////////////////////// 7
            Console.WriteLine("////////////////////////////////////////////////////////////////////////////// 7");

            Class8.SimpleLinq();

            ////////////////////////////////////////////////////////////////////////////// 6
            Console.WriteLine("////////////////////////////////////////////////////////////////////////////// 6");
            Class6.Print();

            //////////////////////////////////////////////////////////////////////////////14
            Console.WriteLine("////////////////////////////////////////////////////////////////////////////// 14");
            //Threads
            Thread t = new Thread(WriteY); // Kick off a new thread
            Console.WriteLine("Thread.CurrentThread.Name");
            Console.WriteLine(Thread.CurrentThread.Name);
            t.Start(); // running WriteY()
            t.Join();
            Console.WriteLine(t.ThreadState);
            // Simultaneously, do something on the main thread.
            for (int i = 0; i < 100; i++) Console.Write("x");
            void WriteY()
            {
                for (int i = 0; i < 100; i++) Console.Write("y");
                Console.WriteLine(Thread.CurrentThread.Name);
            }

            //Done will be printed only once
            //bool _done = false;
            //new Thread(Go).Start();
            //Go();
            //void Go()
            //{
            //    if (!_done) { _done = true; Console.WriteLine("Done"); }
            //}

            //Thread as a lambda expression 

            new Thread(() =>
            {
                Console.WriteLine("I'm running on another thread!");
                Console.WriteLine("This is so easy!");
            }).Start();

            //A continuation says to a task, “When you’ve finished, continue by doing something
            //else.” A continuation is usually implemented by a callback that executes once upon
            //completion of an operation
            Task<int> primeNumberTaskAwait = Task.Run(() =>
                    Enumerable.Range(2, 3000000).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
            var awaiter = primeNumberTaskAwait.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                int result = awaiter.GetResult();
                Console.WriteLine(result); // Writes result
            });
            //The other way
            primeNumberTaskAwait.ContinueWith(antecedent =>
            {
                int result = antecedent.Result;
                Console.WriteLine(result); // Writes 123
            });


            //IsFaulted
            //and IsCanceled
            Task<int> primeNumberTask = Task.Run(() =>
                    Enumerable.Range(2, 3000000).Count(n =>
                    Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
            Console.WriteLine("Task running...");
            Console.WriteLine("The answer is " + primeNumberTask.Result);
            Console.WriteLine(primeNumberTask.IsFaulted);
            Console.WriteLine(primeNumberTask.IsCanceled);
            Console.WriteLine(primeNumberTask.IsCompleted);

            //TaskCompletionSource slave task lets you create a task out of any operation that completes
            //in the future, This is ideal for I / O - bound work:
            var tcs = new TaskCompletionSource<int>();
            new Thread(() => { Thread.Sleep(5000); tcs.SetResult(42); })
            { IsBackground = true }
            .Start();
            Task<int> task = tcs.Task; // Our "slave" task.
            Console.WriteLine(task.Result); // 42

            //Timers
            Task<int> GetAnswerToLife()
            {
                var tcs = new TaskCompletionSource<int>();
                // Create a timer that fires once in 5000 ms:
                var timer = new System.Timers.Timer(5000) { AutoReset = false };
                timer.Elapsed += delegate { timer.Dispose(); tcs.SetResult(42); };
                timer.Start();
                return tcs.Task;
            }
            var awaiterTimed = GetAnswerToLife().GetAwaiter();
            awaiter.OnCompleted(() => Console.WriteLine(awaiterTimed.GetResult()));

            //Shortcut equivalennt to Thread.Sleep
            Task.Delay(5000).GetAwaiter().OnCompleted(() => Console.WriteLine(42));

            //Async 
            Task<int> GetPrimesCountAsync(int start, int count)
            {
                return Task.Run(() =>
                ParallelEnumerable.Range(start, count).Count(n =>
                Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
            }
            for (int i = 0; i < 10; i++)
            {
                var primesAwaiter = GetPrimesCountAsync(i * 1000000 + 2, 1000000).GetAwaiter();
                primesAwaiter.OnCompleted(() =>
                Console.WriteLine(primesAwaiter.GetResult() + " primes between... "));
            }
            Console.WriteLine("Done");

            async void DisplayPrimesCount()
            {
                int result = await GetPrimesCountAsync(2, 1000000);
                Console.WriteLine(result);
            }

            //Asyncronus + yield
            async IAsyncEnumerable<int> RangeAsync(int start, int count, int delay)
            {
                for (int i = start; i < start + count; i++)
                {
                    await Task.Delay(delay);
                    yield return i;
                }
            }
            //await foreach (var number in RangeAsync(0, 10, 500))
            //    Console.WriteLine(number);
            //wait until the whole patch is sent
            static async Task<IEnumerable<int>> RangeTaskAsync(int start, int count, int delay)
            {
                List<int> data = new List<int>();
                for (int i = start; i < start + count; i++)
                {
                    await Task.Delay(delay);
                    data.Add(i);
                }
                return data;
            }
            //foreach (var data in await RangeTaskAsync(0, 10, 500))
            //Console.WriteLine(data);

            //Progress 
            Class14.ProgressBarAsync();

            Console.WriteLine("////////////////////////////////////////////////////////////////////////////// 15");
            //Streams and IO

            string baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            string logoPath = Path.Combine(baseFolder, "logo.jpg");
            Console.WriteLine(File.Exists(logoPath));

            int sum = numbers.Aggregate(0, (total, n) => total + n);
        }
    }
}