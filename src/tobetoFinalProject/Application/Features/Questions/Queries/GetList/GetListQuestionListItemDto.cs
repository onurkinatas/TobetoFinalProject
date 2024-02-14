using Core.Application.Dtos;

namespace Application.Features.Questions.Queries.GetList;

public class GetListQuestionListItemDto : IDto
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Sentence { get; set; }
    public int CorrectOptionId { get; set; }
}