using FluentValidation.Results;

namespace Backend.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public IDictionary<string, string[]> Errors { get; set; }

    public ValidationException() : base("Se genero error al validar el modelo")
    {
        this.Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        this.Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failure => failure.Key, failure => failure.ToArray());
    }
}
