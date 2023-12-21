using Core.Application.Responses;

namespace Application.Features.StudentSkills.Queries.GetById;

public class GetByIdStudentSkillResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SkillId { get; set; }
}