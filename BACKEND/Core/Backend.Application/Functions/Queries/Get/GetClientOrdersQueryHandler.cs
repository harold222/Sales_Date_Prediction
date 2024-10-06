using AutoMapper;
using Backend.Application.Contracts.Persistence;
using Backend.Application.Functions.Vm;
using Backend.Domain;
using MediatR;

namespace Backend.Application.Functions.Queries.Get;

public class GetClientOrdersQueryHandler : IRequestHandler<GetClientOrdersQuery, IReadOnlyList<OrdersByClientVM>>
{
    private readonly IRepositorySpecific _OrderRepository;
    private readonly IMapper Mapper;

    public GetClientOrdersQueryHandler(IRepositorySpecific repository, IMapper mapper)
    {
        this._OrderRepository = repository;
        this.Mapper = mapper;
    }

    public async Task<IReadOnlyList<OrdersByClientVM>> Handle(GetClientOrdersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Orders> item = await this._OrderRepository.GetOrderByClient(request.Id);
        return Mapper.Map<IReadOnlyList<OrdersByClientVM>>(item);
    }
}
