using Domain.Entities;

namespace Repository.Repositories;

public interface IProductRepository
{
    Task InsertAsync(Product product);
    Task InsertEFAsync(Product product);
    void Update(Product product);
    void UpdateEF(Product product);
    Task Delete(int id);
    Task DeleteEF(int id);
    IQueryable<Product> GetAll();
    IQueryable<Product> GetAllEF();
    Task<Product> GetByIdAsync(int id);
    Task<Product> GetByIdEFAsync(int id);
}
