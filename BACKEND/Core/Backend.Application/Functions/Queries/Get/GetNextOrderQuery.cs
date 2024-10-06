using Backend.Application.Functions.Vm;
using MediatR;

namespace Backend.Application.Functions.Queries.Get;

public class GetNextOrderQuery : IRequest<IReadOnlyList<NextOrderVM>>
{
    public string Filter { get; set; }
}
