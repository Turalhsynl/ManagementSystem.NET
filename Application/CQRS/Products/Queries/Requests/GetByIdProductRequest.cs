using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.Products.Queries.Requests;

public class GetByIdProductRequest : IRequest<Result<GetByIdProductRequest>>
{
    public int Id { get; set; }
}
