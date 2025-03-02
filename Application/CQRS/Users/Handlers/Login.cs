using Application.Services;
using Common.Exceptions;
using Common.GlobalResponses.Generics;
using Common.Security;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Repository.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.CQRS.Users.Handlers;

public class Login
{
    public class LoginRequest : IRequest<Result<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public sealed class Handler(IUnitOfWork unitOfWork, IConfiguration configuration) : IRequestHandler<LoginRequest, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IConfiguration _configuration = configuration;

        public async Task<Result<string>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            User currentUser = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email) ?? throw new BadRequestException("User doesn't exist");

            var hashedpassword = PasswordHasher.ComputeStringToSha256Hash(request.Password);
            if (hashedpassword != currentUser.PasswordHash)
            {
                throw new BadRequestException("Wrong Password");
            }
            if (currentUser.Email == request.Email && currentUser.PasswordHash == hashedpassword)
            {
                List<Claim> authClaims = [
                    new Claim(ClaimTypes.NameIdentifier, currentUser.ToString()),
                    new Claim(ClaimTypes.Name, currentUser.Name),
                    new Claim(ClaimTypes.Email, currentUser.Email),
                    ];

                JwtSecurityToken token = TokenService.CreateToken(authClaims, _configuration);
                string tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return new Result<string> { Data = tokenString };
            }
            return new Result<string> { Data = null , Errors = ["Something went wrong!"] , IsSuccess = false };
        }
    }
}