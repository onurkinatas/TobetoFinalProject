using Application.Features.StudentQuizResults.Constants;
using Application.Features.StudentQuizResults.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentQuizResults.Constants.StudentQuizResultsOperationClaims;

namespace Application.Features.StudentQuizResults.Queries.GetById;

public class GetByIdStudentQuizResultQuery : IRequest<GetByIdStudentQuizResultResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentQuizResultQueryHandler : IRequestHandler<GetByIdStudentQuizResultQuery, GetByIdStudentQuizResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentQuizResultRepository _studentQuizResultRepository;
        private readonly StudentQuizResultBusinessRules _studentQuizResultBusinessRules;

        public GetByIdStudentQuizResultQueryHandler(IMapper mapper, IStudentQuizResultRepository studentQuizResultRepository, StudentQuizResultBusinessRules studentQuizResultBusinessRules)
        {
            _mapper = mapper;
            _studentQuizResultRepository = studentQuizResultRepository;
            _studentQuizResultBusinessRules = studentQuizResultBusinessRules;
        }

        public async Task<GetByIdStudentQuizResultResponse> Handle(GetByIdStudentQuizResultQuery request, CancellationToken cancellationToken)
        {
            StudentQuizResult? studentQuizResult = await _studentQuizResultRepository.GetAsync(predicate: sqr => sqr.Id == request.Id, cancellationToken: cancellationToken);
            await _studentQuizResultBusinessRules.StudentQuizResultShouldExistWhenSelected(studentQuizResult);

            GetByIdStudentQuizResultResponse response = _mapper.Map<GetByIdStudentQuizResultResponse>(studentQuizResult);
            return response;
        }
    }
}