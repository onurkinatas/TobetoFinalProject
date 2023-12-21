using FluentValidation;

namespace Application.Features.StudentCertificates.Commands.Update;

public class UpdateStudentCertificateCommandValidator : AbstractValidator<UpdateStudentCertificateCommand>
{
    public UpdateStudentCertificateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.CertificateId).NotEmpty();
    }
}