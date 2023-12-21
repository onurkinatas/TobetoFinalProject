using Core.Application.Responses;

namespace Application.Features.StudentClasses.Commands.Delete;

public class DeletedStudentClassResponse : IResponse
{
    public Guid Id { get; set; }
}