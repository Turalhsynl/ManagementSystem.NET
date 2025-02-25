using Application.CQRS.Categories.Queries.Responses;
using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.Categories.Queries.Requests;

public record struct GetByIdCategoryRequest : IRequest<Result<GetByIdCategoryResponse>>
{
    public int Id { get; set; }
}