using Core.Application.Dtos;

namespace Application.Features.StudentSkills.Queries.GetList;

public class GetListStudentSkillListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string SkillName { get; set; }
}