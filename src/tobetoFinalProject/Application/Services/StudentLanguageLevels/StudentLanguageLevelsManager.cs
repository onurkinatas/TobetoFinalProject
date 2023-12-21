using Application.Features.StudentLanguageLevels.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentLanguageLevels;

public class StudentLanguageLevelsManager : IStudentLanguageLevelsService
{
    private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;
    private readonly StudentLanguageLevelBusinessRules _studentLanguageLevelBusinessRules;

    public StudentLanguageLevelsManager(IStudentLanguageLevelRepository studentLanguageLevelRepository, StudentLanguageLevelBusinessRules studentLanguageLevelBusinessRules)
    {
        _studentLanguageLevelRepository = studentLanguageLevelRepository;
        _studentLanguageLevelBusinessRules = studentLanguageLevelBusinessRules;
    }

    public async Task<StudentLanguageLevel?> GetAsync(
        Expression<Func<StudentLanguageLevel, bool>> predicate,
        Func<IQueryable<StudentLanguageLevel>, IIncludableQueryable<StudentLanguageLevel, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentLanguageLevel? studentLanguageLevel = await _studentLanguageLevelRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentLanguageLevel;
    }

    public async Task<IPaginate<StudentLanguageLevel>?> GetListAsync(
        Expression<Func<StudentLanguageLevel, bool>>? predicate = null,
        Func<IQueryable<StudentLanguageLevel>, IOrderedQueryable<StudentLanguageLevel>>? orderBy = null,
        Func<IQueryable<StudentLanguageLevel>, IIncludableQueryable<StudentLanguageLevel, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentLanguageLevel> studentLanguageLevelList = await _studentLanguageLevelRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentLanguageLevelList;
    }

    public async Task<StudentLanguageLevel> AddAsync(StudentLanguageLevel studentLanguageLevel)
    {
        StudentLanguageLevel addedStudentLanguageLevel = await _studentLanguageLevelRepository.AddAsync(studentLanguageLevel);

        return addedStudentLanguageLevel;
    }

    public async Task<StudentLanguageLevel> UpdateAsync(StudentLanguageLevel studentLanguageLevel)
    {
        StudentLanguageLevel updatedStudentLanguageLevel = await _studentLanguageLevelRepository.UpdateAsync(studentLanguageLevel);

        return updatedStudentLanguageLevel;
    }

    public async Task<StudentLanguageLevel> DeleteAsync(StudentLanguageLevel studentLanguageLevel, bool permanent = false)
    {
        StudentLanguageLevel deletedStudentLanguageLevel = await _studentLanguageLevelRepository.DeleteAsync(studentLanguageLevel);

        return deletedStudentLanguageLevel;
    }
}
