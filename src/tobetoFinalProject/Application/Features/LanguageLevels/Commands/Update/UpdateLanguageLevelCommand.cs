using Application.Features.LanguageLevels.Constants;
using Application.Features.LanguageLevels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.LanguageLevels.Constants.LanguageLevelsOperationClaims;

namespace Application.Features.LanguageLevels.Commands.Update;

public class UpdateLanguageLevelCommand : IRequest<UpdatedLanguageLevelResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid LanguageId { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, LanguageLevelsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLanguageLevels";

    public class UpdateLanguageLevelCommandHandler : IRequestHandler<UpdateLanguageLevelCommand, UpdatedLanguageLevelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILanguageLevelRepository _languageLevelRepository;
        private readonly LanguageLevelBusinessRules _languageLevelBusinessRules;

        public UpdateLanguageLevelCommandHandler(IMapper mapper, ILanguageLevelRepository languageLevelRepository,
                                         LanguageLevelBusinessRules languageLevelBusinessRules)
        {
            _mapper = mapper;
            _languageLevelRepository = languageLevelRepository;
            _languageLevelBusinessRules = languageLevelBusinessRules;
        }

        public async Task<UpdatedLanguageLevelResponse> Handle(UpdateLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            LanguageLevel? languageLevel = await _languageLevelRepository.GetAsync(predicate: ll => ll.Id == request.Id, cancellationToken: cancellationToken);
            await _languageLevelBusinessRules.LanguageLevelShouldExistWhenSelected(languageLevel);
            languageLevel = _mapper.Map(request, languageLevel);

            await _languageLevelRepository.UpdateAsync(languageLevel!);

            UpdatedLanguageLevelResponse response = _mapper.Map<UpdatedLanguageLevelResponse>(languageLevel);
            return response;
        }
    }
}