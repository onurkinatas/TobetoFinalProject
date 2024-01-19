using FluentValidation;

namespace Application.Features.StudentExperiences.Commands.Create;

public class CreateStudentExperienceCommandValidator : AbstractValidator<CreateStudentExperienceCommand>
{
    public CreateStudentExperienceCommandValidator()
    {
        RuleFor(c => c.StudentId).NotEmpty();
        RuleFor(c => c.CompanyName).NotEmpty().WithMessage("Kurum alaný boþ býrakýlamaz.").Length(5, 50).WithMessage("CompanyName en az 5, en fazla 50 karakter olmalýdýr.");
        RuleFor(c => c.Sector).NotEmpty();
        RuleFor(c => c.Position).NotEmpty().WithMessage("Pozisyon alaný boþ býrakýlamaz.")
            .Length(5, 50).WithMessage("Position en az 5, en fazla 50 karakter olmalýdýr.");
        ;
        RuleFor(c => c.StartDate).NotEmpty();
        RuleFor(c => c.EndDate).NotEmpty();
        RuleFor(c => c.Description).NotEmpty();
        RuleFor(c => c.CityId).NotEmpty();

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).WithMessage("Baþlangýç tarihi bitiþ tarihinden önce olmalýdýr.");

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate).WithMessage("Bitiþ tarihi baþlangýç tarihinden sonra olmalýdýr.");
    }
}