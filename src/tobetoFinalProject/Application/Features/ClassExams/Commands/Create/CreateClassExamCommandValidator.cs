using FluentValidation;

namespace Application.Features.ClassExams.Commands.Create;

public class CreateClassExamCommandValidator : AbstractValidator<CreateClassExamCommand>
{
    public CreateClassExamCommandValidator()
    {
        RuleFor(c => c.ExamId).NotEmpty();
        RuleFor(c => c.StudentClassId).NotEmpty();
    }
}