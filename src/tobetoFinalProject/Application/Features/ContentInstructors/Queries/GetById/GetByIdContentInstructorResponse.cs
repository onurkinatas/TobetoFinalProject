using Core.Application.Responses;

namespace Application.Features.ContentInstructors.Queries.GetById;

public class GetByIdContentInstructorResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public Guid InstructorId { get; set; }
}