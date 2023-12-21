using Core.Application.Dtos;

namespace Application.Features.ClassSurveys.Queries.GetList;

public class GetListClassSurveyListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentClassId { get; set; }
    public Guid SurveyId { get; set; }
}