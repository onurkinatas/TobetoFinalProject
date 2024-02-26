using FluentValidation;

namespace Application.Features.StudentLectureComments.Commands.Create;

public class CreateStudentLectureCommentCommandValidator : AbstractValidator<CreateStudentLectureCommentCommand>
{
    public CreateStudentLectureCommentCommandValidator()
    {
        RuleFor(c => c.LectureId).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.Comment).NotEmpty();
    }
}