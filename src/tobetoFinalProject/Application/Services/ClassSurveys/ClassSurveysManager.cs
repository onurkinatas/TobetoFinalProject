using Application.Features.ClassSurveys.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ClassSurveys;

public class ClassSurveysManager : IClassSurveysService
{
    private readonly IClassSurveyRepository _classSurveyRepository;
    private readonly ClassSurveyBusinessRules _classSurveyBusinessRules;

    public ClassSurveysManager(IClassSurveyRepository classSurveyRepository, ClassSurveyBusinessRules classSurveyBusinessRules)
    {
        _classSurveyRepository = classSurveyRepository;
        _classSurveyBusinessRules = classSurveyBusinessRules;
    }

    public async Task<ClassSurvey?> GetAsync(
        Expression<Func<ClassSurvey, bool>> predicate,
        Func<IQueryable<ClassSurvey>, IIncludableQueryable<ClassSurvey, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ClassSurvey? classSurvey = await _classSurveyRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return classSurvey;
    }

    public async Task<IPaginate<ClassSurvey>?> GetListAsync(
        Expression<Func<ClassSurvey, bool>>? predicate = null,
        Func<IQueryable<ClassSurvey>, IOrderedQueryable<ClassSurvey>>? orderBy = null,
        Func<IQueryable<ClassSurvey>, IIncludableQueryable<ClassSurvey, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ClassSurvey> classSurveyList = await _classSurveyRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return classSurveyList;
    }

    public async Task<ClassSurvey> AddAsync(ClassSurvey classSurvey)
    {
        ClassSurvey addedClassSurvey = await _classSurveyRepository.AddAsync(classSurvey);

        return addedClassSurvey;
    }

    public async Task<ClassSurvey> UpdateAsync(ClassSurvey classSurvey)
    {
        ClassSurvey updatedClassSurvey = await _classSurveyRepository.UpdateAsync(classSurvey);

        return updatedClassSurvey;
    }

    public async Task<ClassSurvey> DeleteAsync(ClassSurvey classSurvey, bool permanent = false)
    {
        ClassSurvey deletedClassSurvey = await _classSurveyRepository.DeleteAsync(classSurvey);

        return deletedClassSurvey;
    }
}
