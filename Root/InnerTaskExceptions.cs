namespace ExamplesForInterview;

/// <summary>
/// Task.Run() создаёт задачу с флагом TaskCreationOptions.DenyChildAttach
/// new Task по умолчанию выбрасывает исключение AggregateException при исключениях в тасках созданных в родительской таске
/// Но возникают они либо при использовании Wait(), либо при попытке взять Result 
/// </summary>
public static class InnerTaskExceptions
{
    public static void Run()
    {
        TaskExperiment(TaskCreationType.ViaTaskRun);
        TaskExperiment(TaskCreationType.ViaNewTask);
        TaskExperiment(TaskCreationType.ViaNewTaskWithDenyChildAttach);
    }

    private static void TaskExperiment(TaskCreationType creationType)
    {
        Console.WriteLine($"Experiment {creationType}");
        var task = CreateTask(creationType, TaskAction);

        try
        {
            task.Wait();
        }
        catch
        {
            // ignored
        }
        finally
        {
            var innerExceptions = task.Exception?.InnerExceptions.ToList();
            if (innerExceptions != null)
            {
                innerExceptions.ForEach(x => Console.WriteLine($"Inner Exception: {x.InnerException?.Message}"));
            }
            else
            {
                Console.WriteLine("No Exceptions");
            }
        }

        Console.WriteLine();
    }

    private static void TaskAction()
    {
        new Task(() => throw new Exception("Exception 1"), TaskCreationOptions.AttachedToParent).Start();
        new Task(() => throw new Exception("Exception 2"), TaskCreationOptions.AttachedToParent).Start();
        new Task(() => throw new Exception("Exception 3"), TaskCreationOptions.AttachedToParent).Start();
    }

    private static Task CreateTask(TaskCreationType creationType, Action action)
    {
        return creationType switch
        {
            TaskCreationType.ViaTaskRun =>
                Task.Run(action),
            TaskCreationType.ViaNewTask =>
                StartAndReturnTask(new Task(action)),
            TaskCreationType.ViaNewTaskWithDenyChildAttach =>
                StartAndReturnTask(new Task(action, TaskCreationOptions.DenyChildAttach)),
            _ => throw new ArgumentOutOfRangeException(nameof(creationType), creationType, null)
        };
    }

    private static Task StartAndReturnTask(Task task)
    {
        task.Start();
        return task;
    }

    private enum TaskCreationType
    {
        ViaTaskRun,
        ViaNewTask,
        ViaNewTaskWithDenyChildAttach,
    }
}