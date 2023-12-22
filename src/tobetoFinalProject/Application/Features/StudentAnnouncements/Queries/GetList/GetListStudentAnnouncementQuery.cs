using Application.Features.StudentAnnouncements.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentAnnouncements.Constants.StudentAnnouncementsOperationClaims;

namespace Application.Features.StudentAnnouncements.Queries.GetList;

public class GetListStudentAnnouncementQuery : IRequest<GetListResponse<GetListStudentAnnouncementListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentAnnouncements({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentAnnouncements";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentAnnouncementQueryHandler : IRequestHandler<GetListStudentAnnouncementQuery, GetListResponse<GetListStudentAnnouncementListItemDto>>
    {
        private readonly IStudentAnnouncementRepository _studentAnnouncementRepository;
        private readonly IMapper _mapper;

        public GetListStudentAnnouncementQueryHandler(IStudentAnnouncementRepository studentAnnouncementRepository, IMapper mapper)
        {
            _studentAnnouncementRepository = studentAnnouncementRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentAnnouncementListItemDto>> Handle(GetListStudentAnnouncementQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentAnnouncement> studentAnnouncements = await _studentAnnouncementRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentAnnouncementListItemDto> response = _mapper.Map<GetListResponse<GetListStudentAnnouncementListItemDto>>(studentAnnouncements);
            return response;
        }
    }
}