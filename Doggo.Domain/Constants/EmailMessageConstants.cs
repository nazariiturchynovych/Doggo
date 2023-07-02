namespace Doggo.Domain.Constants;

public static class EmailMessageConstants
{
    public static class MyClass
    {
        public const string ValidateEmail = "Validate your email: <a href = '{0}'> link </a>";

        public const string ResetPassword = "Reset password: <a href = '{0}'> link </a>";
    }

    public static class Subject
    {
        public const string ValidateEmail = "ValidateEmail";

        public const string ResetPassword = "ResetPassword";
    }
}