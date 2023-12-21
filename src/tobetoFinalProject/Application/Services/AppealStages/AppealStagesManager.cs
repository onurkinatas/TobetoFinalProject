using Application.Features.AppealStages.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AppealStages;

public class AppealStagesManager : IAppealStagesService
{
    private readonly IAppealStageRepository _appealStageRepository;
    private readonly AppealStageBusinessRules _appealStageBusinessRules;

    public AppealStagesManager(IAppealStageRepository appealStageRepository, AppealStageBusinessRules appealStageBusinessRules)
    {
        _appealStageRepository = appealStageRepository;
        _appealStageBusinessRules = appealStageBusinessRules;
    }

    public async Task<AppealStage?> GetAsync(
        Expression<Func<AppealStage, bool>> predicate,
        Func<IQueryable<AppealStage>, IIncludableQueryable<AppealStage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        AppealStage? appealStage = await _appealStageRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return appealStage;
    }

    public async Task<IPaginate<AppealStage>?> GetListAsync(
        Expression<Func<AppealStage, bool>>? predicate = null,
        Func<IQueryable<AppealStage>, IOrderedQueryable<AppealStage>>? orderBy = null,
        Func<IQueryable<AppealStage>, IIncludableQueryable<AppealStage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<AppealStage> appealStageList = await _appealStageRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return appealStageList;
    }

    public async Task<AppealStage> AddAsync(AppealStage appealStage)
    {
        AppealStage addedAppealStage = await _appealStageRepository.AddAsync(appealStage);

        return addedAppealStage;
    }

    public async Task<AppealStage> UpdateAsync(AppealStage appealStage)
    {
        AppealStage updatedAppealStage = await _appealStageRepository.UpdateAsync(appealStage);

        return updatedAppealStage;
    }

    public async Task<AppealStage> DeleteAsync(AppealStage appealStage, bool permanent = false)
    {
        AppealStage deletedAppealStage = await _appealStageRepository.DeleteAsync(appealStage);

        return deletedAppealStage;
    }
}
