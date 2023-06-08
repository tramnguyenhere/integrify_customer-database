namespace DatabaseManagement;
public class ExceptionHandler
{
    public static void HandleException(string exceptionMessage)
    {
        Console.WriteLine($"An error occurred: {exceptionMessage}");
    }
}