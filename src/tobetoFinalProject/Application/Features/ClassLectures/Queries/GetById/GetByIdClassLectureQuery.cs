using Application.Features.ClassLectures.Constants;
using Application.Features.ClassLectures.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ClassLectures.Constants.ClassLecturesOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.CacheForMemory;

namespace Application.Features.ClassLectures.Queries.GetById;

public class GetByIdClassLectureQuery : IRequest<GetByIdClassLectureResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read , "Student" };

    public class GetByIdClassLectureQueryHandler : IRequestHandler<GetByIdClassLectureQuery, GetByIdClassLectureResponse>
    {
        private readonly IMapper _mapper;
        private readonly IClassLectureRepository _classLectureRepository;
        private readonly ClassLectureBusinessRules _classLectureBusinessRules;
        private readonly ICacheMemoryService _cacheMemoryService;

        public GetByIdClassLectureQueryHandler(IMapper mapper, IClassLectureRepository classLectureRepository, ClassLectureBusinessRules classLectureBusinessRules, ICacheMemoryService cacheMemoryService)
        {
            _mapper = mapper;
            _classLectureRepository = classLectureRepository;
            _classLectureBusinessRules = classLectureBusinessRules;
            _cacheMemoryService = cacheMemoryService;
        }

        public async Task<GetByIdClassLectureResponse> Handle(GetByIdClassLectureQuery request, CancellationToken cancellationToken)
        {
            List<Guid> getCacheClassIds = _cacheMemoryService.GetStudentClassIdFromCache();

            ClassLecture? classLecture = await _classLectureRepository.GetAsync(
                predicate: ce => getCacheClassIds.Contains(ce.StudentClassId),
                include: ca => ca.Include(ca => ca.Lecture)
                    .ThenInclude(m => m.Manufacturer)
                    .Include(ca => ca.Lecture)
                    .ThenInclude(m => m.Category)
                    .Include(ca => ca.StudentClass),
                cancellationToken: cancellationToken);
            await _classLectureBusinessRules.ClassLectureShouldExistWhenSelected(classLecture);

            GetByIdClassLectureResponse response = _mapper.Map<GetByIdClassLectureResponse>(classLecture);
            return response;
        }
    }
}