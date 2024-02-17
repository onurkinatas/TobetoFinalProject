using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ClassQuizs;

public interface IClassQuizsService
{
    Task<ClassQuiz?> GetAsync(
        Expression<Func<ClassQuiz, bool>> predicate,
        Func<IQueryable<ClassQuiz>, IIncludableQueryable<ClassQuiz, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ClassQuiz>?> GetListAsync(
        Expression<Func<ClassQuiz, bool>>? predicate = null,
        Func<IQueryable<ClassQuiz>, IOrderedQueryable<ClassQuiz>>? orderBy = null,
        Func<IQueryable<ClassQuiz>, IIncludableQueryable<ClassQuiz, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ClassQuiz> AddAsync(ClassQuiz classQuiz);
    Task<ClassQuiz> UpdateAsync(ClassQuiz classQuiz);
    Task<ClassQuiz> DeleteAsync(ClassQuiz classQuiz, bool permanent = false);
}
