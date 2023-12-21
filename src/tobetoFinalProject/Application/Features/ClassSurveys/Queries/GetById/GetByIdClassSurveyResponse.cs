using Core.Application.Responses;

namespace Application.Features.ClassSurveys.Queries.GetById;

public class GetByIdClassSurveyResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentClassId { get; set; }
    public Guid SurveyId { get; set; }
}