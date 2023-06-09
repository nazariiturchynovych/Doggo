namespace Doggo.Application.Requests.Commands.JobRequest.UpdateJobRequestCommand;

using FluentValidation;
using Responses.JobRequest;

public class UpdateJobRequestCommandValidator : AbstractValidator<UpdateJobRequestCommand>
{
    public UpdateJobRequestCommandValidator()
    {
        When(
            x => x.PaymentTo is not null,
            () => RuleFor(x => x.PaymentTo)
                .NotEmpty()
                .GreaterThan(5)
                .LessThan(5000));

        When(
            x => x.Description is not null,
            () => RuleFor(x => x.Description).MinimumLength(5).MaximumLength(400));

        When(
            x => x.RequiredAge is not null,
            () => RuleFor(x => x.RequiredAge).GreaterThan(13).LessThan(73));

        When(x => x.RequiredScheduleDto is not null,
            () => RuleFor(x => x.RequiredScheduleDto)
                .SetValidator(new UpdateRequiredScheduleDtoValidator()!));
    }
}

public class UpdateRequiredScheduleDtoValidator : AbstractValidator<UpdateRequiredScheduleResponse>
{
    public UpdateRequiredScheduleDtoValidator()
    {
        When(x => x.From is not null,
            () => RuleFor(x => x.From).GreaterThan(DateTime.UtcNow));

        When(x => x.To is not null,
            () => RuleFor(x => x.To).GreaterThan(x => x.From));
    }
}