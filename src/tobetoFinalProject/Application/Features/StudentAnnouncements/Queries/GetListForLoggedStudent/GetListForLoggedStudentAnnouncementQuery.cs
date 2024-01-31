using Application.Features.StudentAnnouncements.Queries.GetById;
using Application.Features.StudentAnnouncements.Queries.GetList;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentAnnouncements.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentAnnouncementQuery : IRequest<List<GetListForLoggedStudentAnnouncementResponse>>, ISecuredRequest
{
    public string[] Roles => new[] { "Student" };

    public bool BypassCache { get; }
    public TimeSpan? SlidingExpiration { get; }

    public class GetListForLoggedStudentAnnouncementQueryHandler : IRequestHandler<GetListForLoggedStudentAnnouncementQuery, List<GetListForLoggedStudentAnnouncementResponse>>
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

        public async Task<List<GetListForLoggedStudentAnnouncementResponse>> Handle(GetListForLoggedStudentAnnouncementQuery request, CancellationToken cancellationToken)
        {

            Student getStudent = await _contextOperationService.GetStudentFromContext();

            List<StudentAnnouncement> studentAnnouncements = 
                _studentAnnouncementRepository.GetAllWithoutPaginate(sa => sa.StudentId == getStudent.Id);
            List<GetListForLoggedStudentAnnouncementResponse> response = _mapper.Map<List<GetListForLoggedStudentAnnouncementResponse>>(studentAnnouncements);
            return response;
        }
    }
}
