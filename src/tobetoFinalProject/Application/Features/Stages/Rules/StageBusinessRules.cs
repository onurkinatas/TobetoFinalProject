using Application.Features.Stages.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Stages.Rules;

public class StageBusinessRules : BaseBusinessRules
{
    private readonly IStageRepository _stageRepository;

    public StageBusinessRules(IStageRepository stageRepository)
    {
        _stageRepository = stageRepository;
    }

    public Task StageShouldExistWhenSelected(Stage? stage)
    {
        if (stage == null)
            throw new BusinessException(StagesBusinessMessages.StageNotExists);
        return Task.CompletedTask;
    }

    public async Task StageIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Stage? stage = await _stageRepository.GetAsync(
            predicate: s => s.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StageShouldExistWhenSelected(stage);
    }
}