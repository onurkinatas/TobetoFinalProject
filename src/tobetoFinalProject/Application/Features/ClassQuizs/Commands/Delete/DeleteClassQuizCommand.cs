using Application.Features.ClassQuizs.Constants;
using Application.Features.ClassQuizs.Constants;
using Application.Features.ClassQuizs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ClassQuizs.Constants.ClassQuizsOperationClaims;
using Core.Application.Pipelines.Caching;

namespace Application.Features.ClassQuizs.Commands.Delete;

public class DeleteClassQuizCommand : IRequest<DeletedClassQuizResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest, ICacheRemoverRequest
{
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllClassDetails";
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassQuizsOperationClaims.Delete };

    public class DeleteClassQuizCommandHandler : IRequestHandler<DeleteClassQuizCommand, DeletedClassQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassQuizRepository _classQuizRepository;
        private readonly ClassQuizBusinessRules _classQuizBusinessRules;

        public DeleteClassQuizCommandHandler(IMapper mapper, IClassQuizRepository classQuizRepository,
                                         ClassQuizBusinessRules classQuizBusinessRules)
        {
            _mapper = mapper;
            _classQuizRepository = classQuizRepository;
            _classQuizBusinessRules = classQuizBusinessRules;
        }

        public async Task<DeletedClassQuizResponse> Handle(DeleteClassQuizCommand request, CancellationToken cancellationToken)
        {
            ClassQuiz? classQuiz = await _classQuizRepository.GetAsync(predicate: cq => cq.Id == request.Id, cancellationToken: cancellationToken);
            await _classQuizBusinessRules.ClassQuizShouldExistWhenSelected(classQuiz);

            await _classQuizRepository.DeleteAsync(classQuiz!);

            DeletedClassQuizResponse response = _mapper.Map<DeletedClassQuizResponse>(classQuiz);
            return response;
        }
    }
}