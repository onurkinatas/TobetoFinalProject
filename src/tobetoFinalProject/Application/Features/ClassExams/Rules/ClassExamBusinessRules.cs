using Application.Features.ClassExams.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.ClassExams.Rules;

public class ClassExamBusinessRules : BaseBusinessRules
{
    private readonly IClassExamRepository _classExamRepository;

    public ClassExamBusinessRules(IClassExamRepository classExamRepository)
    {
        _classExamRepository = classExamRepository;
    }

    public Task ClassExamShouldExistWhenSelected(ClassExam? classExam)
    {
        if (classExam == null)
            throw new BusinessException(ClassExamsBusinessMessages.ClassExamNotExists);
        return Task.CompletedTask;
    }

    public async Task ClassExamIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ClassExam? classExam = await _classExamRepository.GetAsync(
            predicate: ce => ce.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ClassExamShouldExistWhenSelected(classExam);
    }
}