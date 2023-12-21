using FluentValidation;

namespace Application.Features.Manufacturers.Commands.Create;

public class CreateManufacturerCommandValidator : AbstractValidator<CreateManufacturerCommand>
{
    public CreateManufacturerCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}