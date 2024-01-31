using Core.Application.Dtos;

namespace Application.Features.StudentEducations.Queries.GetList;

public class GetListStudentEducationListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public string EducationStatus { get; set; }
    public string SchoolName { get; set; }
    public string Branch { get; set; }
    public bool IsContinued { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string StudentEmail { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime GraduationDate { get; set; }
}