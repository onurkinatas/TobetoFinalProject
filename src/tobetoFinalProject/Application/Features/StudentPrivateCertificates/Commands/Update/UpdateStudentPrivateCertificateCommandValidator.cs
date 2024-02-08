using FluentValidation;

namespace Application.Features.StudentPrivateCertificates.Commands.Update;

public class UpdateStudentPrivateCertificateCommandValidator : AbstractValidator<UpdateStudentPrivateCertificateCommand>
{
    public UpdateStudentPrivateCertificateCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.CertificateUrl).NotEmpty();
    }
}