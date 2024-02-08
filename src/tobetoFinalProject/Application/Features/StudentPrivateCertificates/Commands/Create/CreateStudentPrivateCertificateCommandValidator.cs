using FluentValidation;

namespace Application.Features.StudentPrivateCertificates.Commands.Create;

public class CreateStudentPrivateCertificateCommandValidator : AbstractValidator<CreateStudentPrivateCertificateCommand>
{
    public CreateStudentPrivateCertificateCommandValidator()
    {
        RuleFor(c=>c.CertificateUrlTemp).NotEmpty();
    }
}