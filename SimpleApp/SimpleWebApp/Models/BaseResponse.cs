namespace SimpleWebApp.Models;

public class BaseResponse
{
    public bool IsError { get; set; }
    public string Message { get; set; }
    public Exception? Exception { get; set; }
}