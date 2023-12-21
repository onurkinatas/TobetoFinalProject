using Application.Features.StudentExperiences.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentExperiences.Rules;

public class StudentExperienceBusinessRules : BaseBusinessRules
{
    private readonly IStudentExperienceRepository _studentExperienceRepository;

    public StudentExperienceBusinessRules(IStudentExperienceRepository studentExperienceRepository)
    {
        _studentExperienceRepository = studentExperienceRepository;
    }

    public Task StudentExperienceShouldExistWhenSelected(StudentExperience? studentExperience)
    {
        if (studentExperience == null)
            throw new BusinessException(StudentExperiencesBusinessMessages.StudentExperienceNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentExperienceIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentExperience? studentExperience = await _studentExperienceRepository.GetAsync(
            predicate: se => se.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentExperienceShouldExistWhenSelected(studentExperience);
    }
}