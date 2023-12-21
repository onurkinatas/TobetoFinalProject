using Core.Application.Responses;

namespace Application.Features.LanguageLevels.Commands.Create;

public class CreatedLanguageLevelResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid LanguageId { get; set; }
    public string Name { get; set; }
}