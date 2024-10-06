using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationException = Backend.Application.Exceptions.ValidationException;

namespace Backend.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> Validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        this.Validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (this.Validators.Any())
        {
            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);

            ValidationResult[] validations = await Task.WhenAll(this.Validators.Select(x => x.ValidateAsync(context, cancellationToken)));

            List<ValidationFailure> failures = validations.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
                throw new ValidationException(failures);
        }

        return await next();
    }
}
