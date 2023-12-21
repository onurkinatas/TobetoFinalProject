using Core.Application.Dtos;

namespace Application.Features.StudentLanguageLevels.Queries.GetList;

public class GetListStudentLanguageLevelListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid LanguageLevelId { get; set; }
}