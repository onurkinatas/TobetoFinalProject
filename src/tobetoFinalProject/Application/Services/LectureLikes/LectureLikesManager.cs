using Application.Features.LectureLikes.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Threading;

namespace Application.Services.LectureLikes;

public class LectureLikesManager : ILectureLikesService
{
    private readonly ILectureLikeRepository _lectureLikeRepository;
    private readonly LectureLikeBusinessRules _lectureLikeBusinessRules;

    public LectureLikesManager(ILectureLikeRepository lectureLikeRepository, LectureLikeBusinessRules lectureLikeBusinessRules)
    {
        _lectureLikeRepository = lectureLikeRepository;
        _lectureLikeBusinessRules = lectureLikeBusinessRules;
    }

    public async Task<LectureLike?> GetAsync(
        Expression<Func<LectureLike, bool>> predicate,
        Func<IQueryable<LectureLike>, IIncludableQueryable<LectureLike, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        LectureLike? lectureLike = await _lectureLikeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return lectureLike;
    }

    public async Task<IPaginate<LectureLike>?> GetListAsync(
        Expression<Func<LectureLike, bool>>? predicate = null,
        Func<IQueryable<LectureLike>, IOrderedQueryable<LectureLike>>? orderBy = null,
        Func<IQueryable<LectureLike>, IIncludableQueryable<LectureLike, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<LectureLike> lectureLikeList = await _lectureLikeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return lectureLikeList;
    }

    public async Task<LectureLike> AddAsync(LectureLike lectureLike)
    {
        LectureLike addedLectureLike = await _lectureLikeRepository.AddAsync(lectureLike);

        return addedLectureLike;
    }

    public async Task<LectureLike> UpdateAsync(LectureLike lectureLike)
    {
        LectureLike updatedLectureLike = await _lectureLikeRepository.UpdateAsync(lectureLike);

        return updatedLectureLike;
    }

    public async Task<LectureLike> DeleteAsync(LectureLike lectureLike, bool permanent = false)
    {
        LectureLike deletedLectureLike = await _lectureLikeRepository.DeleteAsync(lectureLike);

        return deletedLectureLike;
    }
    public async Task<int> GetCount(Guid lectureId)
    {
        int lectureLikeCount =  _lectureLikeRepository.GetLectureLikeCount(ll=>ll.LectureId ==lectureId);

        return lectureLikeCount;
    }
    public async Task<bool> IsLiked(Guid lectureId,Guid studentId)
    {
        LectureLike? lectureLike = await _lectureLikeRepository.GetAsync(
                predicate: ll => ll.LectureId == lectureId && ll.StudentId == studentId
                );
        return lectureLike.IsLiked;
    }
    
}
