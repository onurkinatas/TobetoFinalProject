using Application.Features.GeneralQuizs.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.GeneralQuizs;

public class GeneralQuizsManager : IGeneralQuizsService
{
    private readonly IGeneralQuizRepository _generalQuizRepository;
    private readonly GeneralQuizBusinessRules _generalQuizBusinessRules;

    public GeneralQuizsManager(IGeneralQuizRepository generalQuizRepository, GeneralQuizBusinessRules generalQuizBusinessRules)
    {
        _generalQuizRepository = generalQuizRepository;
        _generalQuizBusinessRules = generalQuizBusinessRules;
    }

    public async Task<GeneralQuiz?> GetAsync(
        Expression<Func<GeneralQuiz, bool>> predicate,
        Func<IQueryable<GeneralQuiz>, IIncludableQueryable<GeneralQuiz, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        GeneralQuiz? generalQuiz = await _generalQuizRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return generalQuiz;
    }

    public async Task<IPaginate<GeneralQuiz>?> GetListAsync(
        Expression<Func<GeneralQuiz, bool>>? predicate = null,
        Func<IQueryable<GeneralQuiz>, IOrderedQueryable<GeneralQuiz>>? orderBy = null,
        Func<IQueryable<GeneralQuiz>, IIncludableQueryable<GeneralQuiz, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<GeneralQuiz> generalQuizList = await _generalQuizRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return generalQuizList;
    }

    public async Task<GeneralQuiz> AddAsync(GeneralQuiz generalQuiz)
    {
        GeneralQuiz addedGeneralQuiz = await _generalQuizRepository.AddAsync(generalQuiz);

        return addedGeneralQuiz;
    }

    public async Task<GeneralQuiz> UpdateAsync(GeneralQuiz generalQuiz)
    {
        GeneralQuiz updatedGeneralQuiz = await _generalQuizRepository.UpdateAsync(generalQuiz);

        return updatedGeneralQuiz;
    }

    public async Task<GeneralQuiz> DeleteAsync(GeneralQuiz generalQuiz, bool permanent = false)
    {
        GeneralQuiz deletedGeneralQuiz = await _generalQuizRepository.DeleteAsync(generalQuiz);

        return deletedGeneralQuiz;
    }
}
