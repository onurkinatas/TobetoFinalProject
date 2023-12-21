using Application.Features.StudentStages.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentStages.Rules;

public class StudentStageBusinessRules : BaseBusinessRules
{
    private readonly IStudentStageRepository _studentStageRepository;

    public StudentStageBusinessRules(IStudentStageRepository studentStageRepository)
    {
        _studentStageRepository = studentStageRepository;
    }

    public Task StudentStageShouldExistWhenSelected(StudentStage? studentStage)
    {
        if (studentStage == null)
            throw new BusinessException(StudentStagesBusinessMessages.StudentStageNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentStageIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentStage? studentStage = await _studentStageRepository.GetAsync(
            predicate: ss => ss.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentStageShouldExistWhenSelected(studentStage);
    }
}