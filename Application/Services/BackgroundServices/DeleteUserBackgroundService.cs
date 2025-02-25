using Common.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Common;

namespace Application.Services.BackgroundServices;

public class DeleteUserBackgroundService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
                var usersToDelete = _unitOfWork.UserRepository.GetAll().Where(u => u.CreatedDate == null && !u.IsDeleted).ToList();
                if (usersToDelete.Count != 0)
                {
                    foreach (var user in usersToDelete) 
                    {
                        user.IsDeleted = true;
                        user.DeletedDate = DateTime.Now;
                        user.DeletedBy = 1;
                    }
                    await _unitOfWork.SaveChangeAsync();
                }
            }
            catch (Exception ex)
            {

                throw new BadRequestException(ex.Message);
            }

            await Task.Delay(TimeSpan.FromMinutes(0.5) , stoppingToken);
        }
    }
}