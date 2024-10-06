using AutoMapper;
using Backend.Application.Contracts.Persistence;
using Backend.Application.Functions.Vm;
using Backend.Domain;
using MediatR;

namespace Backend.Application.Functions.Queries.GetAll;

public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, IReadOnlyList<ListEmployeesVM>>
{
    private readonly IAsyncRepository<Employees> _EmployeeRepository;
    private readonly IMapper Mapper;

    public GetAllEmployeesQueryHandler(IAsyncRepository<Employees> repository, IMapper mapper)
    {
        this._EmployeeRepository = repository;
        this.Mapper = mapper;
    }

    public async Task<IReadOnlyList<ListEmployeesVM>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Employees> all = await this._EmployeeRepository.GetAllAsync(new() { "Empid", "FullName" });
        return Mapper.Map<IReadOnlyList<ListEmployeesVM>>(all);
    }
}
