namespace Doggo.Domain.Constants;

public static class RoutesConstants
{
    public const string ConfirmEmail = "http://localhost:5246/api/Authentication/ConfirmEmail?userId={0}&token={1}";

    public const string ConfirmResetPasswordCommand = "http://localhost:5173/confirm-forgot-password?email={0}&token={1}";
}