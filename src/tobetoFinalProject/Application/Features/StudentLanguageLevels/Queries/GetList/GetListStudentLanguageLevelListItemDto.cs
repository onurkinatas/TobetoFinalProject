using Core.Application.Dtos;

namespace Application.Features.StudentLanguageLevels.Queries.GetList;

public class GetListStudentLanguageLevelListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid LanguageId { get; set; }
    public Guid LanguageLevelId { get; set; }
    public Guid StudentId { get; set; } 
    public string LanguageLevelName { get; set; }
    public string LanguageName { get; set; }
    public string StudentFirstName { get; set; }
    public string StudentLastName { get; set; }
    public string StudentEmail { get; set; }
}