using Application.Features.Courses.Constants;
using Application.Features.Courses.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Courses.Rules;

public class CourseBusinessRules : BaseBusinessRules
{
    private readonly ICourseRepository _courseRepository;

    public CourseBusinessRules(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task CourseShouldNotExistsWhenInsert(string name)
    {
        bool doesExists = await _courseRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(CoursesBusinessMessages.CourseNameExists);
    }
    public async Task CourseShouldNotExistsWhenUpdate(string name)
    {
        bool doesExists = await _courseRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(CoursesBusinessMessages.CourseNameExists);
    }
    public Task CourseShouldExistWhenSelected(Course? course)
    {
        if (course == null)
            throw new BusinessException(CoursesBusinessMessages.CourseNotExists);
        return Task.CompletedTask;
    }

    public async Task CourseIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Course? course = await _courseRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CourseShouldExistWhenSelected(course);
    }
}