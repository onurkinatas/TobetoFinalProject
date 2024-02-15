using Core.Application.Responses;

namespace Application.Features.PoolQuestions.Commands.Create;

public class CreatedPoolQuestionResponse : IResponse
{
    public int Id { get; set; }
    public int PoolId { get; set; }
    public int QuestionId { get; set; }
}