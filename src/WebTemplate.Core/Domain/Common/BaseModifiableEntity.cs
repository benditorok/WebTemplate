using WebTemplate.Core.Common;

namespace WebTemplate.Core.Domain.Common;

public class BaseModifiableEntity : IBaseModifiableEntity
{
    public string? Modified { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
