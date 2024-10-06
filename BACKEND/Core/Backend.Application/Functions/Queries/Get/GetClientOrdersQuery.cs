using Backend.Application.Functions.Vm;
using MediatR;

namespace Backend.Application.Functions.Queries.Get;

public class GetClientOrdersQuery : IRequest<IReadOnlyList<OrdersByClientVM>>
{
    public int Id { get; set; }
}
