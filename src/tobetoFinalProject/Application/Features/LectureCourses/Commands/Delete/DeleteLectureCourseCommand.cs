using Application.Features.LectureCourses.Constants;
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

namespace Application.Features.LectureCourses.Commands.Delete;

public class DeleteLectureCourseCommand : IRequest<DeletedLectureCourseResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Write, LectureCoursesOperationClaims.Delete };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetLectureCourses";

    public class DeleteLectureCourseCommandHandler : IRequestHandler<DeleteLectureCourseCommand, DeletedLectureCourseResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureCourseRepository _lectureCourseRepository;
        private readonly LectureCourseBusinessRules _lectureCourseBusinessRules;

        public DeleteLectureCourseCommandHandler(IMapper mapper, ILectureCourseRepository lectureCourseRepository,
                                         LectureCourseBusinessRules lectureCourseBusinessRules)
        {
            _mapper = mapper;
            _lectureCourseRepository = lectureCourseRepository;
            _lectureCourseBusinessRules = lectureCourseBusinessRules;
        }

        public async Task<DeletedLectureCourseResponse> Handle(DeleteLectureCourseCommand request, CancellationToken cancellationToken)
        {
            LectureCourse? lectureCourse = await _lectureCourseRepository.GetAsync(predicate: lc => lc.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureCourseBusinessRules.LectureCourseShouldExistWhenSelected(lectureCourse);

            await _lectureCourseRepository.DeleteAsync(lectureCourse!);

            DeletedLectureCourseResponse response = _mapper.Map<DeletedLectureCourseResponse>(lectureCourse);
            return response;
        }
    }
}