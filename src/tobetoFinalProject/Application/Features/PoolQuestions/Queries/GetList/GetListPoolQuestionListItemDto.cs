using Core.Application.Dtos;

namespace Application.Features.PoolQuestions.Queries.GetList;

public class GetListPoolQuestionListItemDto : IDto
{
    public int Id { get; set; }
    public int PoolId { get; set; }
    public int QuestionId { get; set; }
}