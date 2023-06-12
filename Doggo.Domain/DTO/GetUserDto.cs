namespace Doggo.Domain.DTO;

public record GetUserDto(
    string FirstName,
    string LastName,
    int Age,
    string Email);