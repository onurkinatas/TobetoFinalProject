using Application.Features.StudentEducations.Constants;
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
    public async Task StudentEducationShouldNotExistsWhenInsert(StudentEducation studentEducation)
    {
        bool doesExists = await _studentEducationRepository
            .AnyAsync(predicate: se =>
            se.StudentId == studentEducation.StudentId
            && se.Branch==studentEducation.Branch
            &&se.EducationStatus==studentEducation.EducationStatus
            &&se.SchoolName ==studentEducation.SchoolName
            , enableTracking: false);
        if (doesExists)
            throw new BusinessException(StudentEducationsBusinessMessages.StudentEducationAlreadyExists);
    }
    public async Task StudentEducationShouldNotExistsWhenUpdate(StudentEducation? studentEducation)
    {
        bool doesExists = await _studentEducationRepository
            .AnyAsync(predicate: se =>
            se.StudentId == studentEducation.StudentId
            && se.Branch == studentEducation.Branch
            && se.EducationStatus == studentEducation.EducationStatus
            && se.SchoolName == studentEducation.SchoolName
            , enableTracking: false);
        if (doesExists)
            throw new BusinessException(StudentEducationsBusinessMessages.StudentEducationAlreadyExists);
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