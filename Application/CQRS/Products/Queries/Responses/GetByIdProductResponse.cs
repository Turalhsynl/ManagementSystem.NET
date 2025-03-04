namespace Application.CQRS.Products.Queries.Responses;

public class GetByIdProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
}
