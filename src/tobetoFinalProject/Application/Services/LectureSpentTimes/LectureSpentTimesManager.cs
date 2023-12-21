using Application.Features.LectureSpentTimes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.LectureSpentTimes;

public class LectureSpentTimesManager : ILectureSpentTimesService
{
    private readonly ILectureSpentTimeRepository _lectureSpentTimeRepository;
    private readonly LectureSpentTimeBusinessRules _lectureSpentTimeBusinessRules;

    public LectureSpentTimesManager(ILectureSpentTimeRepository lectureSpentTimeRepository, LectureSpentTimeBusinessRules lectureSpentTimeBusinessRules)
    {
        _lectureSpentTimeRepository = lectureSpentTimeRepository;
        _lectureSpentTimeBusinessRules = lectureSpentTimeBusinessRules;
    }

    public async Task<LectureSpentTime?> GetAsync(
        Expression<Func<LectureSpentTime, bool>> predicate,
        Func<IQueryable<LectureSpentTime>, IIncludableQueryable<LectureSpentTime, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LectureSpentTime? lectureSpentTime = await _lectureSpentTimeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return lectureSpentTime;
    }

    public async Task<IPaginate<LectureSpentTime>?> GetListAsync(
        Expression<Func<LectureSpentTime, bool>>? predicate = null,
        Func<IQueryable<LectureSpentTime>, IOrderedQueryable<LectureSpentTime>>? orderBy = null,
        Func<IQueryable<LectureSpentTime>, IIncludableQueryable<LectureSpentTime, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LectureSpentTime> lectureSpentTimeList = await _lectureSpentTimeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return lectureSpentTimeList;
    }

    public async Task<LectureSpentTime> AddAsync(LectureSpentTime lectureSpentTime)
    {
        LectureSpentTime addedLectureSpentTime = await _lectureSpentTimeRepository.AddAsync(lectureSpentTime);

        return addedLectureSpentTime;
    }

    public async Task<LectureSpentTime> UpdateAsync(LectureSpentTime lectureSpentTime)
    {
        LectureSpentTime updatedLectureSpentTime = await _lectureSpentTimeRepository.UpdateAsync(lectureSpentTime);

        return updatedLectureSpentTime;
    }

    public async Task<LectureSpentTime> DeleteAsync(LectureSpentTime lectureSpentTime, bool permanent = false)
    {
        LectureSpentTime deletedLectureSpentTime = await _lectureSpentTimeRepository.DeleteAsync(lectureSpentTime);

        return deletedLectureSpentTime;
    }
}
