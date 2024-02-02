using Application.Features.StudentAnnouncements.Queries.GetList;
using Application.Features.StudentAnnouncements.Queries.GetListByAnnouncementId;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
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

namespace Application.Features.StudentAnnouncements.Queries.GetListByAnnouncementId;
public class GetListByAnnouncementIdStudentAnnouncementQuery : IRequest<GetListResponse<GetListByAnnouncementIdStudentAnnouncementResponse>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    public Guid AnnouncementId { get; set; }

    public string[] Roles => new[] { "Admin" };

    public TimeSpan? SlidingExpiration { get; }

    public class GetListByAnnouncementIdStudentAnnouncementQueryHandler : IRequestHandler<GetListByAnnouncementIdStudentAnnouncementQuery, GetListResponse<GetListByAnnouncementIdStudentAnnouncementResponse>>
    {
        private readonly IStudentAnnouncementRepository _lectureLikeRepository;
        private readonly IMapper _mapper;

        public GetListByAnnouncementIdStudentAnnouncementQueryHandler(IStudentAnnouncementRepository lectureLikeRepository, IMapper mapper)
        {
            _lectureLikeRepository = lectureLikeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByAnnouncementIdStudentAnnouncementResponse>> Handle(GetListByAnnouncementIdStudentAnnouncementQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentAnnouncement> lectureLikes = await _lectureLikeRepository.GetListAsync(
                include: sa => sa.Include(sa => sa.Student)
                .ThenInclude(s => s.User)
                .Include(sa => sa.Announcement),
                predicate:sa=>sa.AnnouncementId==request.AnnouncementId,
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListByAnnouncementIdStudentAnnouncementResponse> response = _mapper.Map<GetListResponse<GetListByAnnouncementIdStudentAnnouncementResponse>>(lectureLikes);
            return response;
        }
    }
}
