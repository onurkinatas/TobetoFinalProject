using Application.Features.Certificates.Constants;
using Application.Features.Certificates.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Certificates.Rules;

public class CertificateBusinessRules : BaseBusinessRules
{
    private readonly ICertificateRepository _certificateRepository;

    public CertificateBusinessRules(ICertificateRepository certificateRepository)
    {
        _certificateRepository = certificateRepository;
    }

   

    public async Task CertificateShouldNotExistsWhenInsert(string imageUrl)
    {
        bool doesExists = await _certificateRepository
            .AnyAsync(predicate: ca => ca.ImageUrl == imageUrl, enableTracking: false);
        if (doesExists)
            throw new BusinessException(CertificatesBusinessMessages.CertificateImgUrlExists);
    }
    public async Task CertificateShouldNotExistsWhenUpdate(string imageUrl)
    {
        bool doesExists = await _certificateRepository
            .AnyAsync(predicate: ca => ca.ImageUrl == imageUrl, enableTracking: false);
        if (doesExists)
            throw new BusinessException(CertificatesBusinessMessages.CertificateImgUrlExists);
    }
    public Task CertificateShouldExistWhenSelected(Certificate? certificate)
    {
        if (certificate == null)
            throw new BusinessException(CertificatesBusinessMessages.CertificateNotExists);
        return Task.CompletedTask;
    }
    public async Task CertificateIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        Certificate? certificate = await _certificateRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CertificateShouldExistWhenSelected(certificate);
    }
}