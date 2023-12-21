using Core.Application.Responses;

namespace Application.Features.StudentExperiences.Commands.Delete;

public class DeletedStudentExperienceResponse : IResponse
{
    public Guid Id { get; set; }
}