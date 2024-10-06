namespace Backend.Application.Functions.Vm;

public class OrdersByClientVM
{
    public int Id { get; set; }
    public DateTime? FechaRequerida { get; set; }
    public DateTime? FechaCompra { get; set; }
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
}
