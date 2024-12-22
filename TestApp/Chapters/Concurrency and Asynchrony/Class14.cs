namespace TestApp.Chapters.Chapter_14
{
    public class Class14
    {
        partial class MyWindow
        {
            string txtMessage = "";
            SynchronizationContext _uiSyncContext;
            public MyWindow()
            {
                // Capture the synchronization context for the current UI thread:
                _uiSyncContext = SynchronizationContext.Current;
                new Thread(Work).Start();
            }
            void Work()
            {
                Thread.Sleep(5000); // Simulate time-consuming task
                UpdateMessage("The answer");
            }
            void UpdateMessage(string message)
            {
                // Marshal the delegate to the UI thread:
                _uiSyncContext.Post(_ => txtMessage = message, null);
            }
        }
        //Tasks use pooled threads by default, which are background
        //threads.This means that when the main thread ends, so do
        //any tasks that you create

        //Calling Wait on a task blocks until it completes and is the equivalent of calling Join
        //on a thread:
        // Pooled Thread
        Task task = Task.Run(() =>
        {
            Thread.Sleep(2000);
            Console.WriteLine("Foo");
        });
        //Console.WriteLine(task.IsCompleted); // False
        //task.Wait(); // Blocks until task is complete

        //Independent Thread 
        //Task task = Task.Factory.StartNew(() => ...,
        //                            TaskCreationOptions.LongRunning);


        //Cancellation Pattern
        async Task Foo(CancellationToken cancellationToken)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                await Task.Delay(1000, cancellationToken);
            }
        }

        //Progress Pattern
        static Task Foo(Action<int> onProgressPercentChanged)
        {
            return Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (i % 10 == 0) onProgressPercentChanged(i / 10);
                    // Do something compute-bound...
                }
            });
        }

        public static async Task ProgressBarAsync()
        {
            Action<int> progress = i => Console.WriteLine(i + " %");
            await Foo(progress);
        }
    }
}
