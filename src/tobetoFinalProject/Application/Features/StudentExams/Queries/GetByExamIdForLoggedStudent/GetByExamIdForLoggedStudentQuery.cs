using Application.Features.StudentExams.Queries.GetById;
using Application.Features.StudentExams.Rules;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentExams.Queries.GetByExamIdForLoggedStudent;
public class GetByExamIdForLoggedStudentQuery : IRequest<GetByExamIdForLoggedStudentQueryResponse>, ISecuredRequest
{
    public Guid ExamId { get; set; }

    public string[] Roles => new[] { "Student" };

    public class GetByExamIdForLoggedStudentQueryHandler : IRequestHandler<GetByExamIdForLoggedStudentQuery, GetByExamIdForLoggedStudentQueryResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentExamRepository _studentExamRepository;
        private readonly StudentExamBusinessRules _studentExamBusinessRules;
        private readonly IContextOperationService _contextOperationService;

        public GetByExamIdForLoggedStudentQueryHandler(IMapper mapper, IStudentExamRepository studentExamRepository, StudentExamBusinessRules studentExamBusinessRules, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _studentExamRepository = studentExamRepository;
            _studentExamBusinessRules = studentExamBusinessRules;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetByExamIdForLoggedStudentQueryResponse> Handle(GetByExamIdForLoggedStudentQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            StudentExam? studentExam = await _studentExamRepository.GetAsync(
                predicate: se => se.ExamId == request.ExamId&&se.StudentId==getStudent.Id,
                cancellationToken: cancellationToken);


            GetByExamIdForLoggedStudentQueryResponse response = new();
            if (studentExam is not null)
                response.IsJoined = true;
            else if (studentExam is null)
                response.IsJoined = false;
            else
                response.IsJoined = false;

            return response;
        }
    }
}