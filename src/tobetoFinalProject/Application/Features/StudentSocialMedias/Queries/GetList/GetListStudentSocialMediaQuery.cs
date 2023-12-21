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

namespace Application.Features.StudentSocialMedias.Queries.GetList;

public class GetListStudentSocialMediaQuery : IRequest<GetListResponse<GetListStudentSocialMediaListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentSocialMedias({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentSocialMedias";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentSocialMediaQueryHandler : IRequestHandler<GetListStudentSocialMediaQuery, GetListResponse<GetListStudentSocialMediaListItemDto>>
    {
        private readonly IStudentSocialMediaRepository _studentSocialMediaRepository;
        private readonly IMapper _mapper;

        public GetListStudentSocialMediaQueryHandler(IStudentSocialMediaRepository studentSocialMediaRepository, IMapper mapper)
        {
            _studentSocialMediaRepository = studentSocialMediaRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentSocialMediaListItemDto>> Handle(GetListStudentSocialMediaQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentSocialMedia> studentSocialMedias = await _studentSocialMediaRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentSocialMediaListItemDto> response = _mapper.Map<GetListResponse<GetListStudentSocialMediaListItemDto>>(studentSocialMedias);
            return response;
        }
    }
}