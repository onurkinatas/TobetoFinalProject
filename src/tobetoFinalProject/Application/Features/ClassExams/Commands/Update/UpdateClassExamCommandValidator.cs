using FluentValidation;

namespace Application.Features.ClassExams.Commands.Update;

public class UpdateClassExamCommandValidator : AbstractValidator<UpdateClassExamCommand>
{
    public UpdateClassExamCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.ExamId).NotEmpty();
        RuleFor(c => c.StudentClassId).NotEmpty();
    }
}