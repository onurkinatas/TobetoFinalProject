using Core.Application.Responses;

namespace Application.Features.QuestionOptions.Commands.Create;

public class CreatedQuestionOptionResponse : IResponse
{
    public int Id { get; set; }
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
}