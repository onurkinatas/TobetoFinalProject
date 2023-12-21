using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Stages;

public interface IStagesService
{
    Task<Stage?> GetAsync(
        Expression<Func<Stage, bool>> predicate,
        Func<IQueryable<Stage>, IIncludableQueryable<Stage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Stage>?> GetListAsync(
        Expression<Func<Stage, bool>>? predicate = null,
        Func<IQueryable<Stage>, IOrderedQueryable<Stage>>? orderBy = null,
        Func<IQueryable<Stage>, IIncludableQueryable<Stage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Stage> AddAsync(Stage stage);
    Task<Stage> UpdateAsync(Stage stage);
    Task<Stage> DeleteAsync(Stage stage, bool permanent = false);
}
