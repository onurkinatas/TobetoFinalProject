using FluentValidation;

namespace Application.Features.StudentQuizOptions.Commands.Delete;

public class DeleteStudentQuizOptionCommandValidator : AbstractValidator<DeleteStudentQuizOptionCommand>
{
    public DeleteStudentQuizOptionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}