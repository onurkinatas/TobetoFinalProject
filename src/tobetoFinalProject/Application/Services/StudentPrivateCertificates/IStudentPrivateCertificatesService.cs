using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentPrivateCertificates;

public interface IStudentPrivateCertificatesService
{
    Task<StudentPrivateCertificate?> GetAsync(
        Expression<Func<StudentPrivateCertificate, bool>> predicate,
        Func<IQueryable<StudentPrivateCertificate>, IIncludableQueryable<StudentPrivateCertificate, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentPrivateCertificate>?> GetListAsync(
        Expression<Func<StudentPrivateCertificate, bool>>? predicate = null,
        Func<IQueryable<StudentPrivateCertificate>, IOrderedQueryable<StudentPrivateCertificate>>? orderBy = null,
        Func<IQueryable<StudentPrivateCertificate>, IIncludableQueryable<StudentPrivateCertificate, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentPrivateCertificate> AddAsync(StudentPrivateCertificate studentPrivateCertificate);
    Task<StudentPrivateCertificate> UpdateAsync(StudentPrivateCertificate studentPrivateCertificate);
    Task<StudentPrivateCertificate> DeleteAsync(StudentPrivateCertificate studentPrivateCertificate, bool permanent = false);
}
