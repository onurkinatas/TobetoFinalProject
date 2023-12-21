using Core.Application.Responses;

namespace Application.Features.ContentInstructors.Commands.Create;

public class CreatedContentInstructorResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid InstructorId { get; set; }
}