using Application.Features.StudentAppeals.Constants;
using Application.Features.StudentAppeals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.StudentAppeals.Constants.StudentAppealsOperationClaims;

namespace Application.Features.StudentAppeals.Commands.Create;

public class CreateStudentAppealCommand : IRequest<CreatedStudentAppealResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid StudentId { get; set; }
    public Guid AppealId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentAppealsOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentAppeals";

    public class CreateStudentAppealCommandHandler : IRequestHandler<CreateStudentAppealCommand, CreatedStudentAppealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAppealRepository _studentAppealRepository;
        private readonly StudentAppealBusinessRules _studentAppealBusinessRules;

        public CreateStudentAppealCommandHandler(IMapper mapper, IStudentAppealRepository studentAppealRepository,
                                         StudentAppealBusinessRules studentAppealBusinessRules)
        {
            _mapper = mapper;
            _studentAppealRepository = studentAppealRepository;
            _studentAppealBusinessRules = studentAppealBusinessRules;
        }

        public async Task<CreatedStudentAppealResponse> Handle(CreateStudentAppealCommand request, CancellationToken cancellationToken)
        {
            StudentAppeal studentAppeal = _mapper.Map<StudentAppeal>(request);

            await _studentAppealRepository.AddAsync(studentAppeal);

            CreatedStudentAppealResponse response = _mapper.Map<CreatedStudentAppealResponse>(studentAppeal);
            return response;
        }
    }
}