using Application.Features.AppealStages.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.AppealStages.Rules;

public class AppealStageBusinessRules : BaseBusinessRules
{
    private readonly IAppealStageRepository _appealStageRepository;

    public AppealStageBusinessRules(IAppealStageRepository appealStageRepository)
    {
        _appealStageRepository = appealStageRepository;
    }

    public Task AppealStageShouldExistWhenSelected(AppealStage? appealStage)
    {
        if (appealStage == null)
            throw new BusinessException(AppealStagesBusinessMessages.AppealStageNotExists);
        return Task.CompletedTask;
    }

    public async Task AppealStageIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        AppealStage? appealStage = await _appealStageRepository.GetAsync(
            predicate: asd => asd.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await AppealStageShouldExistWhenSelected(appealStage);
    }
}