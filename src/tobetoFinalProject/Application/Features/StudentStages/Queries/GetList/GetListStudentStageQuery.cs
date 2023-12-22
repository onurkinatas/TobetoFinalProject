using Application.Features.StudentStages.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentStages.Constants.StudentStagesOperationClaims;

namespace Application.Features.StudentStages.Queries.GetList;

public class GetListStudentStageQuery : IRequest<GetListResponse<GetListStudentStageListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentStages({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentStages";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentStageQueryHandler : IRequestHandler<GetListStudentStageQuery, GetListResponse<GetListStudentStageListItemDto>>
    {
        private readonly IStudentStageRepository _studentStageRepository;
        private readonly IMapper _mapper;

        public GetListStudentStageQueryHandler(IStudentStageRepository studentStageRepository, IMapper mapper)
        {
            _studentStageRepository = studentStageRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentStageListItemDto>> Handle(GetListStudentStageQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentStage> studentStages = await _studentStageRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentStageListItemDto> response = _mapper.Map<GetListResponse<GetListStudentStageListItemDto>>(studentStages);
            return response;
        }
    }
}