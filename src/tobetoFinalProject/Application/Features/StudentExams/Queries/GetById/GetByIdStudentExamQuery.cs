using Application.Features.StudentExams.Constants;
using Application.Features.StudentExams.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentExams.Constants.StudentExamsOperationClaims;

namespace Application.Features.StudentExams.Queries.GetById;

public class GetByIdStudentExamQuery : IRequest<GetByIdStudentExamResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdStudentExamQueryHandler : IRequestHandler<GetByIdStudentExamQuery, GetByIdStudentExamResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly StudentExamBusinessRules _studentExamBusinessRules;

        public GetByIdStudentExamQueryHandler(IMapper mapper, IStudentExamRepository studentExamRepository, StudentExamBusinessRules studentExamBusinessRules)
        {
            _mapper = mapper;
            _studentExamRepository = studentExamRepository;
            _studentExamBusinessRules = studentExamBusinessRules;
        }

        public async Task<GetByIdStudentExamResponse> Handle(GetByIdStudentExamQuery request, CancellationToken cancellationToken)
        {
            StudentExam? studentExam = await _studentExamRepository.GetAsync(predicate: se => se.Id == request.Id, cancellationToken: cancellationToken);
            await _studentExamBusinessRules.StudentExamShouldExistWhenSelected(studentExam);

            GetByIdStudentExamResponse response = _mapper.Map<GetByIdStudentExamResponse>(studentExam);
            return response;
        }
    }
}