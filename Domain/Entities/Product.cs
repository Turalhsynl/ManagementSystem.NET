using Domain.BaseEntities;

namespace Domain.Entities;

public class Product : ProductBaseEntity
{
    public List<int> IngredientId { get; set; }
    public List<int> DepartmentsId { get; set; }
    public List<int> AllergenGroupId { get; set; }
}
