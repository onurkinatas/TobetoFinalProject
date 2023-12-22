using Core.Application.Responses;

namespace Application.Features.StudentStages.Queries.GetById;

public class GetByIdStudentStageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StageId { get; set; }
    public Guid StudentId { get; set; }
    public string StageDescription { get; set; }
}