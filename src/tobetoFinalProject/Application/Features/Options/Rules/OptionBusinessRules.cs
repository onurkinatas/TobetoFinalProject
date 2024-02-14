using Application.Features.Options.Constants;
using Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Domain.Entities;

namespace Application.Features.Options.Rules;

public class OptionBusinessRules : BaseBusinessRules
{
    private readonly IOptionRepository _optionRepository;

    public OptionBusinessRules(IOptionRepository optionRepository)
    {
        _optionRepository = optionRepository;
    }

    public Task OptionShouldExistWhenSelected(Option? option)
    {
        if (option == null)
            throw new BusinessException(OptionsBusinessMessages.OptionNotExists);
        return Task.CompletedTask;
    }

    public async Task OptionIdShouldExistWhenSelected(int id, CancellationToken cancellationToken)
    {
        Option? option = await _optionRepository.GetAsync(
            predicate: o => o.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await OptionShouldExistWhenSelected(option);
    }

}