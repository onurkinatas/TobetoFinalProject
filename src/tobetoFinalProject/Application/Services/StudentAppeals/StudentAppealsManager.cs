using Application.Features.StudentAppeals.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentAppeals;

public class StudentAppealsManager : IStudentAppealsService
{
    private readonly IStudentAppealRepository _studentAppealRepository;
    private readonly StudentAppealBusinessRules _studentAppealBusinessRules;

    public StudentAppealsManager(IStudentAppealRepository studentAppealRepository, StudentAppealBusinessRules studentAppealBusinessRules)
    {
        _studentAppealRepository = studentAppealRepository;
        _studentAppealBusinessRules = studentAppealBusinessRules;
    }

    public async Task<StudentAppeal?> GetAsync(
        Expression<Func<StudentAppeal, bool>> predicate,
        Func<IQueryable<StudentAppeal>, IIncludableQueryable<StudentAppeal, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentAppeal? studentAppeal = await _studentAppealRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentAppeal;
    }

    public async Task<IPaginate<StudentAppeal>?> GetListAsync(
        Expression<Func<StudentAppeal, bool>>? predicate = null,
        Func<IQueryable<StudentAppeal>, IOrderedQueryable<StudentAppeal>>? orderBy = null,
        Func<IQueryable<StudentAppeal>, IIncludableQueryable<StudentAppeal, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentAppeal> studentAppealList = await _studentAppealRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentAppealList;
    }

    public async Task<StudentAppeal> AddAsync(StudentAppeal studentAppeal)
    {
        StudentAppeal addedStudentAppeal = await _studentAppealRepository.AddAsync(studentAppeal);

        return addedStudentAppeal;
    }

    public async Task<StudentAppeal> UpdateAsync(StudentAppeal studentAppeal)
    {
        StudentAppeal updatedStudentAppeal = await _studentAppealRepository.UpdateAsync(studentAppeal);

        return updatedStudentAppeal;
    }

    public async Task<StudentAppeal> DeleteAsync(StudentAppeal studentAppeal, bool permanent = false)
    {
        StudentAppeal deletedStudentAppeal = await _studentAppealRepository.DeleteAsync(studentAppeal);

        return deletedStudentAppeal;
    }
}
