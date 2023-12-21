using FluentValidation;

namespace Application.Features.LectureSpentTimes.Commands.Update;

public class UpdateLectureSpentTimeCommandValidator : AbstractValidator<UpdateLectureSpentTimeCommand>
{
    public UpdateLectureSpentTimeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.LectureId).NotEmpty();
        RuleFor(c => c.SpentedTime).NotEmpty();
    }
}