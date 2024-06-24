namespace ExamplesForInterview;

// None , Repeats : 50, Same Thread 44
// ExecuteSynchronously , Repeats : 50, Same Thread 50

/// <summary>
/// ExecuteSynchronously Заставит вызвать в том же потоке в котором заканчивалось выполнение задачи
/// await Task.Delay могут выполняться в разных потоках, Thread.Sleep просто блочит поток
/// </summary>
public class TasksExecuteSynchronously
{
    public void Run()
    {
        RepeatsExperiments(TaskContinuationOptions.None);
        RepeatsExperiments(TaskContinuationOptions.ExecuteSynchronously);
    }

    private void RepeatsExperiments(TaskContinuationOptions continuationOptions, int repeats = 50)
    {
        var sameThreadCount = CountTrue(repeats, () => TwoTasksExperiment(continuationOptions));
        Console.WriteLine($"{continuationOptions} , Repeats : {repeats}, Same Thread {sameThreadCount}");
    }

    private int CountTrue(int repeats, Func<bool> func)
    {
        return Enumerable.Repeat(0, repeats).Count(_ => func());
    }

    private bool TwoTasksExperiment(TaskContinuationOptions continuationOptions)
    {
        var task = SomeTaskWithDelays();
        var continuedTask = task.ContinueWith(
            _ => Thread.CurrentThread.ManagedThreadId,
            continuationOptions);

        continuedTask.Wait();
        bool isSameThread = task.Result == continuedTask.Result;
        return isSameThread;
    }


    private async Task<int> SomeTaskWithDelays()
    {
        for (int i = 0; i < 2; i++)
        {
            await Task.Delay(1);
            await Task.Delay(1);
            await Task.Delay(1);
            await Task.Delay(1);
            await Task.Delay(1);
        }

        return Thread.CurrentThread.ManagedThreadId;
    }
}