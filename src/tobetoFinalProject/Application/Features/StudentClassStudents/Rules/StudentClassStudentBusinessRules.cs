using Application.Features.StudentClassStudents.Constants;
using Application.Features.StudentClassStudents.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentClassStudents.Rules;

public class StudentClassStudentBusinessRules : BaseBusinessRules
{
    private readonly IStudentClassStudentRepository _studentClassStudentRepository;

    public StudentClassStudentBusinessRules(IStudentClassStudentRepository studentClassStudentRepository)
    {
        _studentClassStudentRepository = studentClassStudentRepository;
    }
    public async Task StudentClassStudentShouldNotExistsWhenInsert(Guid studentClassId, Guid studentId)
    {
        bool doesExists = await _studentClassStudentRepository
            .AnyAsync(predicate: ca => ca.StudentClassId == studentClassId && ca.StudentId == studentId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(StudentClassStudentsBusinessMessages.StudentClassStudentAlreadyExists);
    }
    public async Task StudentClassStudentShouldNotExistsWhenUpdate(Guid studentClassId, Guid studentId)
    {
        bool doesExists = await _studentClassStudentRepository
            .AnyAsync(predicate: ca => ca.StudentClassId == studentClassId && ca.StudentId == studentId, enableTracking: false);
        if (doesExists)
            throw new BusinessException(StudentClassStudentsBusinessMessages.StudentClassStudentAlreadyExists);
    }
    public Task StudentClassStudentShouldExistWhenSelected(StudentClassStudent? studentClassStudent)
    {
        if (studentClassStudent == null)
            throw new BusinessException(StudentClassStudentsBusinessMessages.StudentClassStudentNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentClassStudentIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentClassStudent? studentClassStudent = await _studentClassStudentRepository.GetAsync(
            predicate: scs => scs.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentClassStudentShouldExistWhenSelected(studentClassStudent);
    }
}