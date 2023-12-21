using Application.Features.StudentAppeals.Constants;
using Application.Features.StudentAppeals.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentAppeals.Constants.StudentAppealsOperationClaims;

namespace Application.Features.StudentAppeals.Queries.GetById;

public class GetByIdStudentAppealQuery : IRequest<GetByIdStudentAppealResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentAppealQueryHandler : IRequestHandler<GetByIdStudentAppealQuery, GetByIdStudentAppealResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentAppealRepository _studentAppealRepository;
        private readonly StudentAppealBusinessRules _studentAppealBusinessRules;

        public GetByIdStudentAppealQueryHandler(IMapper mapper, IStudentAppealRepository studentAppealRepository, StudentAppealBusinessRules studentAppealBusinessRules)
        {
            _mapper = mapper;
            _studentAppealRepository = studentAppealRepository;
            _studentAppealBusinessRules = studentAppealBusinessRules;
        }

        public async Task<GetByIdStudentAppealResponse> Handle(GetByIdStudentAppealQuery request, CancellationToken cancellationToken)
        {
            StudentAppeal? studentAppeal = await _studentAppealRepository.GetAsync(predicate: sa => sa.Id == request.Id, cancellationToken: cancellationToken);
            await _studentAppealBusinessRules.StudentAppealShouldExistWhenSelected(studentAppeal);

            GetByIdStudentAppealResponse response = _mapper.Map<GetByIdStudentAppealResponse>(studentAppeal);
            return response;
        }
    }
}