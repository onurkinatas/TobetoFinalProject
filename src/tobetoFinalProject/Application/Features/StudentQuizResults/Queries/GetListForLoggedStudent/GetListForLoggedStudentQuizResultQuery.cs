using Application.Features.StudentQuizResults.Queries.GetList;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentQuizResults.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentQuizResultQuery : IRequest<GetListResponse<GetListStudentQuizResultListItemDto>>, ISecuredRequest
{
    public string[] Roles => new[] { "Student"};

    public class GetListForLoggedStudentQuizResultQueryHandler : IRequestHandler<GetListForLoggedStudentQuizResultQuery, GetListResponse<GetListStudentQuizResultListItemDto>>
    {
        private readonly IStudentQuizResultRepository _studentQuizResultRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;

        public GetListForLoggedStudentQuizResultQueryHandler(IStudentQuizResultRepository studentQuizResultRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _studentQuizResultRepository = studentQuizResultRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentQuizResultListItemDto>> Handle(GetListForLoggedStudentQuizResultQuery request, CancellationToken cancellationToken)
        {
            Student? getStudent = await _contextOperationService.GetStudentFromContext();
            IPaginate<StudentQuizResult> studentQuizResults = await _studentQuizResultRepository.GetListAsync(
                predicate:sqr=>sqr.StudentId==getStudent.Id,
                index: 0,
                size: 100,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentQuizResultListItemDto> response = _mapper.Map<GetListResponse<GetListStudentQuizResultListItemDto>>(studentQuizResults);
            return response;
        }
    }
}

