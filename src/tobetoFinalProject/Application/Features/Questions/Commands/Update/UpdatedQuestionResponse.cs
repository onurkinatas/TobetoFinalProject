using Core.Application.Responses;

namespace Application.Features.Questions.Commands.Update;

public class UpdatedQuestionResponse : IResponse
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Sentence { get; set; }
    public int CorrectOptionId { get; set; }
}