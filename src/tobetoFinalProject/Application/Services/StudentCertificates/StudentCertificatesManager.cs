using Application.Features.StudentCertificates.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentCertificates;

public class StudentCertificatesManager : IStudentCertificatesService
{
    private readonly IStudentCertificateRepository _studentCertificateRepository;
    private readonly StudentCertificateBusinessRules _studentCertificateBusinessRules;

    public StudentCertificatesManager(IStudentCertificateRepository studentCertificateRepository, StudentCertificateBusinessRules studentCertificateBusinessRules)
    {
        _studentCertificateRepository = studentCertificateRepository;
        _studentCertificateBusinessRules = studentCertificateBusinessRules;
    }

    public async Task<StudentCertificate?> GetAsync(
        Expression<Func<StudentCertificate, bool>> predicate,
        Func<IQueryable<StudentCertificate>, IIncludableQueryable<StudentCertificate, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentCertificate? studentCertificate = await _studentCertificateRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentCertificate;
    }

    public async Task<IPaginate<StudentCertificate>?> GetListAsync(
        Expression<Func<StudentCertificate, bool>>? predicate = null,
        Func<IQueryable<StudentCertificate>, IOrderedQueryable<StudentCertificate>>? orderBy = null,
        Func<IQueryable<StudentCertificate>, IIncludableQueryable<StudentCertificate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentCertificate> studentCertificateList = await _studentCertificateRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentCertificateList;
    }

    public async Task<StudentCertificate> AddAsync(StudentCertificate studentCertificate)
    {
        StudentCertificate addedStudentCertificate = await _studentCertificateRepository.AddAsync(studentCertificate);

        return addedStudentCertificate;
    }

    public async Task<StudentCertificate> UpdateAsync(StudentCertificate studentCertificate)
    {
        StudentCertificate updatedStudentCertificate = await _studentCertificateRepository.UpdateAsync(studentCertificate);

        return updatedStudentCertificate;
    }

    public async Task<StudentCertificate> DeleteAsync(StudentCertificate studentCertificate, bool permanent = false)
    {
        StudentCertificate deletedStudentCertificate = await _studentCertificateRepository.DeleteAsync(studentCertificate);

        return deletedStudentCertificate;
    }
}
