using Application.CQRS.Categories.Queries.Requests;
using Application.CQRS.Categories.Queries.Responses;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Categories.Handlers.QueryHandlers;

public sealed class GetByIdCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetByIdCategoryRequest, Result<GetByIdCategoryResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GetByIdCategoryResponse>> Handle(GetByIdCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);
        if (category == null)
            throw new NotFoundException(typeof(Category), request.Id);

        GetByIdCategoryResponse response = new()
        {
            Id = category.Id,
            Name = category.Name,
            CreatedDate = category.CreatedDate ?? DateTime.UtcNow,
        };

        return new Result<GetByIdCategoryResponse> { Data = response, Errors = [], IsSuccess=true };
    }
}