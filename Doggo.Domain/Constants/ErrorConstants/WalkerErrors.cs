namespace Doggo.Domain.Constants.ErrorConstants;

public static class WalkerErrors
{
    public const string UserIsAlreadyDogOwner = "USER_IS_ALREADY_DOGOWNER";

    public const string WalkerDoesNotExist = "Walker_DOES_NOT_EXIST";

    public const string WalkerCanNotApplyJobForHisOwnJobRequest = "WALKER_CAN_NOT_APPLY_JOB_FOR_HIS_OWN_JOB_REQUEST";

    public const string WalkerIsNotOwnerOfThisJob = "WALKER_IS_NOT_OWNER_OF_THIS_JOB";
}