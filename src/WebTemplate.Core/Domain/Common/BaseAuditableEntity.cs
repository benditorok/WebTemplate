using WebTemplate.Core.Common;

namespace WebTemplate.Core.Domain.Common;

public class BaseAuditableEntity : IBaseAuditableEntity
{
    public string? Created { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? Modified { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
