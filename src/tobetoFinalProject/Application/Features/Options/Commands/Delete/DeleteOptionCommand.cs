using Application.Features.Options.Constants;
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

namespace Application.Features.Options.Commands.Delete;

public class DeleteOptionCommand : IRequest<DeletedOptionResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, OptionsOperationClaims.Delete };

    public class DeleteOptionCommandHandler : IRequestHandler<DeleteOptionCommand, DeletedOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IOptionRepository _optionRepository;
        private readonly OptionBusinessRules _optionBusinessRules;

        public DeleteOptionCommandHandler(IMapper mapper, IOptionRepository optionRepository,
                                         OptionBusinessRules optionBusinessRules)
        {
            _mapper = mapper;
            _optionRepository = optionRepository;
            _optionBusinessRules = optionBusinessRules;
        }

        public async Task<DeletedOptionResponse> Handle(DeleteOptionCommand request, CancellationToken cancellationToken)
        {
            Option? option = await _optionRepository.GetAsync(predicate: o => o.Id == request.Id, cancellationToken: cancellationToken);
            await _optionBusinessRules.OptionShouldExistWhenSelected(option);

            await _optionRepository.DeleteAsync(option!);

            DeletedOptionResponse response = _mapper.Map<DeletedOptionResponse>(option);
            return response;
        }
    }
}