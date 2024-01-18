using Application.Features.Announcements.Constants;
using Application.Features.Districts.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Districts.Rules;

public class DistrictBusinessRules : BaseBusinessRules
{
    private readonly IDistrictRepository _districtRepository;

    public DistrictBusinessRules(IDistrictRepository districtRepository)
    {
        _districtRepository = districtRepository;
    }

    public Task DistrictShouldExistWhenSelected(District? district)
    {
        if (district == null)
            throw new BusinessException(DistrictsBusinessMessages.DistrictNotExists);
        return Task.CompletedTask;
    }

    public async Task DistrictIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        District? district = await _districtRepository.GetAsync(
            predicate: d => d.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await DistrictShouldExistWhenSelected(district);
    }

    public Task DistrictShouldNotExist(District? district)
    {
        if (district != null)
            throw new BusinessException(DistrictsBusinessMessages.DistrictNameExists);
        return Task.CompletedTask;
    }
    public async Task DistrictNameShouldNotExist(District district, CancellationToken cancellationToken)
    {
        District? controlDistrict = await _districtRepository.GetAsync(
            predicate: a => a.Name == district.Name,
            enableTracking: false, //Entity Framework'te "tracking" veya "izleme" (tracking) terimi, bir veri nesnesinin (entity) durumunu                          takip etme ve bu durumun veritabanýna nasýl yansýtýlacaðýný belirleme sürecini ifade eder.
            cancellationToken: cancellationToken //asenkron iþlemlerin iptal edilmesine olanak saðlar(Örnek çok uzun süren bir iþlemde)
            );
        await DistrictShouldNotExist(controlDistrict);
    }
}