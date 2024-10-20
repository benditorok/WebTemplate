using WebTemplate.Core.Common;

namespace WebTemplate.Core.Domain.Common;

public class BaseCreatableEntity : IBaseCreatableEntity
{
    public string? Created { get; set; }
    public DateTime? CreatedAt { get; set; }
}
