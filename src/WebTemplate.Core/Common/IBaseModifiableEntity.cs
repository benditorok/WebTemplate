namespace WebTemplate.Core.Common;

public interface IBaseModifiableEntity
{
    string? Modified { get; set; }
    DateTime? ModifiedAt { get; set; }
}
