using Application.Features.ClassExams.Constants;
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

namespace Application.Features.ClassExams.Commands.Delete;

public class DeleteClassExamCommand : IRequest<DeletedClassExamResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassExamsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetClassExams";

    public class DeleteClassExamCommandHandler : IRequestHandler<DeleteClassExamCommand, DeletedClassExamResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassExamRepository _classExamRepository;
        private readonly ClassExamBusinessRules _classExamBusinessRules;

        public DeleteClassExamCommandHandler(IMapper mapper, IClassExamRepository classExamRepository,
                                         ClassExamBusinessRules classExamBusinessRules)
        {
            _mapper = mapper;
            _classExamRepository = classExamRepository;
            _classExamBusinessRules = classExamBusinessRules;
        }

        public async Task<DeletedClassExamResponse> Handle(DeleteClassExamCommand request, CancellationToken cancellationToken)
        {
            ClassExam? classExam = await _classExamRepository.GetAsync(predicate: ce => ce.Id == request.Id, cancellationToken: cancellationToken);
            await _classExamBusinessRules.ClassExamShouldExistWhenSelected(classExam);

            await _classExamRepository.DeleteAsync(classExam!);

            DeletedClassExamResponse response = _mapper.Map<DeletedClassExamResponse>(classExam);
            return response;
        }
    }
}