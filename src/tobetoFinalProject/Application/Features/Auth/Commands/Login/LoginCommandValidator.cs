using FluentValidation;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.UserForLoginDto.Email).NotEmpty().EmailAddress().WithMessage("Lütfen Doğru formatta e-posta adresinizi giriniz.");
        RuleFor(c => c.UserForLoginDto.Password).NotEmpty().MinimumLength(4).WithMessage("Şifreniz 4 Karakterden Büyük Olmalı");
    }
}
