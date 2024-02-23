using FluentValidation;

namespace Application.Features.Students.Commands.Update;

public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
{
    public UpdateStudentCommandValidator()
    {


        RuleFor(c => c.Phone)
            .Matches(@"^\d{11}$").WithMessage("Telefon numarasý tam olarak 11 rakamdan oluþmalýdýr.");

        RuleFor(c => c.NationalIdentity)
            .Matches(@"^\d{11}$").WithMessage("TC Kimlik Numarasý 11 rakamdan oluþmalýdýr.");

        RuleFor(c => c.BirthDate)
            .InclusiveBetween(DateTime.Today.AddYears(-35), DateTime.Today.AddYears(-18))
            .WithMessage("Yaþ 18 ile 35 arasýnda olmalýdýr.");

        RuleFor(c => c.Country)
            .Matches(@"^[a-zA-ZðüþöçÝÐÜÞÖÇ]+$").WithMessage("Sadece harflerden oluþmalýdýr.")
            .MinimumLength(2).WithMessage("Ülke 2 karakterden az olamaz.")
            .MaximumLength(30).WithMessage("Ülke 30 karakterden çok olamaz.");

        RuleFor(c => c.Description).MaximumLength(300).WithMessage("Hakkýmda En fazla 300 karakter olmalý");

        RuleFor(c => c.AddressDetail).MaximumLength(200).WithMessage("Adres Detaylarý En fazla 200 karakter olmalý");
    }
}