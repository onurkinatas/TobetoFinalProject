using Core.Application.Responses;

namespace Application.Features.StudentSkills.Commands.Update;

public class UpdatedStudentSkillResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SkillId { get; set; }
}