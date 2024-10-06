namespace Backend.Application.Exceptions;

public class CustomMessageException : ApplicationException
{
    public int StatusCode { get; set; }

    public CustomMessageException(int status = 400, string message = "Se ha generado un error") : base(message)
    {
        this.StatusCode = status;
    }
}
