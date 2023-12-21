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

namespace Application.Features.ClassExams.Commands.Update;

public class UpdateClassExamCommand : IRequest<UpdatedClassExamResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid ExamId { get; set; }
    public Guid StudentClassId { get; set; }

    public string[] Roles => new[] { Admin, Write, ClassExamsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetClassExams";

    public class UpdateClassExamCommandHandler : IRequestHandler<UpdateClassExamCommand, UpdatedClassExamResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassExamRepository _classExamRepository;
        private readonly ClassExamBusinessRules _classExamBusinessRules;

        public UpdateClassExamCommandHandler(IMapper mapper, IClassExamRepository classExamRepository,
                                         ClassExamBusinessRules classExamBusinessRules)
        {
            _mapper = mapper;
            _classExamRepository = classExamRepository;
            _classExamBusinessRules = classExamBusinessRules;
        }

        public async Task<UpdatedClassExamResponse> Handle(UpdateClassExamCommand request, CancellationToken cancellationToken)
        {
            ClassExam? classExam = await _classExamRepository.GetAsync(predicate: ce => ce.Id == request.Id, cancellationToken: cancellationToken);
            await _classExamBusinessRules.ClassExamShouldExistWhenSelected(classExam);
            classExam = _mapper.Map(request, classExam);

            await _classExamRepository.UpdateAsync(classExam!);

            UpdatedClassExamResponse response = _mapper.Map<UpdatedClassExamResponse>(classExam);
            return response;
        }
    }
}