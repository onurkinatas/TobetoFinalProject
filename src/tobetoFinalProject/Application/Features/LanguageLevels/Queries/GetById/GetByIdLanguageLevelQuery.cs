using Application.Features.LanguageLevels.Constants;
using Application.Features.LanguageLevels.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LanguageLevels.Constants.LanguageLevelsOperationClaims;

namespace Application.Features.LanguageLevels.Queries.GetById;

public class GetByIdLanguageLevelQuery : IRequest<GetByIdLanguageLevelResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdLanguageLevelQueryHandler : IRequestHandler<GetByIdLanguageLevelQuery, GetByIdLanguageLevelResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILanguageLevelRepository _languageLevelRepository;
        private readonly LanguageLevelBusinessRules _languageLevelBusinessRules;

        public GetByIdLanguageLevelQueryHandler(IMapper mapper, ILanguageLevelRepository languageLevelRepository, LanguageLevelBusinessRules languageLevelBusinessRules)
        {
            _mapper = mapper;
            _languageLevelRepository = languageLevelRepository;
            _languageLevelBusinessRules = languageLevelBusinessRules;
        }

        public async Task<GetByIdLanguageLevelResponse> Handle(GetByIdLanguageLevelQuery request, CancellationToken cancellationToken)
        {
            LanguageLevel? languageLevel = await _languageLevelRepository.GetAsync(predicate: ll => ll.Id == request.Id, cancellationToken: cancellationToken);
            await _languageLevelBusinessRules.LanguageLevelShouldExistWhenSelected(languageLevel);

            GetByIdLanguageLevelResponse response = _mapper.Map<GetByIdLanguageLevelResponse>(languageLevel);
            return response;
        }
    }
}