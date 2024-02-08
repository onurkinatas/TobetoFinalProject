using FluentValidation;

namespace Application.Features.StudentPrivateCertificates.Commands.Delete;

public class DeleteStudentPrivateCertificateCommandValidator : AbstractValidator<DeleteStudentPrivateCertificateCommand>
{
    public DeleteStudentPrivateCertificateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}