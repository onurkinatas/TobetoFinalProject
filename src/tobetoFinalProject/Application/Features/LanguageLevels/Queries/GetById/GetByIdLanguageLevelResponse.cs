using Core.Application.Responses;

namespace Application.Features.LanguageLevels.Queries.GetById;

public class GetByIdLanguageLevelResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid LanguageId { get; set; }
    public string Name { get; set; }
}