using Application.Features.StudentEducations.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentEducations.Rules;

public class StudentEducationBusinessRules : BaseBusinessRules
{
    private readonly IStudentEducationRepository _studentEducationRepository;

    public StudentEducationBusinessRules(IStudentEducationRepository studentEducationRepository)
    {
        _studentEducationRepository = studentEducationRepository;
    }

    public Task StudentEducationShouldExistWhenSelected(StudentEducation? studentEducation)
    {
        if (studentEducation == null)
            throw new BusinessException(StudentEducationsBusinessMessages.StudentEducationNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentEducationIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentEducation? studentEducation = await _studentEducationRepository.GetAsync(
            predicate: se => se.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentEducationShouldExistWhenSelected(studentEducation);
    }
}