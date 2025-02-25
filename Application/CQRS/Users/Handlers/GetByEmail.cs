using Application.CQRS.Users.ResponseDtos;
using Common.GlobalResponses;
using Common.GlobalResponses.Generics;
using MediatR;
using Repository.Common;

namespace Application.CQRS.Users.Handlers;

public class GetByEmail
{
    public class Query : IRequest<Result<GetByEmailDto>>
    {
        public string Email { get; set; }
    }

    public class Handler : IRequestHandler<Query, Result<GetByEmailDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetByEmailDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);
            if (currentUser == null)
            {
                return new Result<GetByEmailDto>() { Errors = ["User tapilmadi"], IsSuccess = true };
            }

            GetByEmailDto user = new()
            {
                Id = currentUser.Id,
                Name = currentUser.Name,
                Email = currentUser.Email,
                Surname = currentUser.Surname,
                Phone = currentUser.Phone,
            };
            return new Result<GetByEmailDto>() { Data = user , Errors = [] , IsSuccess=true };
        }
    }
}