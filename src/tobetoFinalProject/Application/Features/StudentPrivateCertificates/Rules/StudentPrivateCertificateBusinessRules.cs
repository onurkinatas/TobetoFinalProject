using Application.Features.StudentPrivateCertificates.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.StudentPrivateCertificates.Rules;

public class StudentPrivateCertificateBusinessRules : BaseBusinessRules
{
    private readonly IStudentPrivateCertificateRepository _studentPrivateCertificateRepository;

    public StudentPrivateCertificateBusinessRules(IStudentPrivateCertificateRepository studentPrivateCertificateRepository)
    {
        _studentPrivateCertificateRepository = studentPrivateCertificateRepository;
    }

    public Task StudentPrivateCertificateShouldExistWhenSelected(StudentPrivateCertificate? studentPrivateCertificate)
    {
        if (studentPrivateCertificate == null)
            throw new BusinessException(StudentPrivateCertificatesBusinessMessages.StudentPrivateCertificateNotExists);
        return Task.CompletedTask;
    }

    public async Task StudentPrivateCertificateIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        StudentPrivateCertificate? studentPrivateCertificate = await _studentPrivateCertificateRepository.GetAsync(
            predicate: spc => spc.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await StudentPrivateCertificateShouldExistWhenSelected(studentPrivateCertificate);
    }
}