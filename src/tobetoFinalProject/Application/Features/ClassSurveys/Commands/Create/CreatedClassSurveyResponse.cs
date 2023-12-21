using Core.Application.Responses;

namespace Application.Features.ClassSurveys.Commands.Create;

public class CreatedClassSurveyResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentClassId { get; set; }
    public Guid SurveyId { get; set; }
}