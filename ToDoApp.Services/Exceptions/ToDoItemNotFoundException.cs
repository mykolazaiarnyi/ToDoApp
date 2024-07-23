using System.Net;

namespace ToDoApp.Services.Exceptions;

public class ToDoItemNotFoundException : ApplicationBaseException
{
    public ToDoItemNotFoundException() : base("Item not found", HttpStatusCode.NotFound)
    {
    }
}
