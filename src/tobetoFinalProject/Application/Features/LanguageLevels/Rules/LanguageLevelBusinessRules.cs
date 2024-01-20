using Application.Features.LanguageLevels.Constants;
using Application.Features.LanguageLevels.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.LanguageLevels.Rules;

public class LanguageLevelBusinessRules : BaseBusinessRules
{
    private readonly ILanguageLevelRepository _languageLevelRepository;

    public LanguageLevelBusinessRules(ILanguageLevelRepository languageLevelRepository)
    {
        _languageLevelRepository = languageLevelRepository;
    }

    public Task LanguageLevelShouldExistWhenSelected(LanguageLevel? languageLevel)
    {
        if (languageLevel == null)
            throw new BusinessException(LanguageLevelsBusinessMessages.LanguageLevelNotExists);
        return Task.CompletedTask;
    }

    public async Task LanguageLevelIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LanguageLevel? languageLevel = await _languageLevelRepository.GetAsync(
            predicate: ll => ll.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LanguageLevelShouldExistWhenSelected(languageLevel);
    }


    public async Task LanguageLevelShouldNotExistsWhenInsert(LanguageLevel languageLevel)
    {
        bool doesExists = await _languageLevelRepository
            .AnyAsync(predicate: ca => ca.Name == languageLevel.Name && ca.LanguageId == languageLevel.LanguageId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(LanguageLevelsBusinessMessages.LanguageLevelExists);
    }
    public async Task LanguageLevelShouldNotExistsWhenUpdate(LanguageLevel languageLevel)
    {
        bool doesExists = await _languageLevelRepository
            .AnyAsync(predicate: ca => ca.Name == languageLevel.Name && ca.LanguageId== languageLevel.LanguageId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(LanguageLevelsBusinessMessages.LanguageLevelExists);
    }
}