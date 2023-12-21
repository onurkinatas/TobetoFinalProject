using FluentValidation;

namespace Application.Features.StudentCertificates.Commands.Delete;

public class DeleteStudentCertificateCommandValidator : AbstractValidator<DeleteStudentCertificateCommand>
{
    public DeleteStudentCertificateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}