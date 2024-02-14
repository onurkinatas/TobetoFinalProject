using Core.Application.Dtos;

namespace Application.Features.QuestionOptions.Queries.GetList;

public class GetListQuestionOptionListItemDto : IDto
{
    public int Id { get; set; }
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
}