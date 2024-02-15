using Application.Features.Options.Queries.GetList;
using Core.Application.Dtos;
using Domain.Entities;

namespace Application.Features.QuestionOptions.Queries.GetList;

public class GetListQuestionOptionListItemDto : IDto
{
    public int Id { get; set; }
    public int OptionId { get; set; }
    public int QuestionId { get; set; }
    public GetListOptionListItemDto Option { get; set; }
}