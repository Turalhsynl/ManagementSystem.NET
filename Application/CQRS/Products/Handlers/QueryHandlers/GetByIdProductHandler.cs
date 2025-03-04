using Application.CQRS.Products.Queries.Requests;
using Application.CQRS.Products.Queries.Responses;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Products.Handlers.QueryHandlers;

public class GetByIdProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetByIdProductRequest, Result<GetByIdProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public Task<Result<GetByIdProductResponse>> Handle(GetByIdProductRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
