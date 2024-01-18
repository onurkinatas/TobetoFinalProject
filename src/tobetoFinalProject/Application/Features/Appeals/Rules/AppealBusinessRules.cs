using Application.Features.Appeals.Constants;
using Application.Features.Languages.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Appeals.Rules;

public class AppealBusinessRules : BaseBusinessRules
{
    private readonly IAppealRepository _appealRepository;

    public AppealBusinessRules(IAppealRepository appealRepository)
    {
        _appealRepository = appealRepository;
    }

    public Task AppealShouldExistWhenSelected(Appeal? appeal)
    {
        if (appeal == null)
            throw new BusinessException(AppealsBusinessMessages.AppealNotExists);
        return Task.CompletedTask;
    }

    public async Task AppealIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Appeal? appeal = await _appealRepository.GetAsync(
            predicate: a => a.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AppealShouldExistWhenSelected(appeal);
    }

    public Task AppealShouldNotExist(Appeal? appeal)
    {
        if (appeal != null)
            throw new BusinessException(AppealsBusinessMessages.AppealNameExists);
        return Task.CompletedTask;
    }
    public async Task AppealNameShouldNotExist(Appeal appeal, CancellationToken cancellationToken)
    {
        Appeal? controlAppeal = await _appealRepository.GetAsync(
            predicate: a => a.Name == appeal.Name,
            enableTracking: false,
            cancellationToken: cancellationToken
            );
        await AppealShouldNotExist(controlAppeal);
    }

}