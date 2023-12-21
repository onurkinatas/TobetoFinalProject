using Core.Application.Responses;

namespace Application.Features.LanguageLevels.Commands.Update;

public class UpdatedLanguageLevelResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid LanguageId { get; set; }
    public string Name { get; set; }
}