using System.Net;

namespace ToDoApp.Services.Exceptions;

public class ToDoItemHasDifferentOwnerException : ApplicationBaseException
{
    public ToDoItemHasDifferentOwnerException() : base("Item has different owner", HttpStatusCode.Forbidden)
    {
    }
}
