namespace Domain.Primitives;
public interface IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}
