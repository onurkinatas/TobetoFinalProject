using Core.Application.Responses;

namespace Application.Features.Instructors.Commands.Create;

public class CreatedInstructorResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}