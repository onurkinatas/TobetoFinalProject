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

namespace Application.Features.StudentStages.Commands.Update;

public class UpdateStudentStageCommand : IRequest<UpdatedStudentStageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid StageId { get; set; }
    public Guid StudentId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentStagesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentStages";

    public class UpdateStudentStageCommandHandler : IRequestHandler<UpdateStudentStageCommand, UpdatedStudentStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentStageRepository _studentStageRepository;
        private readonly StudentStageBusinessRules _studentStageBusinessRules;

        public UpdateStudentStageCommandHandler(IMapper mapper, IStudentStageRepository studentStageRepository,
                                         StudentStageBusinessRules studentStageBusinessRules)
        {
            _mapper = mapper;
            _studentStageRepository = studentStageRepository;
            _studentStageBusinessRules = studentStageBusinessRules;
        }

        public async Task<UpdatedStudentStageResponse> Handle(UpdateStudentStageCommand request, CancellationToken cancellationToken)
        {
            StudentStage? studentStage = await _studentStageRepository.GetAsync(predicate: ss => ss.Id == request.Id, cancellationToken: cancellationToken);
            await _studentStageBusinessRules.StudentStageShouldExistWhenSelected(studentStage);
            studentStage = _mapper.Map(request, studentStage);

            await _studentStageRepository.UpdateAsync(studentStage!);

            UpdatedStudentStageResponse response = _mapper.Map<UpdatedStudentStageResponse>(studentStage);
            return response;
        }
    }
}