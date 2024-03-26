namespace ExamplesForInterview;

public class TasksLockTests
{
    public void Run()
    {
        TasksWithoutLock();
        TasksWithLock();
    }

    private void TasksWithoutLock()
    {
        int x = 0;
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < 100; i++)
        {
            var task = new Task(() =>
            {
                x++;
                Task.Delay(1);
                x++;
                Task.Delay(10);
                x++;
                Task.Delay(5);
                x++;
                Task.Delay(1);
                x++;
            });
            tasks.Add(task);
        }

        tasks.ForEach(task => task.Start());
        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"{nameof(TasksWithoutLock)}, x = {x}");
    }

    private void TasksWithLock()
    {
        int x = 0;
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < 100; i++)
        {
            var task = new Task(() =>
            {
                lock (this)
                {
                    x++;
                    Task.Delay(1);
                    x++;
                    Task.Delay(10);
                    x++;
                    Task.Delay(5);
                    x++;
                    Task.Delay(1);
                    x++;
                }
            });
            tasks.Add(task);
        }

        tasks.ForEach(task => task.Start());
        Task.WaitAll(tasks.ToArray());
        Console.WriteLine($"{nameof(TasksWithLock)}, x = {x}");
    }
}