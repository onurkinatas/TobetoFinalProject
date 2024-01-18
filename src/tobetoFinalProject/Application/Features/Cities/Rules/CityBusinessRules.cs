using Application.Features.Cities.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Cities.Rules;

public class CityBusinessRules : BaseBusinessRules
{
    private readonly ICityRepository _cityRepository;

    public CityBusinessRules(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public Task CityShouldExistWhenSelected(City? city)
    {
        if (city == null)
            throw new BusinessException(CitiesBusinessMessages.CityNotExists);
        return Task.CompletedTask;
    }

    public Task CityNameShouldNotExist(City? city)
    {
        if (city == null)
            throw new BusinessException(CitiesBusinessMessages.CityNameExists);
        return Task.CompletedTask;
    }

    public async Task CityNameShouldNotExist(City city, CancellationToken cancellationToken)
    {
        City? cityControl = await _cityRepository.GetAsync(
            predicate: c => c.Name == city.Name,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CityNameShouldNotExist(city);
    }

    public async Task CityIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        City? city = await _cityRepository.GetAsync(
            predicate: c => c.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await CityShouldExistWhenSelected(city);
    }
}