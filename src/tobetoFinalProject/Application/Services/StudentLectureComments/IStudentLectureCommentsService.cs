using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentLectureComments;

public interface IStudentLectureCommentsService
{
    Task<StudentLectureComment?> GetAsync(
        Expression<Func<StudentLectureComment, bool>> predicate,
        Func<IQueryable<StudentLectureComment>, IIncludableQueryable<StudentLectureComment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<StudentLectureComment>?> GetListAsync(
        Expression<Func<StudentLectureComment, bool>>? predicate = null,
        Func<IQueryable<StudentLectureComment>, IOrderedQueryable<StudentLectureComment>>? orderBy = null,
        Func<IQueryable<StudentLectureComment>, IIncludableQueryable<StudentLectureComment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<StudentLectureComment> AddAsync(StudentLectureComment studentLectureComment);
    Task<StudentLectureComment> UpdateAsync(StudentLectureComment studentLectureComment);
    Task<StudentLectureComment> DeleteAsync(StudentLectureComment studentLectureComment, bool permanent = false);
}
