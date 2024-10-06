namespace Backend.Domain;

public class NextOrder
{
    public int custid { get; set; }
    public string CustomerName { get; set; }
    public string LastOrderDate { get; set; }
    public string? NextPredictedOrder { get; set; }
}
