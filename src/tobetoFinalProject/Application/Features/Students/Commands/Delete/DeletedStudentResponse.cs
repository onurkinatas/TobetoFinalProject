using Core.Application.Responses;

namespace Application.Features.Students.Commands.Delete;

public class DeletedStudentResponse : IResponse
{
    public Guid Id { get; set; }
}