using Application.Features.StudentAppeals.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentAppeals.Rules;

public class StudentAppealBusinessRules : BaseBusinessRules
{
    private readonly IStudentAppealRepository _studentAppealRepository;

    public StudentAppealBusinessRules(IStudentAppealRepository studentAppealRepository)
    {
        _studentAppealRepository = studentAppealRepository;
    }

    public Task StudentAppealShouldExistWhenSelected(StudentAppeal? studentAppeal)
    {
        if (studentAppeal == null)
            throw new BusinessException(StudentAppealsBusinessMessages.StudentAppealNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentAppealIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentAppeal? studentAppeal = await _studentAppealRepository.GetAsync(
            predicate: sa => sa.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentAppealShouldExistWhenSelected(studentAppeal);
    }
}