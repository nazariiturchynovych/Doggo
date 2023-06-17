namespace Doggo.Domain.Constants;

public static class FacebookConstants
{
    public const string BaseUrl = "https://graph.facebook.com/";

    public const string TokenValidatorUrl = "debug_token?input_token={0}&access_token={1}|{2}";

    public const string UserInfoUrl = "me?fields=first_name,last_name,picture,email&access_token={0}";
}