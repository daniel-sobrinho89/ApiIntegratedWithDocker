namespace ApiIntegratedWithDocker.Domain.Requests;

public class OrderRequest
{
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
}
