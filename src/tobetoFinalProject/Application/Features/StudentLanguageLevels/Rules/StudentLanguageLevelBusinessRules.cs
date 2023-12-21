using Application.Features.StudentLanguageLevels.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentLanguageLevels.Rules;

public class StudentLanguageLevelBusinessRules : BaseBusinessRules
{
    private readonly IStudentLanguageLevelRepository _studentLanguageLevelRepository;

    public StudentLanguageLevelBusinessRules(IStudentLanguageLevelRepository studentLanguageLevelRepository)
    {
        _studentLanguageLevelRepository = studentLanguageLevelRepository;
    }

    public Task StudentLanguageLevelShouldExistWhenSelected(StudentLanguageLevel? studentLanguageLevel)
    {
        if (studentLanguageLevel == null)
            throw new BusinessException(StudentLanguageLevelsBusinessMessages.StudentLanguageLevelNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentLanguageLevelIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentLanguageLevel? studentLanguageLevel = await _studentLanguageLevelRepository.GetAsync(
            predicate: sll => sll.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentLanguageLevelShouldExistWhenSelected(studentLanguageLevel);
    }
}