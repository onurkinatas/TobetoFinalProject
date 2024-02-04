using Application.Features.LectureCourses.Constants;
using Application.Features.LectureCourses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.LectureCourses.Constants.LectureCoursesOperationClaims;

namespace Application.Features.LectureCourses.Commands.Create;

public class CreateLectureCourseCommand : IRequest<CreatedLectureCourseResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid CourseId { get; set; }
    public Guid LectureId { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureCoursesOperationClaims.Create };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureCourses";

    public class CreateLectureCourseCommandHandler : IRequestHandler<CreateLectureCourseCommand, CreatedLectureCourseResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureCourseRepository _lectureCourseRepository;
        private readonly LectureCourseBusinessRules _lectureCourseBusinessRules;

        public CreateLectureCourseCommandHandler(IMapper mapper, ILectureCourseRepository lectureCourseRepository,
                                         LectureCourseBusinessRules lectureCourseBusinessRules)
        {
            _mapper = mapper;
            _lectureCourseRepository = lectureCourseRepository;
            _lectureCourseBusinessRules = lectureCourseBusinessRules;
        }

        public async Task<CreatedLectureCourseResponse> Handle(CreateLectureCourseCommand request, CancellationToken cancellationToken)
        {
            LectureCourse lectureCourse = _mapper.Map<LectureCourse>(request);
            await _lectureCourseBusinessRules.LectureCourseContentShouldNotExistsWhenInsert(lectureCourse.LectureId);
            await _lectureCourseBusinessRules.LectureCourseShouldNotExistsWhenInsert(lectureCourse.LectureId, lectureCourse.CourseId);
           
            await _lectureCourseRepository.AddAsync(lectureCourse);

            CreatedLectureCourseResponse response = _mapper.Map<CreatedLectureCourseResponse>(lectureCourse);
            return response;
        }
    }
}