using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentCertificates;

public interface IStudentCertificatesService
{
    Task<StudentCertificate?> GetAsync(
        Expression<Func<StudentCertificate, bool>> predicate,
        Func<IQueryable<StudentCertificate>, IIncludableQueryable<StudentCertificate, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentCertificate>?> GetListAsync(
        Expression<Func<StudentCertificate, bool>>? predicate = null,
        Func<IQueryable<StudentCertificate>, IOrderedQueryable<StudentCertificate>>? orderBy = null,
        Func<IQueryable<StudentCertificate>, IIncludableQueryable<StudentCertificate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentCertificate> AddAsync(StudentCertificate studentCertificate);
    Task<StudentCertificate> UpdateAsync(StudentCertificate studentCertificate);
    Task<StudentCertificate> DeleteAsync(StudentCertificate studentCertificate, bool permanent = false);
}
