using Core.Application.Responses;

namespace Application.Features.StudentSkills.Commands.Create;

public class CreatedStudentSkillResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SkillId { get; set; }
}