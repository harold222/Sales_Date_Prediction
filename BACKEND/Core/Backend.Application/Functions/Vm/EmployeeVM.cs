namespace Backend.Application.Functions.Vm;

public class EmployeeVM
{
    public int Id { get; set; }
    public string Apellido { get; set; }
    public string Nombre { get; set; }
    public string Titulo { get; set; }
    public string Cortesia { get; set; }
    public DateTime Nacimiento { get; set; }
    public DateTime Contratacion { get; set; }
    public string Direccion { get; set; }
    public string Ciudad { get; set; }
    public string Region { get; set; }
    public string CodigoPotal { get; set; }
    public string Pais { get; set; }
    public string Telefono { get; set; }
    public int Mgrid { get; set; }
    public string NombreCompleto { get; set; }
}

public class ListEmployeesVM
{
    public int Id { get; set; }
    public string NombreCompleto { get; set; }
}
