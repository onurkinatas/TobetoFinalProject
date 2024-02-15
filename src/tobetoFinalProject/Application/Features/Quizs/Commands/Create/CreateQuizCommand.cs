using Application.Features.Quizs.Constants;
using Application.Features.Quizs.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Quizs.Constants.QuizsOperationClaims;
using Application.Services.PoolQuestions;

namespace Application.Features.Quizs.Commands.Create;

public class CreateQuizCommand : IRequest<CreatedQuizResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public Guid ExamId { get; set; }
    public int QuizQuestionCount { get; set; }
    public int PoolId { get; set; }

    public string[] Roles => new[] { Admin, Write, QuizsOperationClaims.Create };

    public class CreateQuizCommandHandler : IRequestHandler<CreateQuizCommand, CreatedQuizResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly QuizBusinessRules _quizBusinessRules;
        private readonly IPoolQuestionsService _poolQuestionService;


        public CreateQuizCommandHandler(IMapper mapper, IQuizRepository quizRepository,
                                         QuizBusinessRules quizBusinessRules, IPoolQuestionsService poolQuestionService)
        {
            _mapper = mapper;
            _quizRepository = quizRepository;
            _quizBusinessRules = quizBusinessRules;
            _poolQuestionService = poolQuestionService;
        }

        public async Task<CreatedQuizResponse> Handle(CreateQuizCommand request, CancellationToken cancellationToken)
        {
            Quiz quiz = _mapper.Map<Quiz>(request);
            
            List<int> quizQuestions=await _poolQuestionService.RandomQuestionGenerator(request.QuizQuestionCount, request.PoolId);
            quiz.QuizQuestions = quizQuestions.Select(questionId => new QuizQuestion
            {
                QuizId = quiz.Id,
                QuestionId = questionId
            }).ToList();
            await _quizRepository.AddAsync(quiz);

            CreatedQuizResponse response = _mapper.Map<CreatedQuizResponse>(quiz);
            return response;
        }
    }
}