using Application.Features.Courses.Constants;
using Application.Features.Courses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Courses.Constants.CoursesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Courses.Queries.GetById;

public class GetByIdCourseQuery : IRequest<GetByIdCourseResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdCourseQueryHandler : IRequestHandler<GetByIdCourseQuery, GetByIdCourseResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourseRepository _courseRepository;
        private readonly CourseBusinessRules _courseBusinessRules;

        public GetByIdCourseQueryHandler(IMapper mapper, ICourseRepository courseRepository, CourseBusinessRules courseBusinessRules)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
            _courseBusinessRules = courseBusinessRules;
        }

        public async Task<GetByIdCourseResponse> Handle(GetByIdCourseQuery request, CancellationToken cancellationToken)
        {
            Course? course = await _courseRepository.GetAsync(
                predicate: c => c.Id == request.Id,
                include: c => c.Include(c => c.CourseContents)
                    .ThenInclude(cc => cc.Content)
                    .ThenInclude(c => c.ContentInstructors)
                    .ThenInclude(cý => cý.Instructor)
                    .Include(c => c.CourseContents)
                    .ThenInclude(cc => cc.Content)
                    .ThenInclude(c => c.Manufacturer)
                    .Include(c => c.CourseContents)
                    .ThenInclude(cc => cc.Content)
                    .ThenInclude(c => c.ContentCategory)
                    .Include(c => c.CourseContents)
                    .ThenInclude(cc => cc.Content)
                    .ThenInclude(c => c.Language)
                    .Include(c => c.CourseContents)
                    .ThenInclude(cc => cc.Content)
                    .ThenInclude(c => c.SubType),
                cancellationToken: cancellationToken);
            await _courseBusinessRules.CourseShouldExistWhenSelected(course);

            GetByIdCourseResponse response = _mapper.Map<GetByIdCourseResponse>(course);
            return response;
        }
    }
}