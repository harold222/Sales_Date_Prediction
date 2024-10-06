using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain;

[Table("Sales.Shippers")]
public class Shippers
{
    [Key]
    public int shipperid { get; set; }
    public string companyname { get; set; }
    public string phone { get; set; }
}
