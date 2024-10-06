namespace Backend.Application.Functions.Vm;

public class OrdersVM
{
    public int Id { get; set; }
    public int IdCliente { get; set; }
    public int IdEmpleado { get; set; }
    public DateTime FechaOrden { get; set; }
    public DateTime FechaRequerida { get; set; }
    public DateTime FechaCompra { get; set; }
    public int IdCompra { get; set; }
    public decimal Transporte { get; set; }
    public string Nombre { get; set; }
    public string Direccion { get; set; }
    public string Ciudad { get; set; }
    public string Region { get; set; }
    public string CodigoPostal { get; set; }
    public string Pais { get; set; }
}
