namespace Doggo.Domain.Constants.ErrorConstants;

public static class JobRequestErrors
{
    public const string JobRequestDoesNotExist = "JOB_REQUEST_DOES_NOT_EXIST";

    public const string JobRequestPaymentIsLessThanRequired = "JOB_REQUEST_PAYMENT_IS_LESS_THAN_REQUIRED";

    public const string JobRequestAlreadyHAveAcceptedJob = "JOB_REQUEST_ALREADT_HAVE_ACCEPTED_JOB";

    public const string CurrentDogOwnerIsNotOwnerOfThisJobRequest = "CURRENT_DOG_OWNER_IS_NOT_OWNER_OF_THIS_JOB_REQUEST";

    public const string JobRequestCanBeChangedOnlyForTenMinutesAfterItWasCreated
        = "JOB_REQUEST_CAN_BE_CHANGED_ONLY_FOR_TEN_MINUTES_AFTER_IT_WAS_CREATED";
}