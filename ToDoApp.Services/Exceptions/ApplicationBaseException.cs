using System.Net;

namespace ToDoApp.Services.Exceptions;

public class ApplicationBaseException : Exception
{
    public HttpStatusCode Status { get; }

    public ApplicationBaseException(string message, HttpStatusCode status) : base(message)
    {
        Status = status;
    }
}
