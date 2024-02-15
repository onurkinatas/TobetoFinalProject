using Core.Application.Responses;

namespace Application.Features.PoolQuestions.Queries.GetById;

public class GetByIdPoolQuestionResponse : IResponse
{
    public int Id { get; set; }
    public int PoolId { get; set; }
    public int QuestionId { get; set; }
}