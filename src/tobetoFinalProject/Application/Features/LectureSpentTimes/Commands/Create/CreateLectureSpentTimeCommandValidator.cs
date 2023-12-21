using FluentValidation;

namespace Application.Features.LectureSpentTimes.Commands.Create;

public class CreateLectureSpentTimeCommandValidator : AbstractValidator<CreateLectureSpentTimeCommand>
{
    public CreateLectureSpentTimeCommandValidator()
    {
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.LectureId).NotEmpty();
        RuleFor(c => c.SpentedTime).NotEmpty();
    }
}