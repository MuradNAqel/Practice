using System.Collections.Concurrent;

namespace TestApp.Chapters.Chapter_22_parallel
{
    public class ParallelProg
    {
        static IEnumerable<int> numbers = Enumerable.Range(3, 100000 - 3);

        public static void CalcInParallel()
        {
            var parallelQuery =
            from n in numbers.AsParallel()
            where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
            select n;
            int[] primes = parallelQuery.ToArray();
        }

        public async static void SpellChecker()
        {
            if (!File.Exists("WordLookup.txt")) // Contains about 150,000 words
                File.WriteAllText("WordLookup.txt",
                await new HttpClient().GetStringAsync(
                "http://www.albahari.com/ispell/allwords.txt"));

            var wordLookup = new HashSet<string>(
            File.ReadAllLines("WordLookup.txt"),
            StringComparer.InvariantCultureIgnoreCase);

            //We then use our word lookup to create a test “document” comprising an array of a
            //million random words. After we build the array, let’s introduce a couple of spelling
            //mistakes:
            var random = new Random();
            var localRandom = new ThreadLocal<Random>
                (() => new Random(Guid.NewGuid().GetHashCode()));
            string[] wordList = wordLookup.ToArray();
            string[] wordsToTest = Enumerable.Range(0, 1000000).AsParallel()
            //.Select(i => wordList[random.Next(0, wordList.Length)])  random.Next is not thread safe so we use ThreadLocal<Random>
            .Select(i => wordList[localRandom.Value.Next(0, wordList.Length)])
            .ToArray();
            wordsToTest[12345] = "woozsh"; // Introduce a couple
            wordsToTest[23456] = "wubsie"; // of spelling mistakes.
                                           //Now we can perform our parallel spellcheck by testing wordsToTest against wor
                                           //dLookup.PLINQ makes this very easy:
            var query = wordsToTest
            .AsParallel()
            .Select((word, index) => (word, index))   //tuples are implemented as value types rather than reference types, this improves peak memory
                                                      //consumption and performance by reducing heap allocations and subsequent garbage collections
            .Where(iword => !wordLookup.Contains(iword.word))
            .OrderBy(iword => iword.index);
            foreach (var mistake in query)
                Console.WriteLine(mistake.word + " - index = " + mistake.index);

            // or use Parallel.ForEach( {{to exit loopState.Break();
            var misspellings = new ConcurrentBag<Tuple<int, string>>();
            Parallel.ForEach(wordsToTest, (word, state, i) =>
            {
                if (!wordLookup.Contains(word))
                    misspellings.Add(Tuple.Create((int)i, word));
            });
            //Disadvantage: we had to collate the results into a thread-safe collection
            //Advantage: we avoid the cost of applying an indexed Select query operator—which is less efficient than an indexed ForEach.

            //ConcurrentBag
            var misspellingsBag = new ConcurrentBag<Tuple<int, string>>();
            Parallel.ForEach(wordsToTest, (word, state, i) =>
            {
                if (!wordLookup.Contains(word))
                    misspellingsBag.Add(Tuple.Create((int)i, word));
            });
        }

        public static void CancelAfter(CancellationTokenSource cs)
        {
            IEnumerable<int> tenMillion = Enumerable.Range(3, 10_000_000);
            //var cancelSource = new CancellationTokenSource();
            //cancelSource.CancelAfter(100); // Cancel query after 100 milliseconds
            var primeNumberQuery =
            from n in tenMillion.AsParallel().WithCancellation(cs.Token)
            where Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0)
            select n;
            try
            {
                // Start query running:
                int[] primes = primeNumberQuery.ToArray();
                // We'll never get here because the other thread will cancel us.
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Query canceled");
            }
        }
        public static void PLinqPartitioning()
        {

            "abcdef".AsParallel().Select(c => char.ToUpper(c)).ForAll(Console.Write);
            //Same as range partitioning all threads recive equal ammount of elements ,,
            //downside is that a thread could recive a bunch that is easy to calculate and then sit idle waiting to other threads to finish

            //Chunk partitioning solves this problem giving each thread one or two elements at a time to keep all threads busy (Treads Balanced)
            int[] numbers = { 3, 4, 5, 6, 7, 8, 9 };
            var parallelQuery =
            Partitioner.Create(numbers, true).AsParallel() //Ensure using chunk partitioning using Partitioner.Create loadBalance = true
            .Select(num =>
            {
                Math.Sqrt(num);
                Console.WriteLine(num);
                return 0;
            });

            //Here using chunk partitioning would be more effecient than range
            ParallelEnumerable.Range(1, 10000000).Sum(i => Math.Sqrt(i));
        }
        public static void PLinqAggregate()
        {
            string text = "Let’s suppose this is a really long string";
            //sequential Aggregate
            int[] result =
                text.Aggregate(
                new int[26], // Create the "accumulator"
                (letterFrequencies, c) => // Aggregate a letter into the accumulator
                {
                    int index = char.ToUpper(c) - 'A';
                    if (index >= 0 && index < 26) letterFrequencies[index]++;
                    return letterFrequencies;
                });
            //parallel Aggregate
            int[] result2 = text.AsParallel().Aggregate(
                () => new int[26], // 1. Local accumulator creation for each thread
                (localFrequencies, c) => // 2. Local aggregation for each thread
                {
                    int index = char.ToUpper(c) - 'A';
                    if (index >= 0 && index < 26) localFrequencies[index]++;
                    return localFrequencies;
                },
                (mainFreq, localFreq) => // 3. Merge local accumulators into the main result
                    mainFreq.Zip(localFreq, (f1, f2) => f1 + f2).ToArray(),
                finalResult => finalResult // 4. Final transformation (optional)
        );
        }

        // Method to download a single file
        static async Task DownloadFileAsync(HttpClient client, string fileUrl, string destinationPath)
        {
            Console.WriteLine($"Starting download: {fileUrl}");

            using (var response = await client.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                await using (var contentStream = await response.Content.ReadAsStreamAsync())
                await using (var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await contentStream.CopyToAsync(fileStream);
                }
            }

            Console.WriteLine($"Download complete: {fileUrl}");
        }
        public async static void DownloadAtParallel()
        {
            var httpClient = new HttpClient();

            // URLs of the files to download
            var fileUrls = new[]
            {
                "https://example.com/file1.txt",
                "https://example.com/file2.txt",
                "https://example.com/file3.txt"
            };

            // Destination file paths
            var filePaths = new[]
            {
                "file1.txt",
                "file2.txt",
                "file3.txt"
            };

            // Start downloading all files in parallel
            var downloadTasks = new Task[fileUrls.Length];

            for (int i = 0; i < fileUrls.Length; i++)
            {
                int index = i; // Capture index for the closure
                downloadTasks[i] = DownloadFileAsync(httpClient, fileUrls[index], filePaths[index]);
            }

            await Task.WhenAll(downloadTasks);

            Console.WriteLine("All files have been downloaded successfully!");
        }

        public static void ParallelLoop()
        {
            object locker = new object();
            double grandTotal = 0;
            Parallel.For(1, 10000000,
                () => 0.0, // Initialize the local value.
                (i, state, localTotal) => // Body delegate. Notice that it
                localTotal + Math.Sqrt(i), // returns the new local total.
                localTotal => // Add the local value
                { lock (locker) grandTotal += localTotal; } // to the master value.
            );
        }

        public static void TaskExample()
        {
            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            cts.CancelAfter(500);
            Task task = Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                token.ThrowIfCancellationRequested(); // Check for cancellation request
            }, token);
            try { task.Wait(); }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerException is TaskCanceledException); // True
                Console.WriteLine(task.IsCanceled); // True
                Console.WriteLine(task.Status); // Canceled
            }

            //Continuation 
            Task task11 = Task.Factory.StartNew(() => Console.Write("antecedent.."));
            Task task2 = task11.ContinueWith(ant => Console.Write("..continuation"));


            Task.Factory.StartNew<int>(() => 8)
                .ContinueWith(ant => ant.Result * 2)
                .ContinueWith(ant => Math.Sqrt(ant.Result))
                .ContinueWith(ant => Console.WriteLine(ant.Result)); // 4

            //Task Factory
            var factory = new TaskFactory(

            TaskCreationOptions.LongRunning | TaskCreationOptions.AttachedToParent,
            TaskContinuationOptions.None);
            Task task1 = factory.StartNew(() => { });
            Task task22 = factory.StartNew(() => { });
        }

        public static void CatchAggregateExceptionFlatten()
        {
            //when there is a child task use this to catch the aggregate inner exception
            //            catch (AggregateException aex)
            //{
            //                foreach (Exception ex in aex.Flatten().InnerExceptions)
            //                    myLogWriter.LogException(ex);
            //}

            var parent = Task.Factory.StartNew(() =>
            {
                // We’ll throw 3 exceptions at once using 3 child tasks:
                int[] numbers = { 0 };
                var childFactory = new TaskFactory(TaskCreationOptions.AttachedToParent, TaskContinuationOptions.None);
                childFactory.StartNew(() => 5 / numbers[0]); // Division by zero
                childFactory.StartNew(() => numbers[1]); // Index out of range
                childFactory.StartNew(() => { throw null; }); // Null reference
            });
            try { parent.Wait(); }
            catch (AggregateException aex)
            {
                aex.Flatten().Handle(ex => // Note that we still need to call Flatten
                {
                    if (ex is DivideByZeroException)
                    {
                        Console.WriteLine("Divide by zero");
                        return true; // This exception is "handled"
                    }
                    if (ex is IndexOutOfRangeException)
                    {
                        Console.WriteLine("Index out of range");
                        return true; // This exception is "handled"
                    }
                    return false; // All other exceptions will get rethrown
                });
            }
        }
    }
}
