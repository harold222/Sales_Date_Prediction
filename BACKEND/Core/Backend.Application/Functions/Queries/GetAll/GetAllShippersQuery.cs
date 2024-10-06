using Backend.Application.Functions.Vm;
using MediatR;

namespace Backend.Application.Functions.Queries.GetAll;

public class GetAllShippersQuery : IRequest<IReadOnlyList<ShippersVM>>
{
}
