using Core.Application.Responses;

namespace Application.Features.StudentStages.Commands.Delete;

public class DeletedStudentStageResponse : IResponse
{
    public Guid Id { get; set; }
}