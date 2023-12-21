using Application.Features.ClassAnnouncements.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ClassAnnouncements;

public class ClassAnnouncementsManager : IClassAnnouncementsService
{
    private readonly IClassAnnouncementRepository _classAnnouncementRepository;
    private readonly ClassAnnouncementBusinessRules _classAnnouncementBusinessRules;

    public ClassAnnouncementsManager(IClassAnnouncementRepository classAnnouncementRepository, ClassAnnouncementBusinessRules classAnnouncementBusinessRules)
    {
        _classAnnouncementRepository = classAnnouncementRepository;
        _classAnnouncementBusinessRules = classAnnouncementBusinessRules;
    }

    public async Task<ClassAnnouncement?> GetAsync(
        Expression<Func<ClassAnnouncement, bool>> predicate,
        Func<IQueryable<ClassAnnouncement>, IIncludableQueryable<ClassAnnouncement, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ClassAnnouncement? classAnnouncement = await _classAnnouncementRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return classAnnouncement;
    }

    public async Task<IPaginate<ClassAnnouncement>?> GetListAsync(
        Expression<Func<ClassAnnouncement, bool>>? predicate = null,
        Func<IQueryable<ClassAnnouncement>, IOrderedQueryable<ClassAnnouncement>>? orderBy = null,
        Func<IQueryable<ClassAnnouncement>, IIncludableQueryable<ClassAnnouncement, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ClassAnnouncement> classAnnouncementList = await _classAnnouncementRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return classAnnouncementList;
    }

    public async Task<ClassAnnouncement> AddAsync(ClassAnnouncement classAnnouncement)
    {
        ClassAnnouncement addedClassAnnouncement = await _classAnnouncementRepository.AddAsync(classAnnouncement);

        return addedClassAnnouncement;
    }

    public async Task<ClassAnnouncement> UpdateAsync(ClassAnnouncement classAnnouncement)
    {
        ClassAnnouncement updatedClassAnnouncement = await _classAnnouncementRepository.UpdateAsync(classAnnouncement);

        return updatedClassAnnouncement;
    }

    public async Task<ClassAnnouncement> DeleteAsync(ClassAnnouncement classAnnouncement, bool permanent = false)
    {
        ClassAnnouncement deletedClassAnnouncement = await _classAnnouncementRepository.DeleteAsync(classAnnouncement);

        return deletedClassAnnouncement;
    }
}
