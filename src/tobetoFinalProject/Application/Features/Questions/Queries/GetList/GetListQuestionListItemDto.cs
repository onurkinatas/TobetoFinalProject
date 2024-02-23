using Application.Features.Options.Queries.GetList;
using Application.Features.QuestionOptions.Queries.GetList;
using Core.Application.Dtos;

namespace Application.Features.Questions.Queries.GetList;

public class GetListQuestionListItemDto : IDto
{
    public int Id { get; set; }
    public string? ImageUrl { get; set; }
    public string Sentence { get; set; }
    public ICollection<GetListQuestionOptionListItemDto>? QuestionOptions { get; set; }
}