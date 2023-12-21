using Application.Features.Lectures.Constants;
using Application.Features.Lectures.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Lectures.Constants.LecturesOperationClaims;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Lectures.Queries.GetById;

public class GetByIdLectureQuery : IRequest<GetByIdLectureResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read };

    public class GetByIdLectureQueryHandler : IRequestHandler<GetByIdLectureQuery, GetByIdLectureResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureRepository _lectureRepository;
        private readonly LectureBusinessRules _lectureBusinessRules;

        public GetByIdLectureQueryHandler(IMapper mapper, ILectureRepository lectureRepository, LectureBusinessRules lectureBusinessRules)
        {
            _mapper = mapper;
            _lectureRepository = lectureRepository;
            _lectureBusinessRules = lectureBusinessRules;
        }

        public async Task<GetByIdLectureResponse> Handle(GetByIdLectureQuery request, CancellationToken cancellationToken)
        {
            Lecture? lecture = await _lectureRepository.GetAsync(
                predicate: l => l.Id == request.Id,
                include: m => m.Include(m => m.LectureCourses)
                   .ThenInclude(mc => mc.Course)
                   .ThenInclude(c => c.CourseContents)
                   .ThenInclude(cc => cc.Content)
                   .ThenInclude(c => c.ContentCategory)
                   .Include(m => m.LectureCourses)
                   .ThenInclude(mc => mc.Course)
                   .ThenInclude(c => c.CourseContents)
                   .ThenInclude(cc => cc.Content)
                   .ThenInclude(c => c.Manufacturer)
                   .Include(m => m.LectureCourses)
                   .ThenInclude(mc => mc.Course)
                   .ThenInclude(c => c.CourseContents)
                   .ThenInclude(cc => cc.Content)
                   .ThenInclude(c => c.SubType)
                   .Include(m => m.LectureCourses)
                   .ThenInclude(mc => mc.Course)
                   .ThenInclude(c => c.CourseContents)
                   .ThenInclude(cc => cc.Content)
                   .ThenInclude(c => c. Language)
                   .Include(m => m.Manufacturer)
                   .Include(m => m.Category)
                   .Include(m => m.LectureCourses)
                   .ThenInclude(mc => mc.Course)
                   .ThenInclude(c => c.CourseContents)
                   .ThenInclude(cc => cc.Content)
                   .ThenInclude(cc => cc.ContentInstructors)
                   .ThenInclude(cc => cc.Instructor),
                cancellationToken: cancellationToken);
            await _lectureBusinessRules.LectureShouldExistWhenSelected(lecture);

            GetByIdLectureResponse response = _mapper.Map<GetByIdLectureResponse>(lecture);
            return response;
        }
    }
}