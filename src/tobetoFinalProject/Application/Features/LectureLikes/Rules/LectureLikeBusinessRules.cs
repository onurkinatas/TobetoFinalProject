using Application.Features.LectureLikes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.LectureLikes.Rules;

public class LectureLikeBusinessRules : BaseBusinessRules
{
    private readonly ILectureLikeRepository _lectureLikeRepository;

    public LectureLikeBusinessRules(ILectureLikeRepository lectureLikeRepository)
    {
        _lectureLikeRepository = lectureLikeRepository;
    }

    public Task LectureLikeShouldExistWhenSelected(LectureLike? lectureLike)
    {
        if (lectureLike == null)
            throw new BusinessException(LectureLikesBusinessMessages.LectureLikeNotExists);
        return Task.CompletedTask;
    }

    public async Task LectureLikeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LectureLike? lectureLike = await _lectureLikeRepository.GetAsync(
            predicate: ll => ll.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LectureLikeShouldExistWhenSelected(lectureLike);
    }
}