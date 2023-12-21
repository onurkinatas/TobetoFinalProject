using Core.Application.Responses;

namespace Application.Features.ContentInstructors.Commands.Update;

public class UpdatedContentInstructorResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid InstructorId { get; set; }
}