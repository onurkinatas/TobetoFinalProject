using Application.Features.StudentQuizResults.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentQuizResults.Rules;

public class StudentQuizResultBusinessRules : BaseBusinessRules
{
    private readonly IStudentQuizResultRepository _studentQuizResultRepository;

    public StudentQuizResultBusinessRules(IStudentQuizResultRepository studentQuizResultRepository)
    {
        _studentQuizResultRepository = studentQuizResultRepository;
    }

    public Task StudentQuizResultShouldExistWhenSelected(StudentQuizResult? studentQuizResult)
    {
        if (studentQuizResult == null)
            throw new BusinessException(StudentQuizResultsBusinessMessages.StudentQuizResultNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentQuizResultIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        StudentQuizResult? studentQuizResult = await _studentQuizResultRepository.GetAsync(
            predicate: sqr => sqr.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentQuizResultShouldExistWhenSelected(studentQuizResult);
    }

    public async Task<Task> StudentJoinControl(int quizId, Guid studentId) {
        bool doesExist = await _studentQuizResultRepository.AnyAsync(predicate: sqr => sqr.QuizId == quizId && sqr.StudentId == studentId);
        if (doesExist)
            throw new BusinessException("Bu Sýnava Zaten Katýldýnýz Bir Daha Giremezsiniz");
        return Task.CompletedTask;

        
    }
}