using Application.Features.Lectures.Constants;
using Application.Features.Lectures.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Lectures.Constants.LecturesOperationClaims;
using Microsoft.EntityFrameworkCore;
using Application.Services.LectureLikes;
using Application.Services.ContextOperations;

namespace Application.Features.Lectures.Queries.GetById;

public class GetByIdLectureQuery : IRequest<GetByIdLectureResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { Admin, Read,"Student" };

    public class GetByIdLectureQueryHandler : IRequestHandler<GetByIdLectureQuery, GetByIdLectureResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureRepository _lectureRepository;
        private readonly LectureBusinessRules _lectureBusinessRules;
        private readonly ILectureLikesService _lectureLikesService;
        private readonly IContextOperationService _contextOperationService;

        public GetByIdLectureQueryHandler(IMapper mapper, ILectureRepository lectureRepository, LectureBusinessRules lectureBusinessRules, ILectureLikesService lectureLikesService, IContextOperationService contextOperationService)
        {
            _mapper = mapper;
            _lectureRepository = lectureRepository;
            _lectureBusinessRules = lectureBusinessRules;
            _lectureLikesService = lectureLikesService;
            _contextOperationService = contextOperationService;
        }

        public async Task<GetByIdLectureResponse> Handle(GetByIdLectureQuery request, CancellationToken cancellationToken)
        {
            Student student = await _contextOperationService.GetStudentFromContext();

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

            int lectureLikeCount = await _lectureLikesService.GetCount(lecture.Id);
            bool isLiked = await _lectureLikesService.IsLiked(lecture.Id, student.Id);


            GetByIdLectureResponse response = _mapper.Map<GetByIdLectureResponse>(lecture);
            response.IsLiked = isLiked;
            response.LikeCount = lectureLikeCount;
            return response;
        }
    }
}