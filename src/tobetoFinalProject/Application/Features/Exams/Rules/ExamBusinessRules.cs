using Application.Features.Districts.Constants;
using Application.Features.Exams.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Exams.Rules;

public class ExamBusinessRules : BaseBusinessRules
{
    private readonly IExamRepository _examRepository;

    public ExamBusinessRules(IExamRepository examRepository)
    {
        _examRepository = examRepository;
    }

    public Task ExamShouldExistWhenSelected(Exam? exam)
    {
        if (exam == null)
            throw new BusinessException(ExamsBusinessMessages.ExamNotExists);
        return Task.CompletedTask;
    }

    public async Task ExamIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Exam? exam = await _examRepository.GetAsync(
            predicate: e => e.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ExamShouldExistWhenSelected(exam);
    }

    public Task ExamShouldNotExist(Exam? exam)
    {
        if (exam != null)
            throw new BusinessException(ExamsBusinessMessages.ExamNameExists);
        return Task.CompletedTask;
    }
    public async Task DistrictNameShouldNotExist(Exam exam, CancellationToken cancellationToken)
    {
        Exam? controlExam = await _examRepository.GetAsync(
            predicate: a => a.Name == exam.Name,
            enableTracking: false, //Entity Framework'te "tracking" veya "izleme" (tracking) terimi, bir veri nesnesinin (entity) durumunu                          takip etme ve bu durumun veritabanýna nasýl yansýtýlacaðýný belirleme sürecini ifade eder.
            cancellationToken: cancellationToken //asenkron iþlemlerin iptal edilmesine olanak saðlar(Örnek çok uzun süren bir iþlemde)
            );
        await ExamShouldNotExist(controlExam);
    }
}