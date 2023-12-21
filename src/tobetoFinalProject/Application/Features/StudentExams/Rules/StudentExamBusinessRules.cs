using Application.Features.StudentExams.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentExams.Rules;

public class StudentExamBusinessRules : BaseBusinessRules
{
    private readonly IStudentExamRepository _studentExamRepository;

    public StudentExamBusinessRules(IStudentExamRepository studentExamRepository)
    {
        _studentExamRepository = studentExamRepository;
    }

    public Task StudentExamShouldExistWhenSelected(StudentExam? studentExam)
    {
        if (studentExam == null)
            throw new BusinessException(StudentExamsBusinessMessages.StudentExamNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentExamIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentExam? studentExam = await _studentExamRepository.GetAsync(
            predicate: se => se.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentExamShouldExistWhenSelected(studentExam);
    }
}