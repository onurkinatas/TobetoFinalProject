using Application.Features.StudentSurveys.Queries.GetList;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentSurveys.Queries.GetListForLoggedStudent;
public class GetListStudentSurveyForLoggedStudentQuery : IRequest<GetListResponse<GetListStudentSurveyForLoggedStudentListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] {"Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentSurveys({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentSurveys";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentSurveyForLoggedStudentQueryHandler : IRequestHandler<GetListStudentSurveyForLoggedStudentQuery, GetListResponse<GetListStudentSurveyForLoggedStudentListItemDto>>
    {
        private readonly IStudentSurveyRepository _studentSurveyRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;
        public GetListStudentSurveyForLoggedStudentQueryHandler(IStudentSurveyRepository studentSurveyRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _studentSurveyRepository = studentSurveyRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentSurveyForLoggedStudentListItemDto>> Handle(GetListStudentSurveyForLoggedStudentQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            IPaginate<StudentSurvey> studentSurveys = await _studentSurveyRepository.GetListAsync(
                predicate:ss=>ss.StudentId==getStudent.Id,
                include:ss=>ss.Include(ss=>ss.Survey),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentSurveyForLoggedStudentListItemDto> response = _mapper.Map<GetListResponse<GetListStudentSurveyForLoggedStudentListItemDto>>(studentSurveys);
            return response;
        }
    }
}