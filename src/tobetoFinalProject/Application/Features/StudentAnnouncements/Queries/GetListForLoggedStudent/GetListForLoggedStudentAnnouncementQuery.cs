using Application.Features.StudentAnnouncements.Queries.GetById;
using Application.Features.StudentAnnouncements.Queries.GetList;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;
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

namespace Application.Features.StudentAnnouncements.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentAnnouncementQuery : IRequest<GetListResponse<GetListForLoggedStudentAnnouncementResponse>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }
    
    public string[] Roles => new[] { "Student" };

    public bool BypassCache { get; }
    public TimeSpan? SlidingExpiration { get; }

    public class GetListForLoggedStudentAnnouncementQueryHandler : IRequestHandler<GetListForLoggedStudentAnnouncementQuery, GetListResponse<GetListForLoggedStudentAnnouncementResponse>>
    {
        private readonly IStudentAnnouncementRepository _studentAnnouncementRepository;
        private readonly IMapper _mapper;
        private readonly ICacheMemoryService _cacheMemoryService;
        private readonly IContextOperationService _contextOperationService;
        public GetListForLoggedStudentAnnouncementQueryHandler(IStudentAnnouncementRepository studentAnnouncementRepository, IMapper mapper, ICacheMemoryService cacheMemoryService, IContextOperationService contextOperationService)
        {
            _studentAnnouncementRepository = studentAnnouncementRepository;
            _mapper = mapper;
            _cacheMemoryService = cacheMemoryService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListForLoggedStudentAnnouncementResponse>> Handle(GetListForLoggedStudentAnnouncementQuery request, CancellationToken cancellationToken)
        {

            Student getStudent = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentAnnouncement> studentAnnouncements = await _studentAnnouncementRepository.GetListAsync(
                predicate:sa=>sa.StudentId==getStudent.Id,
                include: sa => sa.Include(sa => sa.Student)
                 .ThenInclude(s => s.User)
                 .Include(ll => ll.Announcement),
                 index: request.PageRequest.PageIndex,
                 size: request.PageRequest.PageSize,
                 orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                 cancellationToken: cancellationToken
             );

            GetListResponse<GetListForLoggedStudentAnnouncementResponse> response = _mapper.Map<GetListResponse<GetListForLoggedStudentAnnouncementResponse>>(studentAnnouncements);
            return response;
        }
    }
}
