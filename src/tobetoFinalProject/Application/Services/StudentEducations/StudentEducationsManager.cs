using Application.Features.StudentEducations.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentEducations;

public class StudentEducationsManager : IStudentEducationsService
{
    private readonly IStudentEducationRepository _studentEducationRepository;
    private readonly StudentEducationBusinessRules _studentEducationBusinessRules;

    public StudentEducationsManager(IStudentEducationRepository studentEducationRepository, StudentEducationBusinessRules studentEducationBusinessRules)
    {
        _studentEducationRepository = studentEducationRepository;
        _studentEducationBusinessRules = studentEducationBusinessRules;
    }

    public async Task<StudentEducation?> GetAsync(
        Expression<Func<StudentEducation, bool>> predicate,
        Func<IQueryable<StudentEducation>, IIncludableQueryable<StudentEducation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentEducation? studentEducation = await _studentEducationRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentEducation;
    }

    public async Task<IPaginate<StudentEducation>?> GetListAsync(
        Expression<Func<StudentEducation, bool>>? predicate = null,
        Func<IQueryable<StudentEducation>, IOrderedQueryable<StudentEducation>>? orderBy = null,
        Func<IQueryable<StudentEducation>, IIncludableQueryable<StudentEducation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentEducation> studentEducationList = await _studentEducationRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentEducationList;
    }

    public async Task<StudentEducation> AddAsync(StudentEducation studentEducation)
    {
        StudentEducation addedStudentEducation = await _studentEducationRepository.AddAsync(studentEducation);

        return addedStudentEducation;
    }

    public async Task<StudentEducation> UpdateAsync(StudentEducation studentEducation)
    {
        StudentEducation updatedStudentEducation = await _studentEducationRepository.UpdateAsync(studentEducation);

        return updatedStudentEducation;
    }

    public async Task<StudentEducation> DeleteAsync(StudentEducation studentEducation, bool permanent = false)
    {
        StudentEducation deletedStudentEducation = await _studentEducationRepository.DeleteAsync(studentEducation);

        return deletedStudentEducation;
    }
}
