using FluentValidation;

namespace Application.Features.Students.Commands.Create;

public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("Öðrenci adý boþ olamaz.");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Öðrenci soyadý boþ olamaz.");

        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Þifre en az 6 karakter uzunluðunda olmalýdýr.");
    }
}