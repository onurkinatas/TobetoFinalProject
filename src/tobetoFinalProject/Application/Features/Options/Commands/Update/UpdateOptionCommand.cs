using Application.Features.Options.Constants;
using Application.Features.Options.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Options.Constants.OptionsOperationClaims;

namespace Application.Features.Options.Commands.Update;

public class UpdateOptionCommand : IRequest<UpdatedOptionResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public string Text { get; set; }

    public string[] Roles => new[] { Admin, Write, OptionsOperationClaims.Update };

    public class UpdateOptionCommandHandler : IRequestHandler<UpdateOptionCommand, UpdatedOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOptionRepository _optionRepository;
        private readonly OptionBusinessRules _optionBusinessRules;

        public UpdateOptionCommandHandler(IMapper mapper, IOptionRepository optionRepository,
                                         OptionBusinessRules optionBusinessRules)
        {
            _mapper = mapper;
            _optionRepository = optionRepository;
            _optionBusinessRules = optionBusinessRules;
        }

        public async Task<UpdatedOptionResponse> Handle(UpdateOptionCommand request, CancellationToken cancellationToken)
        {
            Option? option = await _optionRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            await _optionBusinessRules.OptionShouldExistWhenSelected(option);
            option = _mapper.Map(request, option);

            await _optionRepository.UpdateAsync(option!);

            UpdatedOptionResponse response = _mapper.Map<UpdatedOptionResponse>(option);
            return response;
        }
    }
}