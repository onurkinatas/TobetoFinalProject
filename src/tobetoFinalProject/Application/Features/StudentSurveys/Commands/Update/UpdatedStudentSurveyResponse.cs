using Core.Application.Responses;

namespace Application.Features.StudentSurveys.Commands.Update;

public class UpdatedStudentSurveyResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }
    public Guid StudentId { get; set; }
}