using Application.Features.StudentStages.Constants;
using Application.Features.StudentStages.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentStages.Constants.StudentStagesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudentStages.Queries.GetById;

public class GetByIdStudentStageQuery : IRequest<GetByIdStudentStageResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public class GetByIdStudentStageQueryHandler : IRequestHandler<GetByIdStudentStageQuery, GetByIdStudentStageResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentStageRepository _studentStageRepository;
        private readonly StudentStageBusinessRules _studentStageBusinessRules;

        public GetByIdStudentStageQueryHandler(IMapper mapper, IStudentStageRepository studentStageRepository, StudentStageBusinessRules studentStageBusinessRules)
        {
            _mapper = mapper;
            _studentStageRepository = studentStageRepository;
            _studentStageBusinessRules = studentStageBusinessRules;
        }

        public async Task<GetByIdStudentStageResponse> Handle(GetByIdStudentStageQuery request, CancellationToken cancellationToken)
        {
            StudentStage? studentStage = await _studentStageRepository.GetAsync(
                predicate: ss => ss.Id == request.Id,
                include: ss => ss.Include(ss => ss.Stage),
                cancellationToken: cancellationToken);
            await _studentStageBusinessRules.StudentStageShouldExistWhenSelected(studentStage);

            GetByIdStudentStageResponse response = _mapper.Map<GetByIdStudentStageResponse>(studentStage);
            return response;
        }
    }
}