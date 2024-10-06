using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Domain;

[Table("Production.Products")]
public class Products
{
    [Key]
    public int productid { get; set; }
    public string productname { get; set; }
    public int supplierid { get; set; }
    public int categoryid { get; set; }
    public decimal unitprice { get; set; }
    public decimal discontinued { get; set; }
}
