namespace Doggo.Domain.Results;

public enum ErrorCode
{
    Ok,
    NotFound,
    InvalidArgument,
    PermissionDenied,
    AlreadyExists,
    EntityDoesNotExist,
    Unauthenticated,
    Unavailable,
    InternalError,
    TechnicalWorks,
}