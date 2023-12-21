using Core.Application.Responses;

namespace Application.Features.StudentSurveys.Queries.GetById;

public class GetByIdStudentSurveyResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid StudentId { get; set; }
}