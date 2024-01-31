using Core.Application.Dtos;

namespace Application.Features.StudentSkills.Queries.GetList;

public class GetListStudentSkillListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid SkillId { get; set; }
    public string SkillName { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string StudentEmail { get; set; }
}