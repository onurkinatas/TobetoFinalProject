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

namespace Application.Features.LectureCourses.Commands.Update;

public class UpdateLectureCourseCommand : IRequest<UpdatedLectureCourseResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public Guid LectureId { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureCoursesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureCourses";

    public class UpdateLectureCourseCommandHandler : IRequestHandler<UpdateLectureCourseCommand, UpdatedLectureCourseResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureCourseRepository _lectureCourseRepository;
        private readonly LectureCourseBusinessRules _lectureCourseBusinessRules;

        public UpdateLectureCourseCommandHandler(IMapper mapper, ILectureCourseRepository lectureCourseRepository,
                                         LectureCourseBusinessRules lectureCourseBusinessRules)
        {
            _mapper = mapper;
            _lectureCourseRepository = lectureCourseRepository;
            _lectureCourseBusinessRules = lectureCourseBusinessRules;
        }

        public async Task<UpdatedLectureCourseResponse> Handle(UpdateLectureCourseCommand request, CancellationToken cancellationToken)
        {
            LectureCourse? lectureCourse = await _lectureCourseRepository.GetAsync(predicate: lc => lc.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureCourseBusinessRules.LectureCourseShouldExistWhenSelected(lectureCourse);
            await _lectureCourseBusinessRules.LectureCourseShouldNotExistsWhenUpdate(lectureCourse.LectureId, lectureCourse.CourseId);
            lectureCourse = _mapper.Map(request, lectureCourse);
          
            await _lectureCourseRepository.UpdateAsync(lectureCourse!);

            UpdatedLectureCourseResponse response = _mapper.Map<UpdatedLectureCourseResponse>(lectureCourse);
            return response;
        }
    }
}