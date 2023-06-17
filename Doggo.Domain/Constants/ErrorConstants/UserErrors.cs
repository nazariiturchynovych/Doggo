namespace Doggo.Domain.Constants.ErrorConstants;

public static class UserErrors
{
    public const string UserCreateFailed = "USER_CREATE_FAILED";

    public const string UserEmailConfirmFailed = "USER_EMAIL_CONFIRM_FAILED";

    public const string EmailAlreadyConfirmed = "EMAIL_ALREADY_CONFIRMED";

    public const string PasswordDoesNotMatch = "PASSWORD_DOES_NOT_MATCH";

    public const string ResetPasswordFailed = "RESET_PASSWORD_FAILED";

    public const string PasswordChangeFailed = "PASSWORD_CHANGE_FAILED";

    public const string UserGoogleAuthorizationFailed = "USER_GOOGLE_AUTHORIZATION_FAILED";

    public const string UserFacebookAuthorizationFailed = "USER_FACEBOOK_AUTHORIZATION_FAILED";

    public const string AddToRoleFailed = "USER_ADD_TO_ROLE_FAILED";
}