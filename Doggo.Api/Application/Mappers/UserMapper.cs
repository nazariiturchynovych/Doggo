namespace Doggo.Application.Mappers;

using Domain.DTO;
using Domain.DTO.User;
using Domain.Entities.User;
using Requests.Commands.User;

public static class UserRequestMapper
{
    public static User MapUserUpdateCommandToUser(this UpdateUserCommand updateUserCommand, User user)
    {
        user.Age = updateUserCommand.Age ?? user.Age;
        user.FirstName = updateUserCommand.FirstName ?? user.FirstName;
        user.LastName = updateUserCommand.LastName ?? user.LastName;
        return user;
    }
    public static GetUserDto MapUserToGetUserDto(this User user)
    {
        return new GetUserDto(
            Id: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Age: user.Age,
            Email: user.Email!,
            DogOwner: user.DogOwner?.MapDogOwnerToDogOwnerDto(),
            Walker: user.Walker?.MapDogOwnerToDogOwnerDto()
            );
    }



    public static PageOfTDataDto<GetUserDto> MapUserCollectionToPageOfUsersDto(this IReadOnlyCollection<User> collection)
    {
        var collectionDto = new List<GetUserDto>();

        foreach (var user in collection)
        {
            collectionDto.Add(user.MapUserToGetUserDto());
        }
        return new PageOfTDataDto<GetUserDto>(collectionDto);
    }
}