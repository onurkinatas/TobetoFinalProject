using Application.Features.StudentAnnouncements.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentAnnouncements;

public class StudentAnnouncementsManager : IStudentAnnouncementsService
{
    private readonly IStudentAnnouncementRepository _studentAnnouncementRepository;
    private readonly StudentAnnouncementBusinessRules _studentAnnouncementBusinessRules;
    private readonly IContextOperationService _contextOperationService;
    public StudentAnnouncementsManager(IStudentAnnouncementRepository studentAnnouncementRepository, StudentAnnouncementBusinessRules studentAnnouncementBusinessRules, IContextOperationService contextOperationService)
    {
        _studentAnnouncementRepository = studentAnnouncementRepository;
        _studentAnnouncementBusinessRules = studentAnnouncementBusinessRules;
        _contextOperationService = contextOperationService;
    }

    public async Task<StudentAnnouncement?> GetAsync(
        Expression<Func<StudentAnnouncement, bool>> predicate,
        Func<IQueryable<StudentAnnouncement>, IIncludableQueryable<StudentAnnouncement, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentAnnouncement? studentAnnouncement = await _studentAnnouncementRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentAnnouncement;
    }

    public async Task<IPaginate<StudentAnnouncement>?> GetListAsync(
        Expression<Func<StudentAnnouncement, bool>>? predicate = null,
        Func<IQueryable<StudentAnnouncement>, IOrderedQueryable<StudentAnnouncement>>? orderBy = null,
        Func<IQueryable<StudentAnnouncement>, IIncludableQueryable<StudentAnnouncement, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentAnnouncement> studentAnnouncementList = await _studentAnnouncementRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentAnnouncementList;
    }

    public async Task<StudentAnnouncement> AddAsync(StudentAnnouncement studentAnnouncement)
    {
        StudentAnnouncement addedStudentAnnouncement = await _studentAnnouncementRepository.AddAsync(studentAnnouncement);

        return addedStudentAnnouncement;
    }

    public async Task<StudentAnnouncement> UpdateAsync(StudentAnnouncement studentAnnouncement)
    {
        StudentAnnouncement updatedStudentAnnouncement = await _studentAnnouncementRepository.UpdateAsync(studentAnnouncement);

        return updatedStudentAnnouncement;
    }

    public async Task<StudentAnnouncement> DeleteAsync(StudentAnnouncement studentAnnouncement, bool permanent = false)
    {
        StudentAnnouncement deletedStudentAnnouncement = await _studentAnnouncementRepository.DeleteAsync(studentAnnouncement);

        return deletedStudentAnnouncement;
    }
    public async Task<List<StudentAnnouncement>> GetAllAsync(Expression<Func<StudentAnnouncement, bool>> filter = null)
    {
        return await _studentAnnouncementRepository.GetAll(filter);
    }

    public async Task<int> GetReadingAnnouncementCount(int allClassAnnouncementCount)
    {
        Student getStudent = await _contextOperationService.GetStudentFromContext();
        List<StudentAnnouncement> studentAnnouncements = await _studentAnnouncementRepository.GetAll(ss => ss.StudentId == getStudent.Id);
        int readingAnnouncement = (allClassAnnouncementCount - studentAnnouncements.Count);

        readingAnnouncement = readingAnnouncement < 0 ? 0 : readingAnnouncement;
        return readingAnnouncement;
    }
}
