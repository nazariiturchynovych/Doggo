// ReSharper disable NotAccessedPositionalProperty.Global
namespace Doggo.Application.Responses.Authentication;

public record SignInResponse(string Token, Guid RefreshToken);