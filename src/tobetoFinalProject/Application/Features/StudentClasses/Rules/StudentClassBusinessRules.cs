using Application.Features.StudentClasss.Constants;
using Application.Features.StudentClasses.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentClasses.Rules;

public class StudentClassBusinessRules : BaseBusinessRules
{
    private readonly IStudentClassRepository _studentClassRepository;

    public StudentClassBusinessRules(IStudentClassRepository studentClassRepository)
    {
        _studentClassRepository = studentClassRepository;
    }

    public Task StudentClassShouldExistWhenSelected(StudentClass? studentClass)
    {
        if (studentClass == null)
            throw new BusinessException(StudentClassesBusinessMessages.StudentClassNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentClassIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentClass? studentClass = await _studentClassRepository.GetAsync(
            predicate: sc => sc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentClassShouldExistWhenSelected(studentClass);
    }
    public async Task StudentClassShouldNotExistsWhenInsert(string name)
    {
        bool doesExists = await _studentClassRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(StudentClassesBusinessMessages.StudentClassNameExists);
    }
    public async Task StudentClassShouldNotExistsWhenUpdate(string name)
    {
        bool doesExists = await _studentClassRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(StudentClassesBusinessMessages.StudentClassNameExists);
    }
}