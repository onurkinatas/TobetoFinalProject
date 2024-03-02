using Application.Features.StudentQuizResults.Constants;
using Application.Features.StudentQuizResults.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.StudentQuizResults.Constants.StudentQuizResultsOperationClaims;
using Application.Services.ContextOperations;
using Application.Services.Quizs;
using Application.Services.QuizQuestions;
using Application.Services.StudentQuizResults;

namespace Application.Features.StudentQuizResults.Queries.GetByQuizId;

public class GetByIdStudentQuizResultQuery : IRequest<GetByIdStudentQuizResultResponse>, ISecuredRequest
{
    public int QuizId { get; set; }

    public string[] Roles => new[] { Admin, Read,"Student" };

    public class GetByIdStudentQuizResultQueryHandler : IRequestHandler<GetByIdStudentQuizResultQuery, GetByIdStudentQuizResultResponse>
    {
        private readonly IMapper _mapper;
        private readonly IStudentQuizResultRepository _studentQuizResultRepository;
        private readonly StudentQuizResultBusinessRules _studentQuizResultBusinessRules;
        private readonly IContextOperationService _contextOperationService;
        private readonly IStudentQuizResultsService _studentQuizResultsService;
        private readonly IQuizsService _quizsService;
        public GetByIdStudentQuizResultQueryHandler(IMapper mapper, IStudentQuizResultRepository studentQuizResultRepository, StudentQuizResultBusinessRules studentQuizResultBusinessRules, IContextOperationService contextOperationService, IQuizsService quizsService, IStudentQuizResultsService studentQuizResultsService)
        {
            _mapper = mapper;
            _studentQuizResultRepository = studentQuizResultRepository;
            _studentQuizResultBusinessRules = studentQuizResultBusinessRules;
            _contextOperationService = contextOperationService;
            _quizsService = quizsService;
            _studentQuizResultsService = studentQuizResultsService;
        }

        public async Task<GetByIdStudentQuizResultResponse> Handle(GetByIdStudentQuizResultQuery request, CancellationToken cancellationToken)
        {
            Student? getStudent = await _contextOperationService.GetStudentFromContext();

            StudentQuizResult? studentQuizResult = await _studentQuizResultRepository.GetAsync(
                predicate: sqr => sqr.QuizId == request.QuizId&&sqr.StudentId==getStudent.Id, 
                cancellationToken: cancellationToken
                );

            await _studentQuizResultBusinessRules.StudentQuizResultShouldExistWhenSelected(studentQuizResult);

            int getQuestionCount = await _quizsService.GetQuizQuestionCount(studentQuizResult.QuizId);
            int point = _studentQuizResultsService.QuizPointCalculator(studentQuizResult.CorrectAnswerCount, getQuestionCount);
            int emptyAnswerCount = _studentQuizResultsService.QuizEmptyAnswerCalculator(studentQuizResult.CorrectAnswerCount, studentQuizResult.WrongAnswerCount, getQuestionCount);

            studentQuizResult.EmptyAnswerCount = emptyAnswerCount;
            GetByIdStudentQuizResultResponse response = _mapper.Map<GetByIdStudentQuizResultResponse>(studentQuizResult);
            response.Point = point;
            return response;
        }
    }
}