using Application.Features.StudentPrivateCertificates.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentPrivateCertificates;

public class StudentPrivateCertificatesManager : IStudentPrivateCertificatesService
{
    private readonly IStudentPrivateCertificateRepository _studentPrivateCertificateRepository;
    private readonly StudentPrivateCertificateBusinessRules _studentPrivateCertificateBusinessRules;

    public StudentPrivateCertificatesManager(IStudentPrivateCertificateRepository studentPrivateCertificateRepository, StudentPrivateCertificateBusinessRules studentPrivateCertificateBusinessRules)
    {
        _studentPrivateCertificateRepository = studentPrivateCertificateRepository;
        _studentPrivateCertificateBusinessRules = studentPrivateCertificateBusinessRules;
    }

    public async Task<StudentPrivateCertificate?> GetAsync(
        Expression<Func<StudentPrivateCertificate, bool>> predicate,
        Func<IQueryable<StudentPrivateCertificate>, IIncludableQueryable<StudentPrivateCertificate, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentPrivateCertificate? studentPrivateCertificate = await _studentPrivateCertificateRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentPrivateCertificate;
    }

    public async Task<IPaginate<StudentPrivateCertificate>?> GetListAsync(
        Expression<Func<StudentPrivateCertificate, bool>>? predicate = null,
        Func<IQueryable<StudentPrivateCertificate>, IOrderedQueryable<StudentPrivateCertificate>>? orderBy = null,
        Func<IQueryable<StudentPrivateCertificate>, IIncludableQueryable<StudentPrivateCertificate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentPrivateCertificate> studentPrivateCertificateList = await _studentPrivateCertificateRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentPrivateCertificateList;
    }

    public async Task<StudentPrivateCertificate> AddAsync(StudentPrivateCertificate studentPrivateCertificate)
    {
        StudentPrivateCertificate addedStudentPrivateCertificate = await _studentPrivateCertificateRepository.AddAsync(studentPrivateCertificate);

        return addedStudentPrivateCertificate;
    }

    public async Task<StudentPrivateCertificate> UpdateAsync(StudentPrivateCertificate studentPrivateCertificate)
    {
        StudentPrivateCertificate updatedStudentPrivateCertificate = await _studentPrivateCertificateRepository.UpdateAsync(studentPrivateCertificate);

        return updatedStudentPrivateCertificate;
    }

    public async Task<StudentPrivateCertificate> DeleteAsync(StudentPrivateCertificate studentPrivateCertificate, bool permanent = false)
    {
        StudentPrivateCertificate deletedStudentPrivateCertificate = await _studentPrivateCertificateRepository.DeleteAsync(studentPrivateCertificate);

        return deletedStudentPrivateCertificate;
    }
}
