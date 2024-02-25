namespace Doggo.Application.Mappers;

using Domain.Entities.User;
using Requests.Commands.User.AddUserInformationCommand;
using Requests.Commands.User.UpdateUserCommand;
using Responses;
using Responses.User;
using Responses.User.PersonalIdentifier;

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
        user.IsApproved = true;
        return user;
    }


    public static UserResponse MapUserToUserResponse(this User user)
    {
        return new UserResponse(
            Id: user.Id,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Age: user.Age,
            Email: user.Email!,
            DogOwnerId: user.DogOwner is null ? null : user.DogOwner.Id == Guid.Empty ? null : user.DogOwner.Id ,
            WalkerId: user.Walker is null ? null : user.Walker.Id == Guid.Empty ? null : user.Walker.Id ,
            PersonalIdentifier: user.PersonalIdentifier?.PersonalIdentifierType is null
                ? null
                : new PersonalIdentifierResponse(user.PersonalIdentifier.PersonalIdentifierType));
    }

    public static PageOf<UserResponse> MapUserCollectionToPageOfUsersResponse(this IReadOnlyCollection<User> collection)
    {
        var collectionDto = collection.Select(user => user.MapUserToUserResponse()).ToList();

        return new PageOf<UserResponse>(collectionDto);
    }
}