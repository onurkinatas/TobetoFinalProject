using Application.Features.ClassExams.Constants;
using Application.Features.ClassExams.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ClassExams.Constants.ClassExamsOperationClaims;

namespace Application.Features.ClassExams.Commands.Create;

public class CreateClassExamCommand : IRequest<CreatedClassExamResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid ExamId { get; set; }
    public Guid StudentClassId { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassExamsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetClassExams";

    public class CreateClassExamCommandHandler : IRequestHandler<CreateClassExamCommand, CreatedClassExamResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassExamRepository _classExamRepository;
        private readonly ClassExamBusinessRules _classExamBusinessRules;

        public CreateClassExamCommandHandler(IMapper mapper, IClassExamRepository classExamRepository,
                                         ClassExamBusinessRules classExamBusinessRules)
        {
            _mapper = mapper;
            _classExamRepository = classExamRepository;
            _classExamBusinessRules = classExamBusinessRules;
        }

        public async Task<CreatedClassExamResponse> Handle(CreateClassExamCommand request, CancellationToken cancellationToken)
        {
            ClassExam classExam = _mapper.Map<ClassExam>(request);

            await _classExamRepository.AddAsync(classExam);

            CreatedClassExamResponse response = _mapper.Map<CreatedClassExamResponse>(classExam);
            return response;
        }
    }
}