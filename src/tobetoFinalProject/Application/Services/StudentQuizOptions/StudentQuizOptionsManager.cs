using Application.Features.StudentQuizOptions.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentQuizOptions;

public class StudentQuizOptionsManager : IStudentQuizOptionsService
{
    private readonly IStudentQuizOptionRepository _studentQuizOptionRepository;
    private readonly StudentQuizOptionBusinessRules _studentQuizOptionBusinessRules;

    public StudentQuizOptionsManager(IStudentQuizOptionRepository studentQuizOptionRepository, StudentQuizOptionBusinessRules studentQuizOptionBusinessRules)
    {
        _studentQuizOptionRepository = studentQuizOptionRepository;
        _studentQuizOptionBusinessRules = studentQuizOptionBusinessRules;
    }

    public async Task<StudentQuizOption?> GetAsync(
        Expression<Func<StudentQuizOption, bool>> predicate,
        Func<IQueryable<StudentQuizOption>, IIncludableQueryable<StudentQuizOption, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentQuizOption? studentQuizOption = await _studentQuizOptionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentQuizOption;
    }

    public async Task<IPaginate<StudentQuizOption>?> GetListAsync(
        Expression<Func<StudentQuizOption, bool>>? predicate = null,
        Func<IQueryable<StudentQuizOption>, IOrderedQueryable<StudentQuizOption>>? orderBy = null,
        Func<IQueryable<StudentQuizOption>, IIncludableQueryable<StudentQuizOption, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentQuizOption> studentQuizOptionList = await _studentQuizOptionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentQuizOptionList;
    }

    public async Task<StudentQuizOption> AddAsync(StudentQuizOption studentQuizOption)
    {
        StudentQuizOption addedStudentQuizOption = await _studentQuizOptionRepository.AddAsync(studentQuizOption);

        return addedStudentQuizOption;
    }

    public async Task<StudentQuizOption> UpdateAsync(StudentQuizOption studentQuizOption)
    {
        StudentQuizOption updatedStudentQuizOption = await _studentQuizOptionRepository.UpdateAsync(studentQuizOption);

        return updatedStudentQuizOption;
    }

    public async Task<StudentQuizOption> DeleteAsync(StudentQuizOption studentQuizOption, bool permanent = false)
    {
        StudentQuizOption deletedStudentQuizOption = await _studentQuizOptionRepository.DeleteAsync(studentQuizOption);

        return deletedStudentQuizOption;
    }
}
