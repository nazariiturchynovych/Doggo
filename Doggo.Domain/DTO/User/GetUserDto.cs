namespace Doggo.Domain.DTO.User;

using DogOwner;
using Walker;

public record GetUserDto(
    int Id,
    string FirstName,
    string LastName,
    int Age,
    string Email,
    DogOwnerDto? DogOwner,
    WalkerDto? Walker);