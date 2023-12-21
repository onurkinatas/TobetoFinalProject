using FluentValidation;

namespace Application.Features.StudentCertificates.Commands.Create;

public class CreateStudentCertificateCommandValidator : AbstractValidator<CreateStudentCertificateCommand>
{
    public CreateStudentCertificateCommandValidator()
    {
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.CertificateId).NotEmpty();
    }
}