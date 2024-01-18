using Application.Features.Exams.Constants;
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

    public Task LanguageLevelShouldNotExist(LanguageLevel? languageLevel)
    {
        if (languageLevel != null)
            throw new BusinessException(LanguageLevelsBusinessMessages.LanguageLevelNameExists);
        return Task.CompletedTask;
    }
    public async Task DistrictNameShouldNotExist(LanguageLevel languageLevel, CancellationToken cancellationToken)
    {
        LanguageLevel? controlLanguageLevel = await _languageLevelRepository.GetAsync(
            predicate: a => a.Name == languageLevel.Name,
            enableTracking: false, //Entity Framework'te "tracking" veya "izleme" (tracking) terimi, bir veri nesnesinin (entity) durumunu                          takip etme ve bu durumun veritabanýna nasýl yansýtýlacaðýný belirleme sürecini ifade eder.
            cancellationToken: cancellationToken //asenkron iþlemlerin iptal edilmesine olanak saðlar(Örnek çok uzun süren bir iþlemde)
            );
        await LanguageLevelShouldNotExist(controlLanguageLevel);
    }
}