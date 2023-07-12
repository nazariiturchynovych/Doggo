namespace Doggo.Application.Requests.Queries.Dog.GetPageOfDogsQuery;

using Domain.Results;
using MediatR;
using Responses;
using Responses.Dog;

public record GetPageOfDogsQuery(
    string? NameSearchTerm,
    string? DescriptionSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOf<DogResponse>>>;