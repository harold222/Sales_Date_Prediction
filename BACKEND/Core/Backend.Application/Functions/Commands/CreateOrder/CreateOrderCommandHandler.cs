using AutoMapper;
using Backend.Application.Contracts.Persistence;
using Backend.Application.Functions.Vm;
using Backend.Domain;
using MediatR;
using System.Data;

namespace Backend.Application.Functions.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, NewOrderVm>
{
    private readonly IAsyncRepository<CreateNewOrder> _repository;
    private readonly IMapper Mapper;

    public CreateOrderCommandHandler(IAsyncRepository<CreateNewOrder> repository, IMapper mapper)
    {
        this._repository = repository;
        this.Mapper = mapper;
    }

    public async Task<NewOrderVm> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        string keyOutput = "NewOrderId";

        IDictionary<string, object> newOrder = await this._repository.ExecuteStoredProcedureAsync("CreateNewOrder", request, new Dictionary<string, DbType>
        {
            { keyOutput, DbType.Int32 }
        });

        return new NewOrderVm() { Id = (int)newOrder[keyOutput] };
    }
}
