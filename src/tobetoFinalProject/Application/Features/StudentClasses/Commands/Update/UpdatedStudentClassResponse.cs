using Core.Application.Responses;

namespace Application.Features.StudentClasses.Commands.Update;

public class UpdatedStudentClassResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}