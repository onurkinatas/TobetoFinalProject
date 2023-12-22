using Core.Application.Responses;

namespace Application.Features.ClassSurveys.Queries.GetById;

public class GetByIdClassSurveyResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentClassId { get; set; }
    public Guid SurveyId { get; set; }
    public DateTime SurveyStartDate { get; set; }
    public DateTime SurveyEndDate { get; set; }
    public string SurveyName { get; set; }
    public string SurveyUrl { get; set; }
    public string SurveyDescription { get; set; }
}