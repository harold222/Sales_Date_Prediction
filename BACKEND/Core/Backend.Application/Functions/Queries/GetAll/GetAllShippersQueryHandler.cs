using AutoMapper;
using Backend.Application.Contracts.Persistence;
using Backend.Application.Functions.Vm;
using Backend.Domain;
using MediatR;

namespace Backend.Application.Functions.Queries.GetAll;

public class GetAllShippersQueryHandler : IRequestHandler<GetAllShippersQuery, IReadOnlyList<ShippersVM>>
{
    private readonly IAsyncRepository<Shippers> _ShipeersRepository;
    private readonly IMapper Mapper;

    public GetAllShippersQueryHandler(IAsyncRepository<Shippers> repository, IMapper mapper)
    {
        this._ShipeersRepository = repository;
        this.Mapper = mapper;
    }

    public async Task<IReadOnlyList<ShippersVM>> Handle(GetAllShippersQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Shippers> all = await this._ShipeersRepository.GetAllAsync(new() { "Shipperid", "Companyname" });
        return Mapper.Map<IReadOnlyList<ShippersVM>>(all);
    }
}
