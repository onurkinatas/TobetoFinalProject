using Application.Features.ClassQuizs.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ClassQuizs;

public class ClassQuizsManager : IClassQuizsService
{
    private readonly IClassQuizRepository _classQuizRepository;
    private readonly ClassQuizBusinessRules _classQuizBusinessRules;

    public ClassQuizsManager(IClassQuizRepository classQuizRepository, ClassQuizBusinessRules classQuizBusinessRules)
    {
        _classQuizRepository = classQuizRepository;
        _classQuizBusinessRules = classQuizBusinessRules;
    }

    public async Task<ClassQuiz?> GetAsync(
        Expression<Func<ClassQuiz, bool>> predicate,
        Func<IQueryable<ClassQuiz>, IIncludableQueryable<ClassQuiz, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ClassQuiz? classQuiz = await _classQuizRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return classQuiz;
    }

    public async Task<IPaginate<ClassQuiz>?> GetListAsync(
        Expression<Func<ClassQuiz, bool>>? predicate = null,
        Func<IQueryable<ClassQuiz>, IOrderedQueryable<ClassQuiz>>? orderBy = null,
        Func<IQueryable<ClassQuiz>, IIncludableQueryable<ClassQuiz, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ClassQuiz> classQuizList = await _classQuizRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return classQuizList;
    }

    public async Task<ClassQuiz> AddAsync(ClassQuiz classQuiz)
    {
        ClassQuiz addedClassQuiz = await _classQuizRepository.AddAsync(classQuiz);

        return addedClassQuiz;
    }

    public async Task<ClassQuiz> UpdateAsync(ClassQuiz classQuiz)
    {
        ClassQuiz updatedClassQuiz = await _classQuizRepository.UpdateAsync(classQuiz);

        return updatedClassQuiz;
    }

    public async Task<ClassQuiz> DeleteAsync(ClassQuiz classQuiz, bool permanent = false)
    {
        ClassQuiz deletedClassQuiz = await _classQuizRepository.DeleteAsync(classQuiz);

        return deletedClassQuiz;
    }
}
