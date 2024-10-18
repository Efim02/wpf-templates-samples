namespace Core.Extensions;

using System.Runtime.CompilerServices;

/// <summary>
/// Расширение для класса Task.
/// </summary>
public static class TaskExtensions
{
    private const string ERROR = "Ошибка неожидаемой задачи";

    /// <summary>
    /// Не ожидает задачу, а позволяет ей выполниться асинхронно с проверкой на исключения.
    /// </summary>
    /// <param name="task"> Задача. </param>
    /// <param name="error"> Сообщение в случае ошибки. </param>
    /// <remarks> Больше нужно для читабельности кода ну и ловли ошибок. </remarks>
    public static async void Unawaited(this ConfiguredTaskAwaitable task, string error = ERROR)
    {
        try
        {
            await task;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }

    /// <summary>
    /// Добавляет задачу в список задач.
    /// </summary>
    /// <param name="tasks"> Задачи. </param>
    /// <param name="task"> Func Задача. </param>
    /// <returns> Список задач. </returns>
    public static List<Task> Combine(this List<Task> tasks, Func<Task> task)
    {
        tasks.Add(task.Invoke());
        return tasks;
    }

    /// <summary>
    /// Добавляет задачу в список задач.
    /// </summary>
    /// <param name="tasks"> Задачи. </param>
    /// <param name="task"> Задача. </param>
    /// <returns> Список задач. </returns>
    public static List<Task> Combine(this List<Task> tasks, Task task)
    {
        tasks.Add(task);
        return tasks;
    }

    /// <summary>
    /// Добавляет задачу в список задач.
    /// </summary>
    /// <param name="task"> Задача. </param>
    /// <param name="otherTask"> Func другой задачи. </param>
    /// <returns> Список задач. </returns>
    public static List<Task> Combine(this Task task, Func<Task> otherTask) => new() { task, otherTask.Invoke() };

    /// <summary>
    /// Добавляет задачу в список задач.
    /// </summary>
    /// <param name="task"> Задача. </param>
    /// <param name="action"> Действие. </param>
    /// <returns> Список задач. </returns>
    public static List<Task> Combine(this Task task, Action action) => new() { task, Task.Run(action) };

    /// <summary>
    /// Добавляет задачу в список задач.
    /// </summary>
    /// <param name="task"> Задачи. </param>
    /// <param name="otherTask"> Другая задача. </param>
    /// <returns> Список задач. </returns>
    public static List<Task> Combine(this Task task, Task otherTask) => new() { task, otherTask };

    /// <summary>
    /// Ожидает все задачи.
    /// </summary>
    /// <param name="tasks"> Задачи. </param>
    public static async Task WhenAll(this IEnumerable<Task> tasks)
    {
        await Task.WhenAll(tasks);
    }
}