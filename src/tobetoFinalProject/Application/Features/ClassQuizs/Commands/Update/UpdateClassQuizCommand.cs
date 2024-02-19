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

namespace Application.Features.ClassQuizs.Commands.Update;

public class UpdateClassQuizCommand : IRequest<UpdatedClassQuizResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest, ICacheRemoverRequest
{
    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetAllClassDetails";
    public int Id { get; set; }
    public Guid StudentClassId { get; set; }
    public int QuizId { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassQuizsOperationClaims.Update };

    public class UpdateClassQuizCommandHandler : IRequestHandler<UpdateClassQuizCommand, UpdatedClassQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassQuizRepository _classQuizRepository;
        private readonly ClassQuizBusinessRules _classQuizBusinessRules;

        public UpdateClassQuizCommandHandler(IMapper mapper, IClassQuizRepository classQuizRepository,
                                         ClassQuizBusinessRules classQuizBusinessRules)
        {
            _mapper = mapper;
            _classQuizRepository = classQuizRepository;
            _classQuizBusinessRules = classQuizBusinessRules;
        }

        public async Task<UpdatedClassQuizResponse> Handle(UpdateClassQuizCommand request, CancellationToken cancellationToken)
        {
            ClassQuiz? classQuiz = await _classQuizRepository.GetAsync(predicate: cq => cq.Id == request.Id, cancellationToken: cancellationToken);
            await _classQuizBusinessRules.ClassQuizShouldExistWhenSelected(classQuiz);
            classQuiz = _mapper.Map(request, classQuiz);

            await _classQuizRepository.UpdateAsync(classQuiz!);

            UpdatedClassQuizResponse response = _mapper.Map<UpdatedClassQuizResponse>(classQuiz);
            return response;
        }
    }
}