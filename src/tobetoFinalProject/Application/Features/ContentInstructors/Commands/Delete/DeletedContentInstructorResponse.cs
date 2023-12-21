using Core.Application.Responses;

namespace Application.Features.ContentInstructors.Commands.Delete;

public class DeletedContentInstructorResponse : IResponse
{
    public Guid Id { get; set; }
}