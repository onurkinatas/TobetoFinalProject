using Application.Features.StudentCertificates.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentCertificates.Rules;

public class StudentCertificateBusinessRules : BaseBusinessRules
{
    private readonly IStudentCertificateRepository _studentCertificateRepository;

    public StudentCertificateBusinessRules(IStudentCertificateRepository studentCertificateRepository)
    {
        _studentCertificateRepository = studentCertificateRepository;
    }

    public Task StudentCertificateShouldExistWhenSelected(StudentCertificate? studentCertificate)
    {
        if (studentCertificate == null)
            throw new BusinessException(StudentCertificatesBusinessMessages.StudentCertificateNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentCertificateIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentCertificate? studentCertificate = await _studentCertificateRepository.GetAsync(
            predicate: sc => sc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentCertificateShouldExistWhenSelected(studentCertificate);
    }
}