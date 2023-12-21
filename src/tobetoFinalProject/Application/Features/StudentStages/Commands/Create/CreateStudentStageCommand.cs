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

namespace Application.Features.StudentStages.Commands.Create;

public class CreateStudentStageCommand : IRequest<CreatedStudentStageResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid StageId { get; set; }
    public Guid StudentId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentStagesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentStages";

    public class CreateStudentStageCommandHandler : IRequestHandler<CreateStudentStageCommand, CreatedStudentStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentStageRepository _studentStageRepository;
        private readonly StudentStageBusinessRules _studentStageBusinessRules;

        public CreateStudentStageCommandHandler(IMapper mapper, IStudentStageRepository studentStageRepository,
                                         StudentStageBusinessRules studentStageBusinessRules)
        {
            _mapper = mapper;
            _studentStageRepository = studentStageRepository;
            _studentStageBusinessRules = studentStageBusinessRules;
        }

        public async Task<CreatedStudentStageResponse> Handle(CreateStudentStageCommand request, CancellationToken cancellationToken)
        {
            StudentStage studentStage = _mapper.Map<StudentStage>(request);

            await _studentStageRepository.AddAsync(studentStage);

            CreatedStudentStageResponse response = _mapper.Map<CreatedStudentStageResponse>(studentStage);
            return response;
        }
    }
}