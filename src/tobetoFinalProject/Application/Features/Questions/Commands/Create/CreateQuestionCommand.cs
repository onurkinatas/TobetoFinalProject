using Application.Features.Questions.Constants;
using Application.Features.Questions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Questions.Constants.QuestionsOperationClaims;
using Application.Services.ImageService;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Questions.Commands.Create;

public class CreateQuestionCommand : IRequest<CreatedQuestionResponse>, ISecuredRequest,ILoggableRequest, ITransactionalRequest
{
    public string? ImageUrl { get; set; }
    public string Sentence { get; set; }
    public int CorrectOptionId { get; set; }
    public ICollection<QuestionOption>? QuestionOptions { get; set; }
    public ICollection<PoolQuestion>? PoolQuestions { get; set; }
    public string[] Roles => new[] { Admin, Write, QuestionsOperationClaims.Create };

    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, CreatedQuestionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;
        private readonly QuestionBusinessRules _questionBusinessRules;
        private readonly ImageServiceBase _ýmageServiceBase;


        public CreateQuestionCommandHandler(IMapper mapper, IQuestionRepository questionRepository,
                                         QuestionBusinessRules questionBusinessRules, ImageServiceBase ýmageServiceBase)
        {
            _mapper = mapper;
            _questionRepository = questionRepository;
            _questionBusinessRules = questionBusinessRules;
            _ýmageServiceBase = ýmageServiceBase;
        }

        public async Task<CreatedQuestionResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            Question question = _mapper.Map<Question>(request);

            await _questionBusinessRules.QuestionOptionsCountMustBeLessThanSevenWhenInsert(question.QuestionOptions.Count);
            await _questionBusinessRules.QuestionOptionsMustBeDifferentWhenInsert(question.QuestionOptions);
            await _questionBusinessRules.PoolQuestionsMustBeDifferentWhenInsert(question.PoolQuestions);
            await _questionBusinessRules.QuestionOptionsHaveToContainCorrectOptionId(question.QuestionOptions,question.CorrectOptionId);

            await _questionRepository.AddAsync(question);

            CreatedQuestionResponse response = _mapper.Map<CreatedQuestionResponse>(question);
            return response;
        }
    }
}