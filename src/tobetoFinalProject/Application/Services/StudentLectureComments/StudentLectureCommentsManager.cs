using Application.Features.StudentLectureComments.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentLectureComments;

public class StudentLectureCommentsManager : IStudentLectureCommentsService
{
    private readonly IStudentLectureCommentRepository _studentLectureCommentRepository;
    private readonly StudentLectureCommentBusinessRules _studentLectureCommentBusinessRules;

    public StudentLectureCommentsManager(IStudentLectureCommentRepository studentLectureCommentRepository, StudentLectureCommentBusinessRules studentLectureCommentBusinessRules)
    {
        _studentLectureCommentRepository = studentLectureCommentRepository;
        _studentLectureCommentBusinessRules = studentLectureCommentBusinessRules;
    }

    public async Task<StudentLectureComment?> GetAsync(
        Expression<Func<StudentLectureComment, bool>> predicate,
        Func<IQueryable<StudentLectureComment>, IIncludableQueryable<StudentLectureComment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentLectureComment? studentLectureComment = await _studentLectureCommentRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentLectureComment;
    }

    public async Task<IPaginate<StudentLectureComment>?> GetListAsync(
        Expression<Func<StudentLectureComment, bool>>? predicate = null,
        Func<IQueryable<StudentLectureComment>, IOrderedQueryable<StudentLectureComment>>? orderBy = null,
        Func<IQueryable<StudentLectureComment>, IIncludableQueryable<StudentLectureComment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentLectureComment> studentLectureCommentList = await _studentLectureCommentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentLectureCommentList;
    }

    public async Task<StudentLectureComment> AddAsync(StudentLectureComment studentLectureComment)
    {
        StudentLectureComment addedStudentLectureComment = await _studentLectureCommentRepository.AddAsync(studentLectureComment);

        return addedStudentLectureComment;
    }

    public async Task<StudentLectureComment> UpdateAsync(StudentLectureComment studentLectureComment)
    {
        StudentLectureComment updatedStudentLectureComment = await _studentLectureCommentRepository.UpdateAsync(studentLectureComment);

        return updatedStudentLectureComment;
    }

    public async Task<StudentLectureComment> DeleteAsync(StudentLectureComment studentLectureComment, bool permanent = false)
    {
        StudentLectureComment deletedStudentLectureComment = await _studentLectureCommentRepository.DeleteAsync(studentLectureComment);

        return deletedStudentLectureComment;
    }
}
