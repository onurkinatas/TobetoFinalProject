using Application.Features.CourseContents.Constants;
using Application.Features.CourseContents.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.CourseContents.Rules;

public class CourseContentBusinessRules : BaseBusinessRules
{
    private readonly ICourseContentRepository _courseContentRepository;

    public CourseContentBusinessRules(ICourseContentRepository courseContentRepository)
    {
        _courseContentRepository = courseContentRepository;
    }
    public async Task CourseContentShouldNotExistsWhenInsert(Guid contentId, Guid courseId)
    {
        bool doesExists = await _courseContentRepository
            .AnyAsync(predicate: ca => ca.CourseId == courseId && ca.ContentId == contentId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(CourseContentsBusinessMessages.CourseContentAlreadyExists);
    }
    public async Task CourseContentShouldNotExistsWhenUpdate(Guid contentId, Guid courseId)
    {
        bool doesExists = await _courseContentRepository
            .AnyAsync(predicate: ca => ca.CourseId == courseId && ca.ContentId == contentId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(CourseContentsBusinessMessages.CourseContentAlreadyExists);
    }
    public Task CourseContentShouldExistWhenSelected(CourseContent? courseContent)
    {
        if (courseContent == null)
            throw new BusinessException(CourseContentsBusinessMessages.CourseContentNotExists);
        return Task.CompletedTask;
    }

    public async Task CourseContentIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        CourseContent? courseContent = await _courseContentRepository.GetAsync(
            predicate: cc => cc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CourseContentShouldExistWhenSelected(courseContent);
    }
}