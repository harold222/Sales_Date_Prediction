using FluentValidation;

namespace Backend.Application.Functions.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Empid).NotEmpty().NotNull();

        RuleFor(x => x.Shipperid).NotEmpty().NotNull();

        RuleFor(x => x.Shipname).NotEmpty().NotNull().MaximumLength(40);

        RuleFor(x => x.Shipaddress).NotEmpty().NotNull().MaximumLength(60);

        RuleFor(x => x.Shipcity).NotEmpty().NotNull().MaximumLength(16);

        RuleFor(x => x.Orderdate).NotEmpty().NotNull();

        RuleFor(x => x.Requireddate).NotEmpty().NotNull();

        RuleFor(x => x.Shippeddate).NotEmpty().NotNull();

        RuleFor(x => x.Freight).NotEmpty().NotNull();

        RuleFor(x => x.Shipcountry).NotEmpty().NotNull().MaximumLength(15);

        RuleFor(x => x.ProductId).NotEmpty().NotNull();

        RuleFor(x => x.UnitPrice).NotEmpty().NotNull();

        RuleFor(x => x.Quantity).NotEmpty().NotNull();

        RuleFor(x => x.Discount).NotEmpty().NotNull();

    }
}
