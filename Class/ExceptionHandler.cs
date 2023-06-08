namespace DatabaseManagement;
public class ExceptionHandler
{
    public static void HandleException(Exception exception)
    {
        Console.WriteLine($"An error occurred: {exception.Message}");
    }
}