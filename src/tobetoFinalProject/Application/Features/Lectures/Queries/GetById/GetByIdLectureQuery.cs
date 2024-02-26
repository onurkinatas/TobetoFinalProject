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
using Application.Services.Lectures;
using Application.Services.LectureViews;
using Application.Services.LectureCompletionConditions;
using Microsoft.EntityFrameworkCore.Query;

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
        private readonly ILecturesService _lectureService;
        private readonly ILectureViewsService _lectureViewsService;
        private readonly ILectureCompletionConditionsService _lectureCompletionConditionsService;
        private readonly IContextOperationService _contextOperationService;

        public GetByIdLectureQueryHandler(IMapper mapper, ILectureRepository lectureRepository, LectureBusinessRules lectureBusinessRules, ILectureLikesService lectureLikesService, IContextOperationService contextOperationService, ILectureViewsService lectureViewsService, ILecturesService lectureService, ILectureCompletionConditionsService lectureCompletionConditionsService)
        {
            _mapper = mapper;
            _lectureRepository = lectureRepository;
            _lectureBusinessRules = lectureBusinessRules;
            _lectureLikesService = lectureLikesService;
            _contextOperationService = contextOperationService;
            _lectureViewsService = lectureViewsService;
            _lectureService = lectureService;
            _lectureCompletionConditionsService = lectureCompletionConditionsService;
        }

        public async Task<GetByIdLectureResponse> Handle(GetByIdLectureQuery request, CancellationToken cancellationToken)
        {
            Student student = await _contextOperationService.GetStudentFromContext();

            Lecture? lecture = await _lectureRepository.GetAsync(
                predicate: l => l.Id == request.Id,
                include: IncludeLectureDetails(),
                cancellationToken: cancellationToken);
            await _lectureBusinessRules.LectureShouldExistWhenSelected(lecture);

            int contentCount = _lectureService.GetAllContentCountByLecture(lecture, cancellationToken);
            int lectureViewCount = _lectureViewsService.ContentViewedCountByLectureId(lecture.Id, student.Id);
            int completionPercentage =  _lectureCompletionConditionsService.CompletionPercentageCalculator(lectureViewCount, contentCount);

            int lectureLikeCount = _lectureLikesService.GetCount(lecture.Id);
            
            bool isLiked = await _lectureLikesService.IsLiked(lecture.Id, student.Id);

            int commentCount = lecture.StudentLectureComments.Count;

            GetByIdLectureResponse response = _mapper.Map<GetByIdLectureResponse>(lecture);
            response.IsLiked = isLiked;
            response.LikeCount = lectureLikeCount;
            response.CompletionPercentage = completionPercentage;
            response.TotalWatchedCount = lectureViewCount;
            response.TotalContentCount = contentCount;
            response.CommentCount = commentCount;
            return response;
        }
        private Func<IQueryable<Lecture>, IIncludableQueryable<Lecture, object>> IncludeLectureDetails()
        {
            return query => query
                    .Include(m => m.LectureCourses)
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
                                 .ThenInclude(c => c.Language)

                   .Include(m => m.Manufacturer)
                   .Include(m => m.Category)

                   .Include(m => m.LectureCourses)
                     .ThenInclude(mc => mc.Course)
                         .ThenInclude(c => c.CourseContents)
                           .ThenInclude(cc => cc.Content)
                              .ThenInclude(c => c.ContentTags)
                                 .ThenInclude(c => c.Tag)

                   .Include(m => m.LectureCourses)
                     .ThenInclude(mc => mc.Course)
                        .ThenInclude(c => c.CourseContents)
                         .ThenInclude(cc => cc.Content)
                             .ThenInclude(cc => cc.ContentInstructors)
                                .ThenInclude(cc => cc.Instructor)

                    .Include(m => m.StudentLectureComments);
        }
    }
}