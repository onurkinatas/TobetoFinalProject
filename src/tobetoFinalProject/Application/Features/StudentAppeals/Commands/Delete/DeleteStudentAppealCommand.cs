using Application.Features.StudentAppeals.Constants;
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

namespace Application.Features.StudentAppeals.Commands.Delete;

public class DeleteStudentAppealCommand : IRequest<DeletedStudentAppealResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, StudentAppealsOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetStudentAppeals";

    public class DeleteStudentAppealCommandHandler : IRequestHandler<DeleteStudentAppealCommand, DeletedStudentAppealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAppealRepository _studentAppealRepository;
        private readonly StudentAppealBusinessRules _studentAppealBusinessRules;

        public DeleteStudentAppealCommandHandler(IMapper mapper, IStudentAppealRepository studentAppealRepository,
                                         StudentAppealBusinessRules studentAppealBusinessRules)
        {
            _mapper = mapper;
            _studentAppealRepository = studentAppealRepository;
            _studentAppealBusinessRules = studentAppealBusinessRules;
        }

        public async Task<DeletedStudentAppealResponse> Handle(DeleteStudentAppealCommand request, CancellationToken cancellationToken)
        {
            StudentAppeal? studentAppeal = await _studentAppealRepository.GetAsync(predicate: sa => sa.Id == request.Id, cancellationToken: cancellationToken);
            await _studentAppealBusinessRules.StudentAppealShouldExistWhenSelected(studentAppeal);

            await _studentAppealRepository.DeleteAsync(studentAppeal!);

            DeletedStudentAppealResponse response = _mapper.Map<DeletedStudentAppealResponse>(studentAppeal);
            return response;
        }
    }
}