namespace Doggo.Domain.Entities.Base;

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
}