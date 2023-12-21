using FluentValidation;

namespace Application.Features.StudentEducations.Commands.Delete;

public class DeleteStudentEducationCommandValidator : AbstractValidator<DeleteStudentEducationCommand>
{
    public DeleteStudentEducationCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}