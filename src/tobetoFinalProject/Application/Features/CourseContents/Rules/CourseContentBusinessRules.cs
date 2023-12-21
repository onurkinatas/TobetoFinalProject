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