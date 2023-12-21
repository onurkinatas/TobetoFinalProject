using Core.Application.Dtos;

namespace Application.Features.LanguageLevels.Queries.GetList;

public class GetListLanguageLevelListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid LanguageId { get; set; }
    public string Name { get; set; }
}