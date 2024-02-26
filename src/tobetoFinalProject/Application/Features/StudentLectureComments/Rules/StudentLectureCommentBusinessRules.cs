using Application.Features.StudentLectureComments.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentLectureComments.Rules;

public class StudentLectureCommentBusinessRules : BaseBusinessRules
{
    private readonly IStudentLectureCommentRepository _studentLectureCommentRepository;

    public StudentLectureCommentBusinessRules(IStudentLectureCommentRepository studentLectureCommentRepository)
    {
        _studentLectureCommentRepository = studentLectureCommentRepository;
    }

    public Task StudentLectureCommentShouldExistWhenSelected(StudentLectureComment? studentLectureComment)
    {
        if (studentLectureComment == null)
            throw new BusinessException(StudentLectureCommentsBusinessMessages.StudentLectureCommentNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentLectureCommentIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        StudentLectureComment? studentLectureComment = await _studentLectureCommentRepository.GetAsync(
            predicate: slc => slc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentLectureCommentShouldExistWhenSelected(studentLectureComment);
    }
}