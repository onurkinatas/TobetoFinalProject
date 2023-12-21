using Application.Features.ClassExams.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ClassExams;

public class ClassExamsManager : IClassExamsService
{
    private readonly IClassExamRepository _classExamRepository;
    private readonly ClassExamBusinessRules _classExamBusinessRules;

    public ClassExamsManager(IClassExamRepository classExamRepository, ClassExamBusinessRules classExamBusinessRules)
    {
        _classExamRepository = classExamRepository;
        _classExamBusinessRules = classExamBusinessRules;
    }

    public async Task<ClassExam?> GetAsync(
        Expression<Func<ClassExam, bool>> predicate,
        Func<IQueryable<ClassExam>, IIncludableQueryable<ClassExam, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ClassExam? classExam = await _classExamRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return classExam;
    }

    public async Task<IPaginate<ClassExam>?> GetListAsync(
        Expression<Func<ClassExam, bool>>? predicate = null,
        Func<IQueryable<ClassExam>, IOrderedQueryable<ClassExam>>? orderBy = null,
        Func<IQueryable<ClassExam>, IIncludableQueryable<ClassExam, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ClassExam> classExamList = await _classExamRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return classExamList;
    }

    public async Task<ClassExam> AddAsync(ClassExam classExam)
    {
        ClassExam addedClassExam = await _classExamRepository.AddAsync(classExam);

        return addedClassExam;
    }

    public async Task<ClassExam> UpdateAsync(ClassExam classExam)
    {
        ClassExam updatedClassExam = await _classExamRepository.UpdateAsync(classExam);

        return updatedClassExam;
    }

    public async Task<ClassExam> DeleteAsync(ClassExam classExam, bool permanent = false)
    {
        ClassExam deletedClassExam = await _classExamRepository.DeleteAsync(classExam);

        return deletedClassExam;
    }
}
