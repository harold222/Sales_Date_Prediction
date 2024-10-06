using Backend.Application.Functions.Vm;
using MediatR;

namespace Backend.Application.Functions.Queries.GetAll;

public class GetAllProductsQuery : IRequest<IReadOnlyList<ProductsVM>>
{
}
