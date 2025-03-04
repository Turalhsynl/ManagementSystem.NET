using Domain.Entities;

namespace Repository.Repositories;

public interface IDepartmentRepository
{
    Task AddAsync(Department department);
    void Update(Department department);
    Task<bool> Delete(int id, int deletedBy);
    IQueryable<Department> GetAll();
    Task<Department> GetByIdAsync(int id);
}