using Application.Features.GeneralQuizs.Constants;
using Application.Features.GeneralQuizs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.GeneralQuizs.Constants.GeneralQuizsOperationClaims;

namespace Application.Features.GeneralQuizs.Queries.GetById;

public class GetByIdGeneralQuizQuery : IRequest<GetByIdGeneralQuizResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdGeneralQuizQueryHandler : IRequestHandler<GetByIdGeneralQuizQuery, GetByIdGeneralQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGeneralQuizRepository _generalQuizRepository;
        private readonly GeneralQuizBusinessRules _generalQuizBusinessRules;

        public GetByIdGeneralQuizQueryHandler(IMapper mapper, IGeneralQuizRepository generalQuizRepository, GeneralQuizBusinessRules generalQuizBusinessRules)
        {
            _mapper = mapper;
            _generalQuizRepository = generalQuizRepository;
            _generalQuizBusinessRules = generalQuizBusinessRules;
        }

        public async Task<GetByIdGeneralQuizResponse> Handle(GetByIdGeneralQuizQuery request, CancellationToken cancellationToken)
        {
            GeneralQuiz? generalQuiz = await _generalQuizRepository.GetAsync(predicate: gq => gq.Id == request.Id, cancellationToken: cancellationToken);
            await _generalQuizBusinessRules.GeneralQuizShouldExistWhenSelected(generalQuiz);

            GetByIdGeneralQuizResponse response = _mapper.Map<GetByIdGeneralQuizResponse>(generalQuiz);
            return response;
        }
    }
}