using Application.Features.StudentQuizResults.Rules;
using Application.Services.Questions;
using Application.Services.Quizs;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.StudentQuizResults;

public class StudentQuizResultsManager : IStudentQuizResultsService
{
    private readonly IStudentQuizResultRepository _studentQuizResultRepository;
    private readonly StudentQuizResultBusinessRules _studentQuizResultBusinessRules;
    private readonly IQuizsService _quizsService;
    private readonly IQuestionsService _questionsService;

    public StudentQuizResultsManager(IStudentQuizResultRepository studentQuizResultRepository, StudentQuizResultBusinessRules studentQuizResultBusinessRules, IQuizsService quizsService, IQuestionsService questionsService)
    {
        _studentQuizResultRepository = studentQuizResultRepository;
        _studentQuizResultBusinessRules = studentQuizResultBusinessRules;
        _quizsService = quizsService;
        _questionsService = questionsService;
    }

    public async Task<StudentQuizResult?> GetAsync(
        Expression<Func<StudentQuizResult, bool>> predicate,
        Func<IQueryable<StudentQuizResult>, IIncludableQueryable<StudentQuizResult, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        StudentQuizResult? studentQuizResult = await _studentQuizResultRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return studentQuizResult;
    }

    public async Task<IPaginate<StudentQuizResult>?> GetListAsync(
        Expression<Func<StudentQuizResult, bool>>? predicate = null,
        Func<IQueryable<StudentQuizResult>, IOrderedQueryable<StudentQuizResult>>? orderBy = null,
        Func<IQueryable<StudentQuizResult>, IIncludableQueryable<StudentQuizResult, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<StudentQuizResult> studentQuizResultList = await _studentQuizResultRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return studentQuizResultList;
    }

    public async Task<StudentQuizResult> AddAsync(StudentQuizResult studentQuizResult)
    {
        StudentQuizResult addedStudentQuizResult = await _studentQuizResultRepository.AddAsync(studentQuizResult);

        return addedStudentQuizResult;
    }

    public async Task<StudentQuizResult> UpdateAsync(StudentQuizResult studentQuizResult)
    {
        StudentQuizResult updatedStudentQuizResult = await _studentQuizResultRepository.UpdateAsync(studentQuizResult);

        return updatedStudentQuizResult;
    }

    public async Task<StudentQuizResult> DeleteAsync(StudentQuizResult studentQuizResult, bool permanent = false)
    {
        StudentQuizResult deletedStudentQuizResult = await _studentQuizResultRepository.DeleteAsync(studentQuizResult);

        return deletedStudentQuizResult;
    }
    public async Task<Task> UpdateQuizResultAsync(int quizId,Guid? studentId,int? optionId,int questionId)
    {
        StudentQuizResult? studentQuizResult = await _studentQuizResultRepository.GetAsync(predicate:sq=>sq.QuizId==quizId&&sq.StudentId==studentId);
        
        bool controlCorrect= await _questionsService.ControlCorrectOption(questionId, optionId);

        if(controlCorrect)
        {
            studentQuizResult.CorrectAnswerCount = studentQuizResult.CorrectAnswerCount == null ? 1:studentQuizResult.CorrectAnswerCount += 1;
            await _studentQuizResultRepository.UpdateAsync(studentQuizResult);
        }else if (!controlCorrect)
        {
            studentQuizResult.WrongAnswerCount = studentQuizResult.WrongAnswerCount == null ? 1 : studentQuizResult.WrongAnswerCount += 1;
            await _studentQuizResultRepository.UpdateAsync(studentQuizResult);
        }

        return Task.CompletedTask;
    }

    public int QuizPointCalculator(int? correctAnswerCount,int allQuestionCount)
    {
        int correct = (int)(correctAnswerCount == null ? 0 : correctAnswerCount);
        return (100 * correct / allQuestionCount);
    }
    public int QuizEmptyAnswerCalculator(int? correctAnswerCount, int? wrongAnswerCount, int allQuestionCount)
    {
        int wrong = (int)(wrongAnswerCount == null ? 0 : wrongAnswerCount);
        int correct = (int)(correctAnswerCount == null ? 0 : correctAnswerCount);

        return allQuestionCount - (wrong + correct);
    }
}
