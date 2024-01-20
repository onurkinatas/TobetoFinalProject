using Application.Features.Courses.Constants;
using Application.Features.Courses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Courses.Constants.CoursesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Courses.Commands.Update;

public class UpdateCourseCommand : IRequest<UpdatedCourseResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<CourseContent> CourseContents { get; set; }

    public string[] Roles => new[] { Admin, Write, CoursesOperationClaims.Update };

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string CacheGroupKey => "GetCourses";

    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, UpdatedCourseResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        private readonly CourseBusinessRules _courseBusinessRules;

        public UpdateCourseCommandHandler(IMapper mapper, ICourseRepository courseRepository,
                                         CourseBusinessRules courseBusinessRules)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
            _courseBusinessRules = courseBusinessRules;
        }

        public async Task<UpdatedCourseResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            Course? course = await _courseRepository.GetAsync(
                predicate: c => c.Id == request.Id,
                include: c => c.Include(c => c.CourseContents)
                .Include(c => c.LectureCourses),
                cancellationToken: cancellationToken);
            await _courseBusinessRules.CourseShouldExistWhenSelected(course);
            course = _mapper.Map(request, course);

            await _courseBusinessRules.CourseShouldNotExistsWhenUpdate(course.Name);

            await _courseRepository.UpdateAsync(course!);

            UpdatedCourseResponse response = _mapper.Map<UpdatedCourseResponse>(course);
            return response;
        }
    }
}