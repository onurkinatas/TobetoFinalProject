using Application.Features.StudentStages.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentStages;

public class StudentStagesManager : IStudentStagesService
{
    private readonly IStudentStageRepository _studentStageRepository;
    private readonly StudentStageBusinessRules _studentStageBusinessRules;

    public StudentStagesManager(IStudentStageRepository studentStageRepository, StudentStageBusinessRules studentStageBusinessRules)
    {
        _studentStageRepository = studentStageRepository;
        _studentStageBusinessRules = studentStageBusinessRules;
    }

    public async Task<StudentStage?> GetAsync(
        Expression<Func<StudentStage, bool>> predicate,
        Func<IQueryable<StudentStage>, IIncludableQueryable<StudentStage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentStage? studentStage = await _studentStageRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentStage;
    }

    public async Task<IPaginate<StudentStage>?> GetListAsync(
        Expression<Func<StudentStage, bool>>? predicate = null,
        Func<IQueryable<StudentStage>, IOrderedQueryable<StudentStage>>? orderBy = null,
        Func<IQueryable<StudentStage>, IIncludableQueryable<StudentStage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentStage> studentStageList = await _studentStageRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentStageList;
    }

    public async Task<StudentStage> AddAsync(StudentStage studentStage)
    {
        StudentStage addedStudentStage = await _studentStageRepository.AddAsync(studentStage);

        return addedStudentStage;
    }

    public async Task<StudentStage> UpdateAsync(StudentStage studentStage)
    {
        StudentStage updatedStudentStage = await _studentStageRepository.UpdateAsync(studentStage);

        return updatedStudentStage;
    }

    public async Task<StudentStage> DeleteAsync(StudentStage studentStage, bool permanent = false)
    {
        StudentStage deletedStudentStage = await _studentStageRepository.DeleteAsync(studentStage);

        return deletedStudentStage;
    }
}
