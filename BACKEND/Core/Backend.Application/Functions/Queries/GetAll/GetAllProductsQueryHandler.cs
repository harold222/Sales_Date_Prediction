using AutoMapper;
using Backend.Application.Contracts.Persistence;
using Backend.Application.Functions.Vm;
using Backend.Domain;
using MediatR;

namespace Backend.Application.Functions.Queries.GetAll;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductsVM>>
{
    private readonly IAsyncRepository<Products> _ProductRepository;
    private readonly IMapper Mapper;

    public GetAllProductsQueryHandler(IAsyncRepository<Products> repository, IMapper mapper)
    {
        this._ProductRepository = repository;
        this.Mapper = mapper;
    }

    public async Task<IReadOnlyList<ProductsVM>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Products> all = await this._ProductRepository.GetAllAsync(new() { "Productid", "Productname" });
        return Mapper.Map<IReadOnlyList<ProductsVM>>(all);
    }
}
