using Application.Features.SubTypes.Constants;
using Application.Features.SubTypes.Constants;
using Application.Features.SubTypes.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.SubTypes.Rules;

public class SubTypeBusinessRules : BaseBusinessRules
{
    private readonly ISubTypeRepository _subTypeRepository;

    public SubTypeBusinessRules(ISubTypeRepository subTypeRepository)
    {
        _subTypeRepository = subTypeRepository;
    }

    public Task SubTypeShouldExistWhenSelected(SubType? subType)
    {
        if (subType == null)
            throw new BusinessException(SubTypesBusinessMessages.SubTypeNotExists);
        return Task.CompletedTask;
    }

    public async Task SubTypeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        SubType? subType = await _subTypeRepository.GetAsync(
            predicate: st => st.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SubTypeShouldExistWhenSelected(subType);
    }

    public async Task SubTypeShouldNotExistsWhenInsert(string name)
    {
        bool doesExists = await _subTypeRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(SubTypesBusinessMessages.SubTypeNameExists);
    }
    public async Task SubTypeShouldNotExistsWhenUpdate(string name)
    {
        bool doesExists = await _subTypeRepository
            .AnyAsync(predicate: ca => ca.Name == name, enableTracking: false);
        if (doesExists)
            throw new BusinessException(SubTypesBusinessMessages.SubTypeNameExists);
    }
}