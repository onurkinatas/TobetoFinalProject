using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.AppealStages;

public interface IAppealStagesService
{
    Task<AppealStage?> GetAsync(
        Expression<Func<AppealStage, bool>> predicate,
        Func<IQueryable<AppealStage>, IIncludableQueryable<AppealStage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<AppealStage>?> GetListAsync(
        Expression<Func<AppealStage, bool>>? predicate = null,
        Func<IQueryable<AppealStage>, IOrderedQueryable<AppealStage>>? orderBy = null,
        Func<IQueryable<AppealStage>, IIncludableQueryable<AppealStage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<AppealStage> AddAsync(AppealStage appealStage);
    Task<AppealStage> UpdateAsync(AppealStage appealStage);
    Task<AppealStage> DeleteAsync(AppealStage appealStage, bool permanent = false);
}
