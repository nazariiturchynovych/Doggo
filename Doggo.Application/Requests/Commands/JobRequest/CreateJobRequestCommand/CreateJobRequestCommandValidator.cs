namespace Doggo.Application.Requests.Commands.JobRequest.CreateJobRequestCommand;

using FluentValidation;

public class CreateJobRequestCommandValidator : AbstractValidator<CreateJobRequestCommand>

{
    public CreateJobRequestCommandValidator()


    {
        RuleFor(x => x.DogId).NotEmpty();

        RuleFor(x => x.DogOwnerId).NotEmpty();

        RuleFor(x => x.Salary).NotEmpty().GreaterThan(5).LessThan(5000);

        RuleFor(x => x.Description).MinimumLength(5).MaximumLength(400);

        RuleFor(x => x.RequiredAge).GreaterThan(13).LessThan(73);

        RuleFor(x => x.GetRequiredScheduleDto)
            .ChildRules(
                x =>
                {
                    x.RuleFor(grs => grs.From).GreaterThan(DateTime.UtcNow);
                    x.RuleFor(grs => grs.To).GreaterThan(grs => grs.From);
                });
    }
}