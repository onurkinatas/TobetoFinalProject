using Application.Features.LectureCourses.Constants;
using Application.Features.LectureCourses.Constants;
using Application.Services.Courses;
using Application.Services.Lectures;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.LectureCourses.Rules;

public class LectureCourseBusinessRules : BaseBusinessRules
{
    private readonly ILectureCourseRepository _lectureCourseRepository;
    private readonly ILecturesService _lectureService;
    private readonly ICoursesService _courseService;

    public LectureCourseBusinessRules(ILectureCourseRepository lectureCourseRepository, ILecturesService lectureService, ICoursesService courseService)
    {
        _courseService = courseService;
        _lectureService = lectureService;
        _lectureCourseRepository = lectureCourseRepository;
    }
    public async Task LectureCourseShouldNotExistsWhenInsert(Guid lectureId, Guid courseId)
    {
        bool doesExists = await _lectureCourseRepository
            .AnyAsync(predicate: ca => ca.CourseId == courseId && ca.LectureId == lectureId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(LectureCoursesBusinessMessages.LectureCourseAlreadyExists);
    }
    public async Task LectureCourseShouldNotExistsWhenUpdate(Guid lectureId, Guid courseId)
    {
        bool doesExists = await _lectureCourseRepository
            .AnyAsync(predicate: ca => ca.CourseId == courseId && ca.LectureId == lectureId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(LectureCoursesBusinessMessages.LectureCourseAlreadyExists);
    }
    public async Task LectureCourseContentShouldNotExistsWhenInsert(Guid lectureId)
    {
        //yapilacak
    }
    public async Task LectureCourseContentShouldNotExistsWhenUpdate(Guid lectureId)
    {
       //yapilacak
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