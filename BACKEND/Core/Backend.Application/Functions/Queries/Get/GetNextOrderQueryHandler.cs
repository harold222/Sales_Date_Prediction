using AutoMapper;
using Backend.Application.Contracts.Persistence;
using Backend.Application.Functions.Vm;
using Backend.Domain;
using MediatR;

namespace Backend.Application.Functions.Queries.Get;

public class GetNextOrderQueryHandler : IRequestHandler<GetNextOrderQuery, IReadOnlyList<NextOrderVM>>
{
    private readonly IAsyncRepository<NextOrder> _OrderRepository;
    private readonly IMapper Mapper;

    public GetNextOrderQueryHandler(IAsyncRepository<NextOrder> repository, IMapper mapper)
    {
        this._OrderRepository = repository;
        this.Mapper = mapper;
    }

    public async Task<IReadOnlyList<NextOrderVM>> Handle(GetNextOrderQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<NextOrder> all = await this._OrderRepository.QueryStoredProcedureAsync("GetCustomerOrderPrediction", new { Filter = request.Filter ?? "" });
        return Mapper.Map<IReadOnlyList<NextOrderVM>>(all);
    }
}
