namespace RevvyTasks.Abstractions;

public interface ITaskTester
{
    Task SetupFirstTask(HttpClient client);
    Task SetupSecondTask(HttpClient client);
}