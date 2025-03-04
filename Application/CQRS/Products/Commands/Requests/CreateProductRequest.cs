using Application.CQRS.Products.Commands.Responses;
using Common.GlobalResponses.Generics;
using MediatR;
using System.Globalization;

namespace Application.CQRS.Products.Commands.Requests;

public class CreateProductRequest : IRequest<Result<CreateProductResponse>>
{
    public string Name { get; set; }
}
