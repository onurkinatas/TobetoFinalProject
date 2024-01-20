using Application.Features.Citys.Constants;
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

    public async Task CityShouldNotExistsWhenInsert(string name)
    {
        bool doesExists = await _cityRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(CitiesBusinessMessages.CityNameExists);
    }
    public async Task CityShouldNotExistsWhenUpdate(string name)
    {
        bool doesExists = await _cityRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(CitiesBusinessMessages.CityNameExists);
    }

    public Task CityShouldExistWhenSelected(City? city)
    {
        if (city == null)
            throw new BusinessException(CitiesBusinessMessages.CityNotExists);
        return Task.CompletedTask;
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