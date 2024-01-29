using Application.Features.LectureCompletionConditions.Queries.GetById;
using Application.Features.LectureCompletionConditions.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver.Core.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.LectureCompletionConditions.Queries.GetForLoggedStudent;
public class GetByLoggedStudentCompletionConditionQuery : IRequest<GetByLoggedStudentCompletionConditionResponse>, ISecuredRequest
{
    public Guid LectureId { get; set; }

    public string[] Roles => new[] { "Admin", "Student" };

    public class GetByLoggedStudentCompletionConditionQueryHandler : IRequestHandler<GetByLoggedStudentCompletionConditionQuery, GetByLoggedStudentCompletionConditionResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILectureCompletionConditionRepository _lectureCompletionConditionRepository;
        private readonly LectureCompletionConditionBusinessRules _lectureCompletionConditionBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        private readonly ILectureRepository _lectureRepository;
        private readonly ILectureViewRepository _lectureViewRepository;


        public GetByLoggedStudentCompletionConditionQueryHandler(IMapper mapper, ILectureCompletionConditionRepository lectureCompletionConditionRepository, LectureCompletionConditionBusinessRules lectureCompletionConditionBusinessRules, IContextOperationService contextOperationService, ILectureRepository lectureRepository, ILectureViewRepository lectureViewRepository)
        {
            _mapper = mapper;
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
            _lectureCompletionConditionBusinessRules = lectureCompletionConditionBusinessRules;
            _contextOperationService = contextOperationService;
            _lectureRepository = lectureRepository;
            _lectureViewRepository = lectureViewRepository;
        }

        public async Task<GetByLoggedStudentCompletionConditionResponse> Handle(GetByLoggedStudentCompletionConditionQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            Lecture lecture = await _lectureRepository.GetAsync(
                predicate:l=>l.Id ==request.LectureId, 
                include:l=>l.Include(l => l.LectureCourses)
                   .ThenInclude(lc => lc.Course)
                   .ThenInclude(c => c.CourseContents)
                   .ThenInclude(cc => cc.Content),
                cancellationToken:cancellationToken);
            var contentCount = lecture.LectureCourses
                .SelectMany(lc => lc.Course.CourseContents)
                .Count();
            ICollection<LectureView> lectureView = await _lectureViewRepository.GetAll(lv=>lv.LectureId==request.LectureId&&lv.StudentId==getStudent.Id);
            int lectureViewCount = lectureView.Count;

            float x = (lectureViewCount * 100) / contentCount;
            LectureCompletionCondition? lectureCompletionCondition = await _lectureCompletionConditionRepository.GetAsync(predicate: lcc => lcc.Id == request.LectureId, cancellationToken: cancellationToken);
            await _lectureCompletionConditionBusinessRules.LectureCompletionConditionShouldExistWhenSelected(lectureCompletionCondition);

            GetByLoggedStudentCompletionConditionResponse response = _mapper.Map<GetByLoggedStudentCompletionConditionResponse>(lectureCompletionCondition);
            return response;
        }
    }
}