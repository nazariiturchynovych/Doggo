namespace Doggo.Application.Requests.Queries.Dog.GetPageOfDogsQuery;

using Domain.Results;
using DTO;
using DTO.Dog;
using MediatR;

public record GetPageOfDogsQuery(
    string? NameSearchTerm,
    string? DescriptionSearchTerm,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageCount) : IRequest<CommonResult<PageOfTDataDto<GetDogDto>>>;