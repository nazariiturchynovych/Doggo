// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.Responses;

public record PageOf<TEntity>(
    IReadOnlyCollection<TEntity> Entities);