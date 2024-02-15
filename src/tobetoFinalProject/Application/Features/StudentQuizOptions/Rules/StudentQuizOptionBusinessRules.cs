using Application.Features.StudentQuizOptions.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentQuizOptions.Rules;

public class StudentQuizOptionBusinessRules : BaseBusinessRules
{
    private readonly IStudentQuizOptionRepository _studentQuizOptionRepository;

    public StudentQuizOptionBusinessRules(IStudentQuizOptionRepository studentQuizOptionRepository)
    {
        _studentQuizOptionRepository = studentQuizOptionRepository;
    }

    public Task StudentQuizOptionShouldExistWhenSelected(StudentQuizOption? studentQuizOption)
    {
        if (studentQuizOption == null)
            throw new BusinessException(StudentQuizOptionsBusinessMessages.StudentQuizOptionNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentQuizOptionIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        StudentQuizOption? studentQuizOption = await _studentQuizOptionRepository.GetAsync(
            predicate: sqo => sqo.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentQuizOptionShouldExistWhenSelected(studentQuizOption);
    }
}