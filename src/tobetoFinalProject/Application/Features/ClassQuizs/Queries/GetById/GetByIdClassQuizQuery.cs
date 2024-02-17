using Application.Features.ClassQuizs.Constants;
using Application.Features.ClassQuizs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ClassQuizs.Constants.ClassQuizsOperationClaims;

namespace Application.Features.ClassQuizs.Queries.GetById;

public class GetByIdClassQuizQuery : IRequest<GetByIdClassQuizResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdClassQuizQueryHandler : IRequestHandler<GetByIdClassQuizQuery, GetByIdClassQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassQuizRepository _classQuizRepository;
        private readonly ClassQuizBusinessRules _classQuizBusinessRules;

        public GetByIdClassQuizQueryHandler(IMapper mapper, IClassQuizRepository classQuizRepository, ClassQuizBusinessRules classQuizBusinessRules)
        {
            _mapper = mapper;
            _classQuizRepository = classQuizRepository;
            _classQuizBusinessRules = classQuizBusinessRules;
        }

        public async Task<GetByIdClassQuizResponse> Handle(GetByIdClassQuizQuery request, CancellationToken cancellationToken)
        {
            ClassQuiz? classQuiz = await _classQuizRepository.GetAsync(predicate: cq => cq.Id == request.Id, cancellationToken: cancellationToken);
            await _classQuizBusinessRules.ClassQuizShouldExistWhenSelected(classQuiz);

            GetByIdClassQuizResponse response = _mapper.Map<GetByIdClassQuizResponse>(classQuiz);
            return response;
        }
    }
}