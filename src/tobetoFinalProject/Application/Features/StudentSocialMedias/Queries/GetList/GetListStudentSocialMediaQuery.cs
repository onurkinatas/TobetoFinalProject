using Application.Features.StudentSocialMedias.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentSocialMedias.Constants.StudentSocialMediasOperationClaims;
using Application.Services.CacheForMemory;
using Microsoft.EntityFrameworkCore;
using Application.Services.ContextOperations;

namespace Application.Features.StudentSocialMedias.Queries.GetList;

public class GetListStudentSocialMediaQuery : IRequest<GetListResponse<GetListStudentSocialMediaListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentSocialMedias({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentSocialMedias";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentSocialMediaQueryHandler : IRequestHandler<GetListStudentSocialMediaQuery, GetListResponse<GetListStudentSocialMediaListItemDto>>
    {
        private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListStudentSocialMediaQueryHandler(IStudentSocialMediaRepository studentSocialMediaRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentSocialMediaRepository = studentSocialMediaRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListStudentSocialMediaListItemDto>> Handle(GetListStudentSocialMediaQuery request, CancellationToken cancellationToken)
        {

            IPaginate<StudentSocialMedia> studentSocialMedias = await _studentSocialMediaRepository.GetListAsync(
                include: se => se.Include(se => se.SocialMedia)
                    .Include(se => se.Student)
                    .ThenInclude(s => s.User),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentSocialMediaListItemDto> response = _mapper.Map<GetListResponse<GetListStudentSocialMediaListItemDto>>(studentSocialMedias);
            return response;
        }
    }
}