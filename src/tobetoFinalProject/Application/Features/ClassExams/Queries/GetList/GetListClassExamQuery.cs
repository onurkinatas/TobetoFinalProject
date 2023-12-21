using Application.Features.ClassExams.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ClassExams.Constants.ClassExamsOperationClaims;

namespace Application.Features.ClassExams.Queries.GetList;

public class GetListClassExamQuery : IRequest<GetListResponse<GetListClassExamListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListClassExams({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetClassExams";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListClassExamQueryHandler : IRequestHandler<GetListClassExamQuery, GetListResponse<GetListClassExamListItemDto>>
    {
        private readonly IClassExamRepository _classExamRepository;
        private readonly IMapper _mapper;

        public GetListClassExamQueryHandler(IClassExamRepository classExamRepository, IMapper mapper)
        {
            _classExamRepository = classExamRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListClassExamListItemDto>> Handle(GetListClassExamQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ClassExam> classExams = await _classExamRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListClassExamListItemDto> response = _mapper.Map<GetListResponse<GetListClassExamListItemDto>>(classExams);
            return response;
        }
    }
}