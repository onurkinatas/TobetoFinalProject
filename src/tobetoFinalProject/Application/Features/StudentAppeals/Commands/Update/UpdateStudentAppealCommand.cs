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

namespace Application.Features.StudentAppeals.Commands.Update;

public class UpdateStudentAppealCommand : IRequest<UpdatedStudentAppealResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid StudentId { get; set; }
    public Guid AppealId { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentAppealsOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentAppeals";

    public class UpdateStudentAppealCommandHandler : IRequestHandler<UpdateStudentAppealCommand, UpdatedStudentAppealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAppealRepository _studentAppealRepository;
        private readonly StudentAppealBusinessRules _studentAppealBusinessRules;

        public UpdateStudentAppealCommandHandler(IMapper mapper, IStudentAppealRepository studentAppealRepository,
                                         StudentAppealBusinessRules studentAppealBusinessRules)
        {
            _mapper = mapper;
            _studentAppealRepository = studentAppealRepository;
            _studentAppealBusinessRules = studentAppealBusinessRules;
        }

        public async Task<UpdatedStudentAppealResponse> Handle(UpdateStudentAppealCommand request, CancellationToken cancellationToken)
        {
            StudentAppeal? studentAppeal = await _studentAppealRepository.GetAsync(predicate: sa => sa.Id == request.Id, cancellationToken: cancellationToken);
            await _studentAppealBusinessRules.StudentAppealShouldExistWhenSelected(studentAppeal);
            studentAppeal = _mapper.Map(request, studentAppeal);

            await _studentAppealRepository.UpdateAsync(studentAppeal!);

            UpdatedStudentAppealResponse response = _mapper.Map<UpdatedStudentAppealResponse>(studentAppeal);
            return response;
        }
    }
}