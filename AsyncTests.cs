namespace ExamplesForInterview;

public class AsyncTests
{
    public async void Run()
    {
        // Console.WriteLine(ThreadPool.SetMaxThreads(20, 1000));
        // Console.ReadLine();

        for (int i = 0; i < 400; i++)
        {
            Qwe(i);
        }

        // Qwe();
        while (true)
        {
            Console.WriteLine(ThreadPool.ThreadCount);
            Console.ReadLine();
        }

        return;
    }

    private async Task Qwe(int i)
    {
        await Task.Yield();
        for (int j = 0; j < 10000; j++)
        {
            var result = await GetString();
            // var a = result.Result;
            Console.WriteLine(result + i + " " + j);
        }
    }

    private async Task<string> GetString()
    {
        // ThreadPool.GetMaxThreads(out int a, out int b);

        return $"{ThreadPool.ThreadCount} !!!";
    }
}