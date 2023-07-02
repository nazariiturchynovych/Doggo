namespace Doggo.Application.Mappers;

using Domain.DTO;
using Domain.DTO.User;
using Domain.DTO.User.PersonalIdentifier;
using Domain.Entities.Chat;
using Domain.Entities.User;
using Requests.Commands.User;

public static class UserRequestMapper
{
    public static User MapUserUpdateCommandToUser(this UpdateUserCommand updateUserCommand, User user)
    {
        user.Age = updateUserCommand.Age ?? user.Age;
        user.FirstName = updateUserCommand.FirstName ?? user.FirstName;
        user.LastName = updateUserCommand.LastName ?? user.LastName;
        user.PhoneNumber = updateUserCommand.PhoneNumber ?? user.PhoneNumber;
        return user;
    }

    public static User MapAddUserInformationCommandToUser(this AddUserInformationCommand addUserInformationCommand, User user)
    {
        user.Age = addUserInformationCommand.Age;
        user.FirstName = addUserInformationCommand.FirstName;
        user.LastName = addUserInformationCommand.LastName;
        user.PhoneNumber = addUserInformationCommand.PhoneNumber;
        return user;
    }


    public static UserChats MapUserToUserChatDto(this ICollection<Chat> chats)
    {
        return new UserChats()
        {
            Chats = chats.ToList()
        };
    }


    public static GetUserDto MapUserToGetUserDto(this User user)
    {
        return new GetUserDto(
            Id: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Age: user.Age,
            Email: user.Email!,
            PersonalIdentifier: user.PersonalIdentifier?.PersonalIdentifierType is null
                ? null
                : new GetPersonalIdentifierDto(user.PersonalIdentifier.PersonalIdentifierType));
    }

    public static PageOfTDataDto<GetUserDto> MapUserCollectionToPageOfUsersDto(this IReadOnlyCollection<User> collection)
    {
        var collectionDto = collection.Select(user => user.MapUserToGetUserDto()).ToList();

        return new PageOfTDataDto<GetUserDto>(collectionDto);
    }
}