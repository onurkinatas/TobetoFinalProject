using Core.Application.Responses;

namespace Application.Features.Questions.Commands.Create;

public class CreatedQuestionResponse : IResponse
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Sentence { get; set; }
    public int CorrectOptionId { get; set; }
}