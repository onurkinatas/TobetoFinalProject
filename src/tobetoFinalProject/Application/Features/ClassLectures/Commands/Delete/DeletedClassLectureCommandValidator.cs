using FluentValidation;

namespace Application.Features.ClassLectures.Commands.Delete;

public class DeleteClassLectureCommandValidator : AbstractValidator<DeleteClassLectureCommand>
{
    public DeleteClassLectureCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}