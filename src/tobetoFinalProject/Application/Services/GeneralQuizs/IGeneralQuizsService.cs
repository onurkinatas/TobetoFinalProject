using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.GeneralQuizs;

public interface IGeneralQuizsService
{
    Task<GeneralQuiz?> GetAsync(
        Expression<Func<GeneralQuiz, bool>> predicate,
        Func<IQueryable<GeneralQuiz>, IIncludableQueryable<GeneralQuiz, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<GeneralQuiz>?> GetListAsync(
        Expression<Func<GeneralQuiz, bool>>? predicate = null,
        Func<IQueryable<GeneralQuiz>, IOrderedQueryable<GeneralQuiz>>? orderBy = null,
        Func<IQueryable<GeneralQuiz>, IIncludableQueryable<GeneralQuiz, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<GeneralQuiz> AddAsync(GeneralQuiz generalQuiz);
    Task<GeneralQuiz> UpdateAsync(GeneralQuiz generalQuiz);
    Task<GeneralQuiz> DeleteAsync(GeneralQuiz generalQuiz, bool permanent = false);
}
