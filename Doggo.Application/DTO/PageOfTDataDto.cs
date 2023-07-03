// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.DTO;

public record PageOfTDataDto<TEntity>(
    IReadOnlyCollection<TEntity> Entities);