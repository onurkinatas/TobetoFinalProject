using FluentValidation;

namespace Application.Features.StudentLectureComments.Commands.Update;

public class UpdateStudentLectureCommentCommandValidator : AbstractValidator<UpdateStudentLectureCommentCommand>
{
    public UpdateStudentLectureCommentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.LectureId).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.Comment).NotEmpty();
    }
}