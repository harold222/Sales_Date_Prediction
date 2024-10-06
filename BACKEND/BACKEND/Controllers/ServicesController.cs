using Backend.Application.Functions.Commands.CreateOrder;
using Backend.Application.Functions.Queries.Get;
using Backend.Application.Functions.Queries.GetAll;
using Backend.Application.Functions.Vm;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BACKEND.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    private readonly IMediator Mediator;

    public ServicesController (IMediator mediator)
    {
        this.Mediator = mediator;
    }

    [HttpGet("NextOrder")]
    [ProducesResponseType(typeof(NextOrderVM), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<NextOrderVM>> NextOrder([FromQuery] string? filter = "")
    {
        GetNextOrderQuery query = new();
        query.Filter = filter;

        IReadOnlyList<NextOrderVM> result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetOrderByClient/{Id}")]
    [ProducesResponseType(typeof(OrdersByClientVM), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrdersByClientVM>> GetOrderByClient([FromRoute] int id)
    {
        GetClientOrdersQuery query = new();
        query.Id = id;

        IReadOnlyList<OrdersByClientVM> result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("GetEmployees")]
    [ProducesResponseType(typeof(EmployeeVM), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<EmployeeVM>> GetEmployees()
    {
        GetAllEmployeesQuery query = new();
        IReadOnlyList<ListEmployeesVM> result = await Mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("GetShippers")]
    [ProducesResponseType(typeof(ShippersVM), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<EmployeeVM>> GetShippers()
    {
        GetAllShippersQuery query = new();
        IReadOnlyList<ShippersVM> result = await Mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("GetProducts")]
    [ProducesResponseType(typeof(ProductsVM), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ProductsVM>> GetProducts()
    {
        GetAllProductsQuery query = new();
        IReadOnlyList<ProductsVM> result = await Mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("CreateOrder")]
    [ProducesResponseType(typeof(NewOrderVm), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<NewOrderVm>> CreateOrder([FromBody] CreateOrderCommand request)
    {
        NewOrderVm result = await Mediator.Send(request);

        return Ok(result);
    }
}
