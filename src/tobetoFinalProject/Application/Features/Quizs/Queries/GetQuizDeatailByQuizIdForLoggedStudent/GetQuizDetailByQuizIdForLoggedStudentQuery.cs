using Application.Features.Quizs.Queries.GetQuizSession;
using Application.Features.Quizs.Rules;
using Application.Services.ContextOperations;
using Application.Services.Repositories;
using Application.Services.StudentQuizOptions;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Quizs.Queries.GetQuizDeatailByQuizIdForLoggedStudent;
public class GetQuizDetailByQuizIdForLoggedStudentQuery : IRequest<GetQuizDetailByQuizIdForLoggedStudentResponse>, ISecuredRequest
{
    public int QuizId { get; set; }

    public string[] Roles => new[] { "Admin", "Student" };

    public class GetQuizDetailByQuizIdForLoggedStudentQueryHandler : IRequestHandler<GetQuizDetailByQuizIdForLoggedStudentQuery, GetQuizDetailByQuizIdForLoggedStudentResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly QuizBusinessRules _quizBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        private readonly IStudentQuizOptionsService _studentQuizOptionService;

        public GetQuizDetailByQuizIdForLoggedStudentQueryHandler(IMapper mapper, IQuizRepository quizRepository, QuizBusinessRules quizBusinessRules, IContextOperationService contextOperationService, IStudentQuizOptionsService studentQuizOptionService)
        {
            _mapper = mapper;
            _quizRepository = quizRepository;
            _quizBusinessRules = quizBusinessRules;
            _contextOperationService = contextOperationService;
            _studentQuizOptionService = studentQuizOptionService;
        }

        public async Task<GetQuizDetailByQuizIdForLoggedStudentResponse> Handle(GetQuizDetailByQuizIdForLoggedStudentQuery request, CancellationToken cancellationToken)
        {
            Student getStudent =await _contextOperationService.GetStudentFromContext();

            Quiz? quiz = await _quizRepository.GetAsync(
                include: q => q
                    .Include(q => q.QuizQuestions)
                    .ThenInclude(qq => qq.Question)
                    .ThenInclude(q => q.QuestionOptions)
                    .ThenInclude(qo => qo.Option),
                predicate: q => q.Id == request.QuizId,
                cancellationToken: cancellationToken);
            await _quizBusinessRules.QuizShouldExistWhenSelected(quiz);

            var studentAnswers = await _studentQuizOptionService.GetAllAsync(sqo => sqo.StudentId == getStudent.Id && sqo.QuizId == quiz.Id);


            GetQuizDetailByQuizIdForLoggedStudentResponse response = _mapper.Map<GetQuizDetailByQuizIdForLoggedStudentResponse>(quiz);
            response.StudentOptions = studentAnswers;
            return response;
        }
    }
}

