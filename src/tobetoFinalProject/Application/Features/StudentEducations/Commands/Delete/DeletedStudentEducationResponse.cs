using Core.Application.Responses;

namespace Application.Features.StudentEducations.Commands.Delete;

public class DeletedStudentEducationResponse : IResponse
{
    public Guid Id { get; set; }
}