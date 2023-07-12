namespace Doggo.Application.Mappers;

using Domain.Entities.Walker;
using Requests.Commands.Walker.UpdateWalkerCommand;
using Responses;
using Responses.Walker;

public static class WalkerMapper
{
    public static Walker MapWalkerUpdateCommandToWalker(this UpdateWalkerCommand updateWalkerCommand, Walker walker)
    {
        walker.Skills = updateWalkerCommand.Skills ?? walker.Skills;
        walker.About = updateWalkerCommand.About ?? walker.About;
        return walker;
    }

    public static WalkerResponse MapWalkerToWalkerResponse(this Walker walker)
    {
        return new WalkerResponse(
            walker.Id,
            walker.UserId,
            walker.Skills,
            walker.About,
            walker.User.FirstName,
            walker.User.LastName,
            walker.User.PhoneNumber!,
            walker.User.Email!);
    }


    public static PageOf<WalkerResponse> MapWalkerCollectionToPageOWalkersResponse(this IReadOnlyCollection<Walker> collection)
    {
        var collectionDto = collection.Select(walker => walker.MapWalkerToWalkerResponse()).ToList();

        return new PageOf<WalkerResponse>(collectionDto);
    }
}