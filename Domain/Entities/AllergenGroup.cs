using Domain.BaseEntities;

namespace Domain.Entities;

public class AllergenGroup : BaseEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
}
