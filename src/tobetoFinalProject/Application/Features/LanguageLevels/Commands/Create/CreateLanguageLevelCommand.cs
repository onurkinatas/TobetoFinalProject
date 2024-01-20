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
using Application.Features.Exams.Rules;

namespace Application.Features.LanguageLevels.Commands.Create;

public class CreateLanguageLevelCommand : IRequest<CreatedLanguageLevelResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid LanguageId { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, LanguageLevelsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLanguageLevels";

    public class CreateLanguageLevelCommandHandler : IRequestHandler<CreateLanguageLevelCommand, CreatedLanguageLevelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILanguageLevelRepository _languageLevelRepository;
        private readonly LanguageLevelBusinessRules _languageLevelBusinessRules;

        public CreateLanguageLevelCommandHandler(IMapper mapper, ILanguageLevelRepository languageLevelRepository,
                                         LanguageLevelBusinessRules languageLevelBusinessRules)
        {
            _mapper = mapper;
            _languageLevelRepository = languageLevelRepository;
            _languageLevelBusinessRules = languageLevelBusinessRules;
        }

        public async Task<CreatedLanguageLevelResponse> Handle(CreateLanguageLevelCommand request, CancellationToken cancellationToken)
        {
            LanguageLevel languageLevel = _mapper.Map<LanguageLevel>(request);

            await _languageLevelBusinessRules.LanguageLevelShouldNotExistsWhenInsert(languageLevel);

            await _languageLevelRepository.AddAsync(languageLevel);

            CreatedLanguageLevelResponse response = _mapper.Map<CreatedLanguageLevelResponse>(languageLevel);
            return response;
        }
    }
}