using Application.Features.StudentStages.Constants;
using Application.Features.StudentStages.Constants;
using Application.Features.StudentStages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentStages.Constants.StudentStagesOperationClaims;

namespace Application.Features.StudentStages.Commands.Delete;

public class DeleteStudentStageCommand : IRequest<DeletedStudentStageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentStagesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentStages";

    public class DeleteStudentStageCommandHandler : IRequestHandler<DeleteStudentStageCommand, DeletedStudentStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentStageRepository _studentStageRepository;
        private readonly StudentStageBusinessRules _studentStageBusinessRules;

        public DeleteStudentStageCommandHandler(IMapper mapper, IStudentStageRepository studentStageRepository,
                                         StudentStageBusinessRules studentStageBusinessRules)
        {
            _mapper = mapper;
            _studentStageRepository = studentStageRepository;
            _studentStageBusinessRules = studentStageBusinessRules;
        }

        public async Task<DeletedStudentStageResponse> Handle(DeleteStudentStageCommand request, CancellationToken cancellationToken)
        {
            StudentStage? studentStage = await _studentStageRepository.GetAsync(predicate: ss => ss.Id == request.Id, cancellationToken: cancellationToken);
            await _studentStageBusinessRules.StudentStageShouldExistWhenSelected(studentStage);

            await _studentStageRepository.DeleteAsync(studentStage!);

            DeletedStudentStageResponse response = _mapper.Map<DeletedStudentStageResponse>(studentStage);
            return response;
        }
    }
}