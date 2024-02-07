using Application.Features.ClassLectures.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Paging;
using MediatR;
using static Application.Features.ClassLectures.Constants.ClassLecturesOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.CacheForMemory;
using Application.Services.ContextOperations;

namespace Application.Features.ClassLectures.Queries.GetList;

public class GetListClassLectureQuery : IRequest<GetListResponse<GetListClassLectureListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { Admin, Read, "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListClassLectures({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetClassLectures";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListClassLectureQueryHandler : IRequestHandler<GetListClassLectureQuery, GetListResponse<GetListClassLectureListItemDto>>
    {
        private readonly IClassLectureRepository _classLectureRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;
        public GetListClassLectureQueryHandler(IClassLectureRepository classLectureRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _classLectureRepository = classLectureRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListClassLectureListItemDto>> Handle(GetListClassLectureQuery request, CancellationToken cancellationToken)
        {
            ICollection<Guid> getClassIds = await _contextOperationService.GetStudentClassesFromContext();

            IPaginate<ClassLecture> classLectures = await _classLectureRepository.GetListAsync(
                predicate: ce => getClassIds.Contains(ce.StudentClassId),
                include: ca => ca.Include(ca => ca.Lecture)
                    .ThenInclude(m => m.Manufacturer)
                    .Include(ca => ca.Lecture)
                    .ThenInclude(m => m.Category)
                    .Include(ca => ca.StudentClass),
                orderBy: ce => ce.OrderByDescending(x => x.CreatedDate),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );
            
            GetListResponse<GetListClassLectureListItemDto> response = _mapper.Map<GetListResponse<GetListClassLectureListItemDto>>(classLectures);
            return response;
        }
    }
}