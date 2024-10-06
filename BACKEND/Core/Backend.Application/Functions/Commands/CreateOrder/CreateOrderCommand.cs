using Backend.Application.Functions.Vm;
using MediatR;

namespace Backend.Application.Functions.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<NewOrderVm>
{
    public int Empid { get; set; }
    public int Shipperid { get; set; }
    public string Shipname { get; set; }
    public string Shipaddress { get; set; }
    public string Shipcity { get; set; }
    public string Orderdate { get; set; }
    public string Requireddate { get; set; }
    public string Shippeddate { get; set; }
    public decimal Freight { get; set; }
    public string Shipcountry { get; set; }
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Discount { get; set; }
    public int CustomerId { get; set; }
}
