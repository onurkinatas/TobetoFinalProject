using Application.Features.StudentQuizOptions.Constants;
using Application.Features.StudentQuizOptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentQuizOptions.Constants.StudentQuizOptionsOperationClaims;

namespace Application.Features.StudentQuizOptions.Queries.GetById;

public class GetByIdStudentQuizOptionQuery : IRequest<GetByIdStudentQuizOptionResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentQuizOptionQueryHandler : IRequestHandler<GetByIdStudentQuizOptionQuery, GetByIdStudentQuizOptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentQuizOptionRepository _studentQuizOptionRepository;
        private readonly StudentQuizOptionBusinessRules _studentQuizOptionBusinessRules;

        public GetByIdStudentQuizOptionQueryHandler(IMapper mapper, IStudentQuizOptionRepository studentQuizOptionRepository, StudentQuizOptionBusinessRules studentQuizOptionBusinessRules)
        {
            _mapper = mapper;
            _studentQuizOptionRepository = studentQuizOptionRepository;
            _studentQuizOptionBusinessRules = studentQuizOptionBusinessRules;
        }

        public async Task<GetByIdStudentQuizOptionResponse> Handle(GetByIdStudentQuizOptionQuery request, CancellationToken cancellationToken)
        {
            StudentQuizOption? studentQuizOption = await _studentQuizOptionRepository.GetAsync(predicate: sqo => sqo.Id == request.Id, cancellationToken: cancellationToken);
            await _studentQuizOptionBusinessRules.StudentQuizOptionShouldExistWhenSelected(studentQuizOption);

            GetByIdStudentQuizOptionResponse response = _mapper.Map<GetByIdStudentQuizOptionResponse>(studentQuizOption);
            return response;
        }
    }
}