using Application.Features.StudentClassStudents.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentClassStudents.Constants.StudentClassStudentsOperationClaims;

namespace Application.Features.StudentClassStudents.Queries.GetList;

public class GetListStudentClassStudentQuery : IRequest<GetListResponse<GetListStudentClassStudentListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentClassStudents({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentClassStudents";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentClassStudentQueryHandler : IRequestHandler<GetListStudentClassStudentQuery, GetListResponse<GetListStudentClassStudentListItemDto>>
    {
        private readonly IStudentClassStudentRepository _studentClassStudentRepository;
        private readonly IMapper _mapper;

        public GetListStudentClassStudentQueryHandler(IStudentClassStudentRepository studentClassStudentRepository, IMapper mapper)
        {
            _studentClassStudentRepository = studentClassStudentRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentClassStudentListItemDto>> Handle(GetListStudentClassStudentQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentClassStudent> studentClassStudents = await _studentClassStudentRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentClassStudentListItemDto> response = _mapper.Map<GetListResponse<GetListStudentClassStudentListItemDto>>(studentClassStudents);
            return response;
        }
    }
}