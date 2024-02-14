using Core.Application.Responses;

namespace Application.Features.QuestionOptions.Commands.Update;

public class UpdatedQuestionOptionResponse : IResponse
{
    public int Id { get; set; }
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
}