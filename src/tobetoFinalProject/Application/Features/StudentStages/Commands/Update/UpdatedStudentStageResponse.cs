using Core.Application.Responses;

namespace Application.Features.StudentStages.Commands.Update;

public class UpdatedStudentStageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StageId { get; set; }
    public Guid StudentId { get; set; }
}