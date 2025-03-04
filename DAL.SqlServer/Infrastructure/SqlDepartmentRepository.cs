using DAL.SqlServer.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlDepartmentRepository(AppDbContext context) : IDepartmentRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Department department)
    {
        await _context.Department.AddAsync(department);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Delete(int id, int deletedBy)
    {
        var user = await _context.Department.FirstOrDefaultAsync(d => d.Id == id);
        user.IsDeleted = true;
        user.DeletedDate = DateTime.Now;
        user.DeletedBy = 0;

        await _context.SaveChangesAsync();

        return true;
    }

    public IQueryable<Department> GetAll()
    {
        return _context.Department;
    }

    public async Task<Department> GetByIdAsync(int id)
    {
        return (await _context.Department.FirstOrDefaultAsync(u => u.Id == id))!;
    }

    public void Update(Department department)
    {
        department.UpdatedDate = DateTime.Now;
        _context.Update(department);
        _context.SaveChanges();
    }
}
