using Core.Application.Responses;

namespace Application.Features.StudentSurveys.Commands.Create;

public class CreatedStudentSurveyResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid StudentId { get; set; }
}