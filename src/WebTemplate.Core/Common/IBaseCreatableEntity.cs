namespace WebTemplate.Core.Common;

public interface IBaseCreatableEntity
{
    string? Created { get; set; }
    DateTime? CreatedAt { get; set; }
}
