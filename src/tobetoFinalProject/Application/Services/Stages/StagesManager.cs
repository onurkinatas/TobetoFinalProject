using Application.Features.Stages.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Stages;

public class StagesManager : IStagesService
{
    private readonly IStageRepository _stageRepository;
    private readonly StageBusinessRules _stageBusinessRules;

    public StagesManager(IStageRepository stageRepository, StageBusinessRules stageBusinessRules)
    {
        _stageRepository = stageRepository;
        _stageBusinessRules = stageBusinessRules;
    }

    public async Task<Stage?> GetAsync(
        Expression<Func<Stage, bool>> predicate,
        Func<IQueryable<Stage>, IIncludableQueryable<Stage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Stage? stage = await _stageRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return stage;
    }

    public async Task<IPaginate<Stage>?> GetListAsync(
        Expression<Func<Stage, bool>>? predicate = null,
        Func<IQueryable<Stage>, IOrderedQueryable<Stage>>? orderBy = null,
        Func<IQueryable<Stage>, IIncludableQueryable<Stage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Stage> stageList = await _stageRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return stageList;
    }

    public async Task<Stage> AddAsync(Stage stage)
    {
        Stage addedStage = await _stageRepository.AddAsync(stage);

        return addedStage;
    }

    public async Task<Stage> UpdateAsync(Stage stage)
    {
        Stage updatedStage = await _stageRepository.UpdateAsync(stage);

        return updatedStage;
    }

    public async Task<Stage> DeleteAsync(Stage stage, bool permanent = false)
    {
        Stage deletedStage = await _stageRepository.DeleteAsync(stage);

        return deletedStage;
    }
}
