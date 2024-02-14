using Application.Features.Quizs.Rules;
using Application.Services.Repositories;
using Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Quizs;

public class QuizsManager : IQuizsService
{
    private readonly IQuizRepository _quizRepository;
    private readonly QuizBusinessRules _quizBusinessRules;

    public QuizsManager(IQuizRepository quizRepository, QuizBusinessRules quizBusinessRules)
    {
        _quizRepository = quizRepository;
        _quizBusinessRules = quizBusinessRules;
    }

    public async Task<Quiz?> GetAsync(
        Expression<Func<Quiz, bool>> predicate,
        Func<IQueryable<Quiz>, IIncludableQueryable<Quiz, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Quiz? quiz = await _quizRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return quiz;
    }

    public async Task<IPaginate<Quiz>?> GetListAsync(
        Expression<Func<Quiz, bool>>? predicate = null,
        Func<IQueryable<Quiz>, IOrderedQueryable<Quiz>>? orderBy = null,
        Func<IQueryable<Quiz>, IIncludableQueryable<Quiz, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Quiz> quizList = await _quizRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return quizList;
    }

    public async Task<Quiz> AddAsync(Quiz quiz)
    {
        Quiz addedQuiz = await _quizRepository.AddAsync(quiz);

        return addedQuiz;
    }

    public async Task<Quiz> UpdateAsync(Quiz quiz)
    {
        Quiz updatedQuiz = await _quizRepository.UpdateAsync(quiz);

        return updatedQuiz;
    }

    public async Task<Quiz> DeleteAsync(Quiz quiz, bool permanent = false)
    {
        Quiz deletedQuiz = await _quizRepository.DeleteAsync(quiz);

        return deletedQuiz;
    }
}
