using Application.Features.LectureCompletionConditions.Queries.GetById;
using Application.Features.LectureCompletionConditions.Rules;
using Application.Services.ContextOperations;
using Application.Services.Lectures;
using Application.Services.LectureViews;
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
        private readonly ILecturesService _lecturesService;
        private readonly ILectureViewsService _lectureViewsService;
        private readonly ILectureViewRepository _lectureViewRepository;


        public GetByLoggedStudentCompletionConditionQueryHandler(IMapper mapper, ILectureCompletionConditionRepository lectureCompletionConditionRepository, LectureCompletionConditionBusinessRules lectureCompletionConditionBusinessRules, IContextOperationService contextOperationService, ILectureRepository lectureRepository, ILectureViewRepository lectureViewRepository, ILecturesService lecturesService, ILectureViewsService lectureViewsService)
        {
            _mapper = mapper;
            _lectureCompletionConditionRepository = lectureCompletionConditionRepository;
            _lectureCompletionConditionBusinessRules = lectureCompletionConditionBusinessRules;
            _contextOperationService = contextOperationService;
            _lectureRepository = lectureRepository;
            _lectureViewRepository = lectureViewRepository;
            _lecturesService = lecturesService;
            _lectureViewsService = lectureViewsService;
        }

        public async Task<GetByLoggedStudentCompletionConditionResponse> Handle(GetByLoggedStudentCompletionConditionQuery request, CancellationToken cancellationToken)
        {
            Student getStudent = await _contextOperationService.GetStudentFromContext();
            
            int contentCount = await _lecturesService.GetAllContentCountByLectureId(request.LectureId,cancellationToken);
            int lectureViewCount = await _lectureViewsService.ContentViewedByLectureId(request.LectureId, getStudent.Id);

            LectureCompletionCondition? lectureCompletionCondition = await _lectureCompletionConditionRepository.GetAsync(predicate: lcc => lcc.LectureId == request.LectureId&&lcc.StudentId==getStudent.Id, cancellationToken: cancellationToken);


            GetByLoggedStudentCompletionConditionResponse response = new GetByLoggedStudentCompletionConditionResponse();
            response.CompletionPercentage = lectureCompletionCondition == null ? 0 : lectureCompletionCondition.CompletionPercentage;
            response.TotalWatchedCount = lectureViewCount;
            response.TotalContentCount = contentCount;
            return response;
        }
    }
}