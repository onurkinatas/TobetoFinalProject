using FluentValidation;

namespace Application.Features.LectureLikes.Commands.Create;

public class CreateLectureLikeCommandValidator : AbstractValidator<CreateLectureLikeCommand>
{
    public CreateLectureLikeCommandValidator()
    {
        RuleFor(c => c.LectureId).NotEmpty();
    }
}