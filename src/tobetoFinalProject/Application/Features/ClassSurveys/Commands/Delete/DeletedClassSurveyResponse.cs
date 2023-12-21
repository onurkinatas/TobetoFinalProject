using Core.Application.Responses;

namespace Application.Features.ClassSurveys.Commands.Delete;

public class DeletedClassSurveyResponse : IResponse
{
    public Guid Id { get; set; }
}