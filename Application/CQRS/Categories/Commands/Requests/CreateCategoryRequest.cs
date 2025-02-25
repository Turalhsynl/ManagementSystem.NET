using Application.CQRS.Categories.Commands.Responses;
using Common.GlobalResponses.Generics;
using MediatR;

namespace Application.CQRS.Categories.Commands.Requests;

public class CreateCategoryRequest:IRequest<Result<CreateCategoryResponse>>
{
    public string Name { get; set; }
}