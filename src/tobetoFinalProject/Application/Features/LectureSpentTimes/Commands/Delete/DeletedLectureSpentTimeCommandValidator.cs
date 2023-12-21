using FluentValidation;

namespace Application.Features.LectureSpentTimes.Commands.Delete;

public class DeleteLectureSpentTimeCommandValidator : AbstractValidator<DeleteLectureSpentTimeCommand>
{
    public DeleteLectureSpentTimeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}