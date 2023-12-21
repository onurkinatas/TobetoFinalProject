using Core.Application.Responses;

namespace Application.Features.StudentClasses.Commands.Create;

public class CreatedStudentClassResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}