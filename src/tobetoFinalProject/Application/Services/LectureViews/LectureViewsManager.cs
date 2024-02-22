using Application.Features.LectureViews.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LectureViews;

public class LectureViewsManager : ILectureViewsService
{
    private readonly ILectureViewRepository _lectureViewRepository;
    private readonly LectureViewBusinessRules _lectureViewBusinessRules;

    public LectureViewsManager(ILectureViewRepository lectureViewRepository, LectureViewBusinessRules lectureViewBusinessRules)
    {
        _lectureViewRepository = lectureViewRepository;
        _lectureViewBusinessRules = lectureViewBusinessRules;
    }

    public async Task<LectureView?> GetAsync(
        Expression<Func<LectureView, bool>> predicate,
        Func<IQueryable<LectureView>, IIncludableQueryable<LectureView, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LectureView? lectureView = await _lectureViewRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return lectureView;
    }

    public async Task<IPaginate<LectureView>?> GetListAsync(
        Expression<Func<LectureView, bool>>? predicate = null,
        Func<IQueryable<LectureView>, IOrderedQueryable<LectureView>>? orderBy = null,
        Func<IQueryable<LectureView>, IIncludableQueryable<LectureView, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LectureView> lectureViewList = await _lectureViewRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return lectureViewList;
    }

    public async Task<LectureView> AddAsync(LectureView lectureView)
    {
        LectureView addedLectureView = await _lectureViewRepository.AddAsync(lectureView);

        return addedLectureView;
    }

    public async Task<LectureView> UpdateAsync(LectureView lectureView)
    {
        LectureView updatedLectureView = await _lectureViewRepository.UpdateAsync(lectureView);

        return updatedLectureView;
    }

    public async Task<LectureView> DeleteAsync(LectureView lectureView, bool permanent = false)
    {
        LectureView deletedLectureView = await _lectureViewRepository.DeleteAsync(lectureView);

        return deletedLectureView;
    }

    public async Task<int> ContentViewedByLectureId(Guid lectureId, Guid studentId) 
    {
        var lectureViews = await _lectureViewRepository.GetAll(lv => lv.LectureId == lectureId && lv.StudentId == studentId);
        int lectureViewCount = lectureViews.Count;
        return lectureViewCount;
    }
    public int ContentViewedCountByLectureId(Guid lectureId, Guid studentId)
    {
        int lectureViewCount =  _lectureViewRepository.GetCount(lv => lv.LectureId == lectureId && lv.StudentId == studentId);
        return lectureViewCount;
    }
}
