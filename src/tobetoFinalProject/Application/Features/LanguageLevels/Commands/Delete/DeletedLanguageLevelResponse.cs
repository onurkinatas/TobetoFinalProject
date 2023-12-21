using Core.Application.Responses;

namespace Application.Features.LanguageLevels.Commands.Delete;

public class DeletedLanguageLevelResponse : IResponse
{
    public Guid Id { get; set; }
}