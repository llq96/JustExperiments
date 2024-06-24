namespace ExamplesForInterview;

public class AsyncTests2
{
    public void Run()
    {
        int x = 0;
        var action = async () =>
        {
            lock (this)
            {
                x++;
            }

            await Task.Delay(1);
            lock (this)
            {
                x++;
            }

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(10);
            lock (this)
            {
                x++;
            }

            await Task.Delay(5);
            lock (this)
            {
                x++;
            }

            await Task.Delay(1);
            lock (this)
            {
                x++;
            }
        };
        for (int i = 0; i < 100; i++)
        {
            action();
        }

        Console.WriteLine($"{nameof(Run)}, x = {x}");
        Thread.Sleep(5000);
        Console.WriteLine($"{nameof(Run)}, x = {x}");
    }
}