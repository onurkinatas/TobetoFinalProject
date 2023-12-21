using Application.Features.LectureCourses.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.LectureCourses.Rules;

public class LectureCourseBusinessRules : BaseBusinessRules
{
    private readonly ILectureCourseRepository _lectureCourseRepository;

    public LectureCourseBusinessRules(ILectureCourseRepository lectureCourseRepository)
    {
        _lectureCourseRepository = lectureCourseRepository;
    }

    public Task LectureCourseShouldExistWhenSelected(LectureCourse? lectureCourse)
    {
        if (lectureCourse == null)
            throw new BusinessException(LectureCoursesBusinessMessages.LectureCourseNotExists);
        return Task.CompletedTask;
    }

    public async Task LectureCourseIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        LectureCourse? lectureCourse = await _lectureCourseRepository.GetAsync(
            predicate: lc => lc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await LectureCourseShouldExistWhenSelected(lectureCourse);
    }
}