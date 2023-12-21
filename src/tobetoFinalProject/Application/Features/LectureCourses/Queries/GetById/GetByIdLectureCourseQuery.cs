using Application.Features.LectureCourses.Constants;
using Application.Features.LectureCourses.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LectureCourses.Constants.LectureCoursesOperationClaims;

namespace Application.Features.LectureCourses.Queries.GetById;

public class GetByIdLectureCourseQuery : IRequest<GetByIdLectureCourseResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdLectureCourseQueryHandler : IRequestHandler<GetByIdLectureCourseQuery, GetByIdLectureCourseResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureCourseRepository _lectureCourseRepository;
        private readonly LectureCourseBusinessRules _lectureCourseBusinessRules;

        public GetByIdLectureCourseQueryHandler(IMapper mapper, ILectureCourseRepository lectureCourseRepository, LectureCourseBusinessRules lectureCourseBusinessRules)
        {
            _mapper = mapper;
            _lectureCourseRepository = lectureCourseRepository;
            _lectureCourseBusinessRules = lectureCourseBusinessRules;
        }

        public async Task<GetByIdLectureCourseResponse> Handle(GetByIdLectureCourseQuery request, CancellationToken cancellationToken)
        {
            LectureCourse? lectureCourse = await _lectureCourseRepository.GetAsync(predicate: lc => lc.Id == request.Id, cancellationToken: cancellationToken);
            await _lectureCourseBusinessRules.LectureCourseShouldExistWhenSelected(lectureCourse);

            GetByIdLectureCourseResponse response = _mapper.Map<GetByIdLectureCourseResponse>(lectureCourse);
            return response;
        }
    }
}