using Application.Features.StudentClasses.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.StudentClasses.Constants.StudentClassesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.StudentClasses.Queries.GetList;

public class GetListStudentClassQuery : IRequest<GetListResponse<GetListStudentClasses>>, ISecuredRequest/*, ICachableRequest*/
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListStudentClasses({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetStudentClasses";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListStudentClassQueryHandler : IRequestHandler<GetListStudentClassQuery, GetListResponse<GetListStudentClasses>>
    {
        private readonly IStudentClassRepository _studentClassRepository;
        private readonly IMapper _mapper;

        public GetListStudentClassQueryHandler(IStudentClassRepository studentClassRepository, IMapper mapper)
        {
            _studentClassRepository = studentClassRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListStudentClasses>> Handle(GetListStudentClassQuery request, CancellationToken cancellationToken)
        {
            IPaginate<StudentClass> studentClasses = await _studentClassRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                include: sc => sc.Include(sc => sc.ClassAnnouncements)
               .ThenInclude(ca => ca.Announcement)
               .Include(sc => sc.ClassLectures)
               .ThenInclude(ca => ca.Lecture)
               .Include(sc => sc.ClassExams)
               .ThenInclude(ca => ca.Exam)
               .Include(sc => sc.StudentClassStudentes)
               .ThenInclude(ca => ca.Student)
               .ThenInclude(ss => ss.City)
               .Include(sc => sc.StudentClassStudentes)
               .ThenInclude(ca => ca.Student)
               .ThenInclude(ss => ss.District)
               .Include(sc => sc.ClassSurveys)
               .ThenInclude(ca => ca.Survey),
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListStudentClasses> response = _mapper.Map<GetListResponse<GetListStudentClasses>>(studentClasses);
            return response;
        }
    }
}