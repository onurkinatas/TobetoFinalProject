using Core.Application.Responses;

namespace Application.Features.StudentStages.Commands.Create;

public class CreatedStudentStageResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StageId { get; set; }
    public Guid StudentId { get; set; }
}