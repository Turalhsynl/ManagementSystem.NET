using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

internal class SqlProductRepository(string connectionString, AppDbContext context) : BaseSqlRepository(connectionString), IProductRepository
{
    private readonly AppDbContext _context = context;

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteEF(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Product> GetAllEF()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetByIdEFAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task InsertAsync(Product product)
    {
        using var conn = OpenConnection();

        using var transaction = conn.BeginTransaction();

        var sqlProduct = @"INSERT INTO Products 
                        ([Name], [Type], [Barcode], [Price], [OpenPrice], [ButtonColor], [TextColor], [InvoiceNumber], [CreatedBy])
                        VALUES (@Name, @Type, @Barcode, @Price, @OpenPrice, @ButtonColor, @TextColor, @InvoiceNumber, @CreatedBy)
                        SELECT SCOPE_IDENTITY()";

        var generatedProductId = await conn.ExecuteScalarAsync<int>(sqlProduct, product, transaction);
        product.Id = generatedProductId;

        if (product.IngredientId != null && product.IngredientId.Any())
        {
            foreach (var ingredientId in product.IngredientId)
            {
                var sqlIngredient = @"INSERT INTO ProductIngredient ([ProductId], [IngredientId], [CreatedBy])
                                  VALUES (@ProductId, @IngredientId, @CreatedBy)";
                await conn.ExecuteScalarAsync(sqlIngredient, new { ProductId = product.Id, IngredientId = ingredientId, CreatedBy = product.CreatedBy }, transaction);
            }
        }

        if (product.DepartmentsId != null && product.DepartmentsId.Any())
        {
            foreach (var departmentId in product.DepartmentsId)
            {
                var sqlDepartment = @"INSERT INTO ProductDepartment ([ProductId], [DepartmentId], [CreatedBy])
                                  VALUES (@ProductId, @DepartmentId, @CreatedBy)";
                await conn.ExecuteScalarAsync(sqlDepartment, new { ProductId = product.Id, DepartmentId = departmentId, CreatedBy = product.CreatedBy }, transaction);
            }
        }

        if (product.AllergenGroupId != null && product.AllergenGroupId.Any())
        {
            foreach (var allergenGroupId in product.AllergenGroupId)
            {
                var sqlAllergenGroup = @"INSERT INTO ProductAllergenGroup ([ProductId], [AllergenGroupId], [CreatedBy])
                                     VALUES (@ProductId, @AllergenGroupId, @CreatedBy)";
                await conn.ExecuteScalarAsync(sqlAllergenGroup, new { ProductId = product.Id, AllergenGroupId = allergenGroupId, CreatedBy = product.CreatedBy }, transaction);
            }
        }

        transaction.Commit();
    }


    public async Task InsertEFAsync(Product product)
    {
        await _context.Product.AddAsync(product);
    }

    public void Update(Product product)
    {
        throw new NotImplementedException();
    }

    public void UpdateEF(Product product)
    {
        throw new NotImplementedException();
    }
}
