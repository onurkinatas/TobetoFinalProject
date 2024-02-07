using Application.Features.ClassLectures.Queries.GetList;
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

namespace Application.Features.ClassLectures.Queries.GetListForCompleted;
public class GetListClassLectureForCompletedQuery : IRequest<GetListResponse<GetListClassLectureListItemDto>>, ISecuredRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => new[] { "Student" };

    public bool BypassCache { get; }
    public string CacheKey => $"GetListClassLectures({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string CacheGroupKey => "GetClassLectures";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListClassLectureForCompletedQueryHandler : IRequestHandler<GetListClassLectureForCompletedQuery, GetListResponse<GetListClassLectureListItemDto>>
    {
        private readonly IClassLectureRepository _classLectureRepository;
        private readonly IMapper _mapper;
        private readonly IContextOperationService _contextOperationService;
        public GetListClassLectureForCompletedQueryHandler(IClassLectureRepository classLectureRepository, IMapper mapper, IContextOperationService contextOperationService)
        {
            _classLectureRepository = classLectureRepository;
            _mapper = mapper;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetListResponse<GetListClassLectureListItemDto>> Handle(GetListClassLectureForCompletedQuery request, CancellationToken cancellationToken)
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