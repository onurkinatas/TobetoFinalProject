using Core.Application.Dtos;

namespace Application.Features.StudentSurveys.Queries.GetList;

public class GetListStudentSurveyListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid StudentId { get; set; }
}