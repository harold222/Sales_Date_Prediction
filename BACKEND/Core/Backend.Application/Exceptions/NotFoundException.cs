namespace Backend.Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string name, object key) : base($"No fue encontrado {key}")
    { }
}
