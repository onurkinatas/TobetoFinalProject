using Application.Features.QuizQuestions.Constants;
using Application.Features.QuizQuestions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.QuizQuestions.Constants.QuizQuestionsOperationClaims;

namespace Application.Features.QuizQuestions.Commands.Update;

public class UpdateQuizQuestionCommand : IRequest<UpdatedQuizQuestionResponse>, ISecuredRequest, ILoggableRequest, ITransactionalRequest
{
    public int Id { get; set; }
    public int QuizId { get; set; }
    public int QuestionId { get; set; }

    public string[] Roles => new[] { Admin, Write, QuizQuestionsOperationClaims.Update };

    public class UpdateQuizQuestionCommandHandler : IRequestHandler<UpdateQuizQuestionCommand, UpdatedQuizQuestionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuizQuestionRepository _quizQuestionRepository;
        private readonly QuizQuestionBusinessRules _quizQuestionBusinessRules;

        public UpdateQuizQuestionCommandHandler(IMapper mapper, IQuizQuestionRepository quizQuestionRepository,
                                         QuizQuestionBusinessRules quizQuestionBusinessRules)
        {
            _mapper = mapper;
            _quizQuestionRepository = quizQuestionRepository;
            _quizQuestionBusinessRules = quizQuestionBusinessRules;
        }

        public async Task<UpdatedQuizQuestionResponse> Handle(UpdateQuizQuestionCommand request, CancellationToken cancellationToken)
        {
            QuizQuestion? quizQuestion = await _quizQuestionRepository.GetAsync(predicate: qq => qq.Id == request.Id, cancellationToken: cancellationToken);
            await _quizQuestionBusinessRules.QuizQuestionShouldExistWhenSelected(quizQuestion);
            quizQuestion = _mapper.Map(request, quizQuestion);

            await _quizQuestionRepository.UpdateAsync(quizQuestion!);

            UpdatedQuizQuestionResponse response = _mapper.Map<UpdatedQuizQuestionResponse>(quizQuestion);
            return response;
        }
    }
}