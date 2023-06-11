namespace DatabaseManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

public class ExceptionHandler : Exception
{
    private string _message;
    private int _errorCode;
    public ExceptionHandler(string message, int errorCode)
    {
        _message = message;
        _errorCode = errorCode;
    }
    public static void FileException(string? message)
    {
        Console.WriteLine(message ?? "There is error happened when processing the file", 500);
    }
    public static void FetchDataException(string? message)
    {
        Console.WriteLine(message ?? "Cannot read data from the file", 500);
    }
    public static void UpdateDataException(string? message)
    {
        Console.WriteLine(message ?? "Cannot update data in the file", 500);
    }
}