using Application.Features.StudentClasses.Queries.GetList;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using Application.Services.StudentAnnouncements;
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

namespace Application.Features.StudentClasses.Queries.GetListForLoggedStudent;
public class GetListForLoggedStudentClassQuery : IRequest<GetListForLoggedStudentClassListItemDto>, ISecuredRequest, ICachableRequest
{
    public int ClassAnnouncementsCount { get; set; } = 12;
    public int ClassSurveysCount { get; set; } =12;
    public int ClassQuizsCount { get; set; } = 12;
    public int ClassLecturesCount { get; set; } =12;
    public int? UserId { get; set; }
    public string CacheKey => $"GetAllClassDetails({UserId})";
    public string CacheGroupKey => "GetAllClassDetails";
    public TimeSpan? SlidingExpiration { get; }
    public string[] Roles => new[] { "Student" };

    public bool BypassCache { get; set; }

    public class GetListForLoggedStudentClassQueryHandler : IRequestHandler<GetListForLoggedStudentClassQuery, GetListForLoggedStudentClassListItemDto>
    {
        private readonly IStudentClassRepository _studentClassRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;
        private readonly IStudentAnnouncementsService _studentAnnouncementService;

        public GetListForLoggedStudentClassQueryHandler(IStudentClassRepository studentClassRepository, IMapper mapper, IContextOperationService contextOperationService, IStudentAnnouncementsService studentAnnouncementService)
        {
            _studentClassRepository = studentClassRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
            _studentAnnouncementService = studentAnnouncementService;
        }

        public async Task<GetListForLoggedStudentClassListItemDto> Handle(GetListForLoggedStudentClassQuery request, CancellationToken cancellationToken)
        {
            ICollection<Guid> getStudentClasses = await _contextOperationService.GetStudentClassesFromContext();
            Student getStudent = await _contextOperationService.GetStudentFromContext();

            IPaginate<StudentClass> studentClasses = await _studentClassRepository.GetListAsync(
                predicate: sc => getStudentClasses.Contains(sc.Id),
                include: sc => sc
                .Include(sc => sc.ClassAnnouncements)
                     .ThenInclude(ca => ca.Announcement)
               .Include(sc => sc.ClassLectures)
                    .ThenInclude(ca => ca.Lecture)
               .Include(sc => sc.ClassQuizs)
                    .ThenInclude(ca => ca.Quiz)
               .Include(sc => sc.ClassSurveys)
                      .ThenInclude(ca => ca.Survey),
                index: 0,
                size: 100,
                cancellationToken: cancellationToken
            );

            var studentAnnouncements = await _studentAnnouncementService.GetAllAsync(ss=>ss.StudentId==getStudent.Id);
            
            int readingAnnouncement = studentClasses.Items.SelectMany(sc => sc.ClassAnnouncements).ToList().Count - studentAnnouncements.Count;
           
            StudentClass studentClassGetData = new()
            {
                
                ClassAnnouncements = studentClasses.Items.SelectMany(sc => sc.ClassAnnouncements).Take(request.ClassAnnouncementsCount).OrderByDescending(ca=>ca.CreatedDate).ToList(),
                ClassLectures = studentClasses.Items.SelectMany(sc => sc.ClassLectures).Take(request.ClassLecturesCount).OrderByDescending(ca => ca.CreatedDate).ToList(),
                ClassQuizs = studentClasses.Items.SelectMany(sc => sc.ClassQuizs).Take(request.ClassQuizsCount).OrderByDescending(ca => ca.CreatedDate).ToList(),
                ClassSurveys = studentClasses.Items.SelectMany(sc => sc.ClassSurveys).Take(request.ClassSurveysCount).OrderByDescending(ca => ca.CreatedDate).ToList()
            };

            GetListForLoggedStudentClassListItemDto response = _mapper.Map<GetListForLoggedStudentClassListItemDto>(studentClassGetData);
            response.ReadingAnnouncement = readingAnnouncement;
            return response;
        }
    }
}