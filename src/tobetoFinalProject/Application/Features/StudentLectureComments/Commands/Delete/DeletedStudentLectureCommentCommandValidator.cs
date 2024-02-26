using FluentValidation;

namespace Application.Features.StudentLectureComments.Commands.Delete;

public class DeleteStudentLectureCommentCommandValidator : AbstractValidator<DeleteStudentLectureCommentCommand>
{
    public DeleteStudentLectureCommentCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}