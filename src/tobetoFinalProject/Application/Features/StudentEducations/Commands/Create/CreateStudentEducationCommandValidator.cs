using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace Application.Features.StudentEducations.Commands.Create;

public class CreateStudentEducationCommandValidator : AbstractValidator<CreateStudentEducationCommand>
{
    public CreateStudentEducationCommandValidator()
    {
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.EducationStatus).NotEmpty();
        RuleFor(c => c.SchoolName).NotEmpty().WithMessage("Okul alaný boþ býrakýlamaz.").MinimumLength(2).WithMessage("Okul alaný en az 2 karakter olmalýdýr.");
        RuleFor(c => c.Branch).NotEmpty().WithMessage("Bölüm alaný boþ býrakýlamaz.").Length(2, 50).WithMessage("Bölüm alaný en az 2, en fazla 50 karakter olmalýdýr.");
        ;

        RuleFor(c => c.StartDate).NotEmpty();

        RuleFor(x => x.GraduationDate)
            .GreaterThan(x => x.StartDate).WithMessage("Mezuniyet Tarihi alaný baþlangýç tarihinden sonra olmalýdýr.");

        RuleFor(c => c.GraduationDate)
            .NotEmpty()
            .When(c => c.IsContinued == false)
            .WithMessage("Eðer devam edilmiyorsa mezuniyet tarihi boþ olmamalýdýr.")
            .Must(c => c == null)
            .When(c => c.IsContinued == true)
            .WithMessage("Eðer devam ediyorsa mezuniyet tarihi girilemez.");

        RuleFor(c => c.IsContinued)
            .Must(isContinued => isContinued).When(c => c.GraduationDate == null)
            .WithMessage("Eðer mezuniyet tarihi belirtilmediyse, devam edip etmediði belirtilmelidir.");
    }
}