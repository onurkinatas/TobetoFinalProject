using FluentValidation;

namespace Application.Features.Manufacturers.Commands.Delete;

public class DeleteManufacturerCommandValidator : AbstractValidator<DeleteManufacturerCommand>
{
    public DeleteManufacturerCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}