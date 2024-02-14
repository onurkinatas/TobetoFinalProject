using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace Application.Features.Questions.Commands.Create;

public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
{
    public CreateQuestionCommandValidator()
    {
        RuleFor(c => c.Sentence).NotEmpty();
        RuleFor(c => c.CorrectOptionId).NotEmpty();
       
    }
   
}