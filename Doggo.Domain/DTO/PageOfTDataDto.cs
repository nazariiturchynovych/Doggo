namespace Doggo.Domain.DTO;

public record PageOfTDataDto<TEntity>(
    IReadOnlyCollection<TEntity> Entities);