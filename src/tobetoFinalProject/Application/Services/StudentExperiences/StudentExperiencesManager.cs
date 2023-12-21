using Application.Features.StudentExperiences.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentExperiences;

public class StudentExperiencesManager : IStudentExperiencesService
{
    private readonly IStudentExperienceRepository _studentExperienceRepository;
    private readonly StudentExperienceBusinessRules _studentExperienceBusinessRules;

    public StudentExperiencesManager(IStudentExperienceRepository studentExperienceRepository, StudentExperienceBusinessRules studentExperienceBusinessRules)
    {
        _studentExperienceRepository = studentExperienceRepository;
        _studentExperienceBusinessRules = studentExperienceBusinessRules;
    }

    public async Task<StudentExperience?> GetAsync(
        Expression<Func<StudentExperience, bool>> predicate,
        Func<IQueryable<StudentExperience>, IIncludableQueryable<StudentExperience, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentExperience? studentExperience = await _studentExperienceRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentExperience;
    }

    public async Task<IPaginate<StudentExperience>?> GetListAsync(
        Expression<Func<StudentExperience, bool>>? predicate = null,
        Func<IQueryable<StudentExperience>, IOrderedQueryable<StudentExperience>>? orderBy = null,
        Func<IQueryable<StudentExperience>, IIncludableQueryable<StudentExperience, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentExperience> studentExperienceList = await _studentExperienceRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentExperienceList;
    }

    public async Task<StudentExperience> AddAsync(StudentExperience studentExperience)
    {
        StudentExperience addedStudentExperience = await _studentExperienceRepository.AddAsync(studentExperience);

        return addedStudentExperience;
    }

    public async Task<StudentExperience> UpdateAsync(StudentExperience studentExperience)
    {
        StudentExperience updatedStudentExperience = await _studentExperienceRepository.UpdateAsync(studentExperience);

        return updatedStudentExperience;
    }

    public async Task<StudentExperience> DeleteAsync(StudentExperience studentExperience, bool permanent = false)
    {
        StudentExperience deletedStudentExperience = await _studentExperienceRepository.DeleteAsync(studentExperience);

        return deletedStudentExperience;
    }
}
